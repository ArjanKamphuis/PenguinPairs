using PenguinPairs.MenuItems;
using Microsoft.Xna.Framework;
using GameManagement;

namespace PenguinPairs.States
{
    class TitleMenuState : GameObjectList
    {
        protected Button playButton, optionsButton, helpButton;

        public TitleMenuState()
        {
            Add(new SpriteGameObject("Sprites/spr_titlescreen", 0, "background"));

            playButton = new Button("Sprites/spr_button_play", 1) { Position = new Vector2(415, 540) };
            Add(playButton);
            optionsButton = new Button("Sprites/spr_button_options", 1) { Position = new Vector2(415, 650) };
            Add(optionsButton);
            helpButton = new Button("Sprites/spr_button_help", 1) { Position = new Vector2(415, 760) };
            Add(helpButton);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (optionsButton.Pressed)
                GameEnvironment.GameStateManager.SwitchTo(GameState.OptionsMenu);
            else if (helpButton.Pressed)
                GameEnvironment.GameStateManager.SwitchTo(GameState.HelpMenu);
            else if (playButton.Pressed)
                GameEnvironment.GameStateManager.SwitchTo(GameState.LevelMenu);
        }
    }
}
