using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManagement;
using PenguinPairs.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinPairs
{
    class PenguinPairs : GameEnvironment
    {
        [STAThread]
        static void Main()
        {
            using (PenguinPairs game = new PenguinPairs())
                game.Run();
        }

        public PenguinPairs()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            GameSettingsManager.SetValue("hints", "on");
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Screen = new Point(1200, 900);
            windowSize = new Point(1024, 768);

            GameStateManager.AddGameState(GameState.PlayingState, new PlayingState(Content));
            GameStateManager.AddGameState(GameState.TitleMenu, new TitleMenuState());
            GameStateManager.AddGameState(GameState.OptionsMenu, new OptionsMenuState());
            GameStateManager.AddGameState(GameState.LevelMenu, new LevelMenuState());
            GameStateManager.AddGameState(GameState.HelpMenu, new HelpMenuState());
            GameStateManager.AddGameState(GameState.LevelFinishedState, new LevelFinishedState());

            GameStateManager.SwitchTo(GameState.TitleMenu);

            ApplyResolutionSettings();
            Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - windowSize.X) / 2, (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - windowSize.Y) / 2);
        }
    }
}
