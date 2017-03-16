using GameManagement;

namespace PenguinPairs.GameObjects
{
    class Arrow : GameObjectList
    {
        protected SpriteGameObject arrow_normal, arrow_hover;

        public int Width { get { return arrow_normal.Width; } }
        public int Height { get { return arrow_normal.Height; } }

        public bool Pressed { get; protected set; }

        public Arrow(string assetName_normal, string assetName_hover, int layer = 0, string id = "", int sheetIndex = 0) : base(layer, id)
        {
            arrow_normal = new SpriteGameObject(assetName_normal, 0, id, sheetIndex);
            arrow_hover = new SpriteGameObject(assetName_hover, 1, id, sheetIndex);
            Add(arrow_normal);
            Add(arrow_hover);
            arrow_hover.Visible = false;
            Pressed = false;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            arrow_hover.Visible = arrow_normal.BoundingBox.Contains(inputHelper.MousePosition);
            Pressed = inputHelper.MouseLeftButtonPressed() && arrow_hover.Visible;
        }
    }
}
