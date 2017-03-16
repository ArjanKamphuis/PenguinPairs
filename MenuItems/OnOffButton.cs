using PenguinPairs.GameObjects;
using GameManagement;

namespace PenguinPairs.MenuItems
{
    class OnOffButton : SpriteGameObject
    {
        public bool On
        { 
            get { return Sprite.SheetIndex == 1; }
            set
            {
                if (value)
                    Sprite.SheetIndex = 1;
                else
                    Sprite.SheetIndex = 0;
            }
        }

        public OnOffButton(string assetName, int layer = 0, string id = "") : base(assetName, layer, id, 0)
        {
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains(inputHelper.MousePosition))
                Sprite.SheetIndex = 1 - Sprite.SheetIndex;
        }
    }
}
