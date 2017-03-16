using System.Collections.Generic;
using System.IO;
using PenguinPairs.MenuItems;
using Microsoft.Xna.Framework;
using GameManagement;

namespace PenguinPairs.GameObjects
{
    class Level : GameObjectList
    {
        protected List<Animal> animals;
        protected List<SpriteGameObject> sharks;

        public bool Locked { get; set; }
        public bool Solved { get; set; }
        public bool FirstMoveMade { get; set; }

        Button quitButton, hintButton, retryButton;
        public bool PlayerQuit { get { return quitButton.Pressed; } }

        public bool PlayerWon { get { return (Find("pairList") as PairList).Completed; } }

        public Level(int levelIndex, StreamReader reader, int layer = 0, string id = "") : base(layer, id)
        {
            animals = new List<Animal>();
            sharks = new List<SpriteGameObject>();

            Add(new SpriteGameObject("Sprites/spr_background_level"));

            string levelTitle = reader.ReadLine();
            string levelHelp = reader.ReadLine();
            int nrPairs = int.Parse(reader.ReadLine());

            string[] stringList = reader.ReadLine().Split();
            int width = int.Parse(stringList[0]);
            int height = int.Parse(stringList[1]);

            GameObjectList playingField = new GameObjectList(1, "playingField") { Position = new Vector2((PenguinPairs.Screen.X - width * 73) / 2, 100) };
            Add(playingField);

            stringList = reader.ReadLine().Split();
            int hintX = int.Parse(stringList[0]) - 1;
            int hintY = int.Parse(stringList[1]) - 1;
            int hintDirection = int.Parse(stringList[2]);
            SpriteGameObject hint = new SpriteGameObject("Sprites/spr_arrow_hint@4", 2, "hint", hintDirection) { Position = new Vector2(hintX, hintY) * new Vector2(73, 72) };
            playingField.Add(hint);

            GameObjectGrid tileField = new GameObjectGrid(height, width, 1, "tileField") { CellWidth = 73, CellHeight = 72 };
            for (int row = 0; row < height; row++)
            {
                string currRow = reader.ReadLine();
                for (int col = 0; col < currRow.Length; col++)
                {
                    Tile t;
                    switch (currRow[col])
                    {
                        case '.':
                            tileField.Add(new Tile("Sprites/spr_field@2", 0, "", (row + col) % 2), col, row);
                            break;
                        case ' ':
                            tileField.Add(new Tile("Sprites/spr_wall") { Type = TileType.Background }, col, row);
                            break;
                        case 'r':
                        case 'b':
                        case 'g':
                        case 'o':
                        case 'p':
                        case 'y':
                        case 'm':
                        case 'x':
                        case 'R':
                        case 'B':
                        case 'G':
                        case 'O':
                        case 'P':
                        case 'Y':
                        case 'M':
                        case 'X':
                            t = new Tile("Sprites/spr_field@2", 0, "", (row + col) % 2);
                            tileField.Add(t, col, row);
                            string assetName = "Sprites/spr_penguin@8";
                            if (char.IsUpper(currRow[col]))
                                assetName = "Sprites/spr_penguin_boxed@8";
                            Animal a = new Animal(assetName, 2, "", currRow[col]);
                            a.Position = t.Position;
                            a.InitialPosition = t.Position;
                            playingField.Add(a);
                            animals.Add(a);
                            break;
                        case '@':
                            t = new Tile("Sprites/spr_field@2", 0, "", (row + col) % 2);
                            tileField.Add(t, col, row);
                            SpriteGameObject s = new SpriteGameObject("Sprites/spr_shark", 2) { Position = t.Position };
                            playingField.Add(s);
                            sharks.Add(s);
                            break;
                        default:
                            tileField.Add(new Tile("Sprites/spr_wall") { Type = TileType.Wall }, col, row);
                            break;
                    }
                }
            }

            playingField.Add(tileField);

            playingField.Add(new AnimalSelector(2, "animalSelector") { Visible = false });

            Add(new PairList(nrPairs, 1, "pairList") { Position = new Vector2(20, 15) });

            quitButton = new Button("Sprites/spr_button_quit", 1) { Position = new Vector2(1058, 20) };
            Add(quitButton);

            playingField.Add(new VisibilityTimer(hint, 0, "hintVisible"));
            hintButton = new Button("Sprites/spr_button_hint", 1, "hintButton") { Position = new Vector2(916, 20) };
            FirstMoveMade = false;
            Add(hintButton);
            retryButton = new Button("Sprites/spr_button_retry", 1, "retryButton") { Position = new Vector2(916, 20), Visible = false };
            Add(retryButton);

            SpriteGameObject helpField = new SpriteGameObject("Sprites/spr_help", 1);
            helpField.Position = new Vector2((PenguinPairs.Screen.X - helpField.Width) / 2, 780);
            Add(helpField);
            TextGameObject helpText = new TextGameObject("Fonts/HelpFont", 2) { Text = levelHelp.Replace('#', '\n'), Color = Color.DarkBlue };
            helpText.Position = new Vector2((PenguinPairs.Screen.X - helpText.Size.X) / 2, (90 - helpText.Size.Y) / 2 + 780);
            Add(helpText);
        }

        public override void Reset()
        {
            base.Reset();
            FirstMoveMade = false;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (hintButton.Pressed && hintButton.Visible)
                (Find("hintVisible") as VisibilityTimer).StartVisible();
            else if (retryButton.Pressed && retryButton.Visible)
                Reset();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            hintButton.Visible = GameEnvironment.GameSettingsManager.GetValue("hints") == "on" && !FirstMoveMade;
            retryButton.Visible = !hintButton.Visible;
        }

        public SpriteGameObject FindSharkAtPosition(Point p)
        {
            GameObjectGrid tileField = GameWorld.Find("tileField") as GameObjectGrid;
            Vector2 finalPosition = new Vector2(p.X * tileField.CellWidth, p.Y * tileField.CellHeight);
            foreach (SpriteGameObject s in sharks)
                if (s.Position == finalPosition)
                    return s;
            return null;
        }

        public Animal FindAnimalAtPosition(Point p)
        {
            foreach (Animal a in animals)
                if (a.GetCurrentBlock() == p && a.Velocity == Vector2.Zero)
                    return a;
            return null;
        }
    }
}
