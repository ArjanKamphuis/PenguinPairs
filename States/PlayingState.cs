using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinPairs.GameObjects;
using GameManagement;

namespace PenguinPairs.States
{
    class PlayingState : IGameLoopObject
    {
        public List<Level> Levels { get; protected set; }
        public Level CurrentLevel { get { return Levels[currentLevelIndex]; } }

        protected ContentManager content;

        protected int currentLevelIndex;
        public int CurrentLevelIndex
        {
            get { return currentLevelIndex; }
            set
            {
                if (value >= 0 && value < Levels.Count)
                    currentLevelIndex = value;
            }
        }

        public PlayingState(ContentManager content)
        {
            Levels = new List<Level>();
            currentLevelIndex = -1;
            this.content = content;
            LoadLevels(content.RootDirectory + "/Levels/levels.txt");
            LoadLevelsStatus(content.RootDirectory + "/Levels/levels_status.txt");
        }

        public void LoadLevels(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                int nrLevels = int.Parse(line);
                for (int currLevel = 1; currLevel <= nrLevels; currLevel++)
                    Levels.Add(new Level(currLevel, reader));
            }
        }

        public void LoadLevelsStatus(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                for (int i = 0; i < Levels.Count; i++)
                {
                    string[] elems = reader.ReadLine().Split(',');
                    if (elems.Length == 2)
                    {
                        Levels[i].Locked = bool.Parse(elems[0]);
                        Levels[i].Solved = bool.Parse(elems[1]);
                    }
                }
            }
        }

        public void WriteLevelsStatus(string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
                for (int i = 0; i < Levels.Count; i++)
                    writer.WriteLine(Levels[i].Locked.ToString() + "," + Levels[i].Solved.ToString());
        }

        public void NextLevel()
        {
            CurrentLevel.Reset();

            if (currentLevelIndex >= Levels.Count - 1)
                GameEnvironment.GameStateManager.SwitchTo(GameState.LevelMenu);
            else
            {
                currentLevelIndex++;
                Levels[currentLevelIndex].Locked = false;
            }
            WriteLevelsStatus(content.RootDirectory + "/Levels/levels_status.txt");
        }

        public void Reset()
        {
            CurrentLevel.Reset();
        }

        public void HandleInput(InputHelper inputHelper)
        {
            CurrentLevel.HandleInput(inputHelper);
        }

        public void Update(GameTime gameTime)
        {
            CurrentLevel.Update(gameTime);
            if (CurrentLevel.PlayerQuit)
            {
                CurrentLevel.Reset();
                GameEnvironment.GameStateManager.SwitchTo(GameState.LevelMenu);
            }
            else if (CurrentLevel.PlayerWon)
            {
                CurrentLevel.Solved = true;
                GameEnvironment.GameStateManager.SwitchTo(GameState.LevelFinishedState);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentLevel.Draw(gameTime, spriteBatch);
        }
    }
}
