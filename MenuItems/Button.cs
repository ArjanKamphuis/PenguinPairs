using PenguinPairs.GameObjects;
using GameManagement;

namespace PenguinPairs.MenuItems
{
    class Button : SpriteGameObject
    {
        public bool Pressed { get; protected set; }

        public Button(string assetName, int layer = 0, string id = "", int sheetIndex = 0) : base(assetName, layer, id, sheetIndex)
        {
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            Pressed = inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains(inputHelper.MousePosition);
        }
    }
}
