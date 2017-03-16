using System.Collections.Generic;
using PenguinPairs.GameObjects;
using PenguinPairs.MenuItems;
using Microsoft.Xna.Framework;
using GameManagement;

namespace PenguinPairs.States
{
    class LevelMenuState : GameObjectList
    {
        Button backButton;

        public int LevelSelected
        {
            get
            {
                foreach (GameObject obj in gameObjects)
                {
                    LevelButton levelButton = obj as LevelButton;
                    if (levelButton != null && levelButton.Pressed)
                        return levelButton.LevelIndex;
                }
                return -1;
            }
        }

        public LevelMenuState()
        {
            Add(new SpriteGameObject("Sprites/spr_background_levelselect"));
            backButton = new Button("Sprites/spr_button_back", 1) { Position = new Vector2(415, 720) };
            Add(backButton);

            List<Level> levels = (GameEnvironment.GameStateManager.GetGameState(GameState.PlayingState) as PlayingState).Levels;
            for (int i = 0; i < 12; i++)
            {
                int row = i / 5;
                int column = i % 5;
                LevelButton level = new LevelButton(i + 1, levels[i]);
                level.Position = new Vector2(column * (level.Width + 30), row * (level.Height + 5)) + new Vector2(155, 230);
                Add(level);
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (LevelSelected != -1)
            {
                PlayingState playingState = GameEnvironment.GameStateManager.GetGameState(GameState.PlayingState) as PlayingState;
                playingState.CurrentLevelIndex = LevelSelected - 1;
                GameEnvironment.GameStateManager.SwitchTo(GameState.PlayingState);
            }
            else if (backButton.Pressed)
                GameEnvironment.GameStateManager.SwitchTo(GameState.TitleMenu);
        }
    }
}
