using PenguinPairs.GameObjects;
using Microsoft.Xna.Framework;
using GameManagement;

namespace PenguinPairs.MenuItems
{
    class Slider : GameObjectList
    {
        SpriteGameObject back, front;
        int leftMargin, rightMargin;
        bool dragging;

        public float Value
        {
            get { return (front.Position.X - back.Position.X - leftMargin) / (back.Width - leftMargin - rightMargin - front.Width); }
            set
            {
                float newXPos = value * (back.Width - leftMargin - rightMargin - front.Width) + back.Position.X + leftMargin;
                front.Position = new Vector2(newXPos, front.Position.Y);
            }
        }

        public Slider(string sliderBack, string sliderFront, int layer = 0, string id = "") : base(layer, id)
        {
            leftMargin = 5;
            rightMargin = 7;
            back = new SpriteGameObject(sliderBack, 0);
            Add(back);
            front = new SpriteGameObject(sliderFront, 1) { Position = new Vector2(leftMargin, 8) };
            Add(front);
            dragging = false;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (!inputHelper.MouseLeftButtonDown())
            {
                dragging = false;
                return;
            }
            if (front.BoundingBox.Contains(inputHelper.MousePosition) || dragging)
            {
                float newXPos = MathHelper.Clamp(inputHelper.MousePosition.X - back.GlobalPosition.X - front.Width / 2,
                    back.Position.X + leftMargin,
                    back.Position.X + back.Width - front.Width - rightMargin);
                front.Position = new Vector2(newXPos, front.Position.Y);
                dragging = true;
            }
        }
    }
}
