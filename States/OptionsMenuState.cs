using PenguinPairs.GameObjects;
using PenguinPairs.MenuItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using GameManagement;

namespace PenguinPairs.States
{
    class OptionsMenuState : GameObjectList
    {
        Button backButton;
        OnOffButton onOffButton;
        Slider musicVolumeSlider;

        public OptionsMenuState()
        {
            Add(new SpriteGameObject("Sprites/spr_background_options"));

            backButton = new Button("Sprites/spr_button_back", 1) { Position = new Vector2(415, 720) };
            Add(backButton);

            Add(new TextGameObject("Fonts/MenuFont", 1) { Text = "Hints", Color = Color.DarkBlue, Position = new Vector2(150, 340) });
            onOffButton = new OnOffButton("Sprites/spr_button_offon@2", 1) { Position = new Vector2(650, 340) };
            if (GameEnvironment.GameSettingsManager.GetValue("hints") == "on")
                onOffButton.On = true;
            Add(onOffButton);
            Add(new TextGameObject("Fonts/MenuFont", 1) { Text = "Music Volume", Color = Color.DarkBlue, Position = new Vector2(150, 480) });
            musicVolumeSlider = new Slider("Sprites/spr_slider_bar", "Sprites/spr_slider_button", 1) { Position = new Vector2(650, 500), Value = MediaPlayer.Volume };
            Add(musicVolumeSlider);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (backButton.Pressed)
                GameEnvironment.GameStateManager.SwitchTo(GameState.TitleMenu);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MediaPlayer.Volume = musicVolumeSlider.Value;
            if (onOffButton.On)
                GameEnvironment.GameSettingsManager.SetValue("hints", "on");
            else
                GameEnvironment.GameSettingsManager.SetValue("hints", "off");
        }
    }
}
