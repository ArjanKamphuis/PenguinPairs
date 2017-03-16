using Microsoft.Xna.Framework;
using GameManagement;

namespace PenguinPairs.GameObjects
{
    class AnimalSelector : GameObjectList
    {
        protected Arrow arrowRight, arrowUp, arrowLeft, arrowDown;
        public Animal SelectedAnimal { get; set; }

        public AnimalSelector(int layer = 0, string id = "") : base(layer, id)
        {
            arrowRight = new Arrow("Sprites/spr_arrow1@4", "Sprites/spr_arrow2@4", 0, "arrowRight", 0);
            arrowRight.Position = new Vector2(arrowRight.Width, 0);
            arrowUp = new Arrow("Sprites/spr_arrow1@4", "Sprites/spr_arrow2@4", 0, "arrowUp", 1);
            arrowUp.Position = new Vector2(0, -arrowUp.Height);
            arrowLeft = new Arrow("Sprites/spr_arrow1@4", "Sprites/spr_arrow2@4", 0, "arrowLeft", 2);
            arrowLeft.Position = new Vector2(-arrowLeft.Width, 0);
            arrowDown = new Arrow("Sprites/spr_arrow1@4", "Sprites/spr_arrow2@4", 0, "arrowDown", 3);
            arrowDown.Position = new Vector2(0, arrowDown.Height);

            Add(arrowRight);
            Add(arrowUp);
            Add(arrowLeft);
            Add(arrowDown);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (!Visible)
                return;

            base.HandleInput(inputHelper);
            Vector2 animalVelocity = Vector2.Zero;
            if (arrowDown.Pressed)
                animalVelocity.Y = 1;
            else if (arrowUp.Pressed)
                animalVelocity.Y = -1;
            else if (arrowLeft.Pressed)
                animalVelocity.X = -1;
            else if (arrowRight.Pressed)
                animalVelocity.X = 1;
            animalVelocity *= 300;

            if (inputHelper.MouseLeftButtonPressed())
                Visible = false;

            if (SelectedAnimal != null && animalVelocity != Vector2.Zero)
                SelectedAnimal.Velocity = animalVelocity;

            (GameWorld as Level).FirstMoveMade = true;
        }
    }
}
