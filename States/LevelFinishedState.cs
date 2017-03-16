using GameManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PenguinPairs.States
{
    class LevelFinishedState : GameObjectList
    {
        protected IGameLoopObject playingState;

        public LevelFinishedState()
        {
            playingState = GameEnvironment.GameStateManager.GetGameState(GameState.PlayingState);
            SpriteGameObject overlay = new SpriteGameObject("Sprites/spr_level_finished", 1, "you_win");
            overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
            Add(overlay);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (!inputHelper.KeyPressed(Keys.Space) && !inputHelper.MouseLeftButtonPressed())
                return;
            GameEnvironment.GameStateManager.SwitchTo(GameState.PlayingState);
            (playingState as PlayingState).NextLevel();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            playingState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
