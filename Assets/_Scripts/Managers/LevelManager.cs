using Giroo.Core;
using UnityEngine;

namespace _Scripts.Managers
{
    public class LevelManager : IInitializable
    {
        private int _currentLevel = 1;
        private int _loopIndex = 1;

        private const string _levelSaveKey = "Level";

        public void Initialize()
        {
            LoadSavedData();
        }

        public void LoadLevel(int level)
        {
            var levelIndex = GetLevelIndex(level);
        }

        public int GetLevelIndex(int level)
        {
            var maxLevelCount = Game.LevelData.levels.Count;

            if (level <= maxLevelCount)
            {
                return level;
            }
            else
            {
                int loopedLevel = _loopIndex + ((level - maxLevelCount - 1) % (maxLevelCount - _loopIndex + 1));
                return loopedLevel;
            }
        }

        public int GetLevel()
        {
            return _currentLevel;
        }

        public void SetLevel(int level)
        {
            _currentLevel = level;
        }


        public void LoadSavedData()
        {
            _currentLevel = PlayerPrefs.GetInt(_levelSaveKey, 1);
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt(_levelSaveKey, _currentLevel);
            PlayerPrefs.Save();
        }
    }
}