using PenguinPairs.GameObjects;
using PenguinPairs.MenuItems;
using Microsoft.Xna.Framework;
using GameManagement;

namespace PenguinPairs.States
{
    class HelpMenuState : GameObjectList
    {
        Button backButton;

        public HelpMenuState()
        {
            Add(new SpriteGameObject("Sprites/spr_background_help"));
            backButton = new Button("Sprites/spr_button_back", 1) { Position = new Vector2(415, 720) };
            Add(backButton);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (backButton.Pressed)
                GameEnvironment.GameStateManager.SwitchTo(GameState.TitleMenu);
        }
    }
}
