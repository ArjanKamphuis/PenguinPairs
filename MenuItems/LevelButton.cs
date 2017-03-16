using PenguinPairs.GameObjects;
using Microsoft.Xna.Framework;
using GameManagement;

namespace PenguinPairs.MenuItems
{
    class LevelButton : GameObjectList
    {
        protected SpriteGameObject spr_lock, spr_solved, spr_unsolved;
        protected Level level;

        public int Width { get { return spr_lock.Width; } }
        public int Height { get { return spr_lock.Height; } }

        public int LevelIndex { get; protected set; }
        public bool Pressed { get; protected set; }

        public LevelButton(int levelIndex, Level level, int layer = 0, string id = "") : base(layer, id)
        {
            LevelIndex = levelIndex;
            this.level = level;
            spr_lock = new SpriteGameObject("Sprites/spr_lock", 2);
            Add(spr_lock);
            spr_solved = new SpriteGameObject("Sprites/spr_levels_solved@6", 0, "", LevelIndex - 1);
            Add(spr_solved);
            spr_unsolved = new SpriteGameObject("Sprites/spr_level_unsolved");
            Add(spr_unsolved);

            TextGameObject levelIndexText = new TextGameObject("Fonts/ScoreFont", 1) { Text = levelIndex.ToString(), Color = Color.DarkBlue };
            levelIndexText.Position = new Vector2(spr_lock.Width - levelIndexText.Size.X, spr_lock.Height - levelIndexText.Size.Y + 45) / 2;
            Add(levelIndexText);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            Pressed = inputHelper.MouseLeftButtonPressed() && !level.Locked && spr_lock.BoundingBox.Contains(inputHelper.MousePosition);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            spr_lock.Visible = level.Locked;
            spr_solved.Visible = level.Solved;
            spr_unsolved.Visible = !level.Solved;
        }
    }
}
