using System;
using Microsoft.Xna.Framework;
using GameManagement;

namespace PenguinPairs.GameObjects
{
    class Animal : SpriteGameObject
    {
        public Vector2 InitialPosition { get; set; }

        protected bool boxed, initialEmptyBox;

        public bool IsSeal { get { return Sprite.SheetIndex == 7 && !boxed; } }
        public bool IsEmptyBox { get { return Sprite.SheetIndex == 7 && boxed; } }
        public bool IsMultiColoredPenguin { get { return Sprite.SheetIndex == 6 && !boxed; } }

        public Animal(string assetName, int layer = 0, string id = "", char color = 'r') : base(assetName, layer, id, 8)
        {
            boxed = char.IsUpper(color);
            initialEmptyBox = boxed && char.ToLower(color) == 'x';
            Sprite.SheetIndex = "brgyopmx".IndexOf(char.ToLower(color));
        }

        public override void Reset()
        {
            base.Reset();
            Position = InitialPosition;
            if (initialEmptyBox)
                Sprite.SheetIndex = 7;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (!Visible || boxed || Velocity != Vector2.Zero || !inputHelper.MouseLeftButtonPressed() || !BoundingBox.Contains(inputHelper.MousePosition))
                return;

            AnimalSelector selector = GameWorld.Find("animalSelector") as AnimalSelector;
            selector.Position = Position;
            selector.Visible = true;
            selector.SelectedAnimal = this;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!Visible || Velocity == Vector2.Zero)
                return;

            Point target = GetCurrentBlock();
            if (GetTileType(target) == TileType.Background)
            {
                Visible = false;
                Velocity = Vector2.Zero;
                return;
            }
            else if (GetTileType(target) == TileType.Wall)
            {
                StopMoving();
                return;
            }

            Level level = Root as Level;

            SpriteGameObject s = level.FindSharkAtPosition(target);
            if (s != null && s.Visible)
            {
                Visible = s.Visible = false;
                StopMoving();
            }

            Animal a = level.FindAnimalAtPosition(target);
            if (a == null || !a.Visible)
                return;
            if (a.IsEmptyBox)
            {
                Visible = false;
                a.Sprite.SheetIndex = Sprite.SheetIndex;
            }
            else if ((a.Sprite.SheetIndex == Sprite.SheetIndex || IsMultiColoredPenguin || a.IsMultiColoredPenguin) && !a.IsSeal)
            {
                Visible = a.Visible = false;
                (GameWorld.Find("pairList") as PairList).AddPair(Sprite.SheetIndex);
            }
            else
                StopMoving();
        }

        public Point GetCurrentBlock()
        {
            GameObjectGrid tileField = GameWorld.Find("tileField") as GameObjectGrid;
            Point p = new Point((int)Math.Floor(Position.X / tileField.CellWidth), (int)Math.Floor(Position.Y / tileField.CellHeight));
            if (Velocity.X > 0)
                p.X++;
            if (Velocity.Y > 0)
                p.Y++;
            return p;
        }

        protected bool IsOutsideField(Point p)
        {
            GameObjectGrid tileField = GameWorld.Find("tileField") as GameObjectGrid;
            return p.X < 0 || p.X >= tileField.Width || p.Y < 0 || p.Y >= tileField.Height;
        }

        protected TileType GetTileType(Point p)
        {
            if (IsOutsideField(p))
                return TileType.Background;
            return ((GameWorld.Find("tileField") as GameObjectGrid).Grid[p.X, p.Y] as Tile).Type;
            
        }

        protected void StopMoving()
        {
            GameObjectGrid tileField = GameWorld.Find("tileField") as GameObjectGrid;
            Velocity = Vector2.Normalize(Velocity);
            Vector2 oldBlock = new Vector2(GetCurrentBlock().X, GetCurrentBlock().Y) - Velocity;
            Position = oldBlock * new Vector2(tileField.CellWidth, tileField.CellHeight);
            Velocity = Vector2.Zero;
        }
    }
}
