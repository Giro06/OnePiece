using _Scripts.EventBus;
using Giroo.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class LevelManager : IInitializable, IDisposable
    {
        private int _currentLevel = 1;
        private int _loopIndex = 1;

        private const string _levelSaveKey = "Level";

        private EventBinding<CoreEvents.GameInitialized> _gameInitializedEventBinding;
        private EventBinding<CoreEvents.LevelComplete> _levelCompleteEventBinding;
        private EventBinding<CoreEvents.LevelRestart> _levelRestartEventBinding;

        public void Initialize()
        {
            _gameInitializedEventBinding = new EventBinding<CoreEvents.GameInitialized>(OnGameInitialized);
            EventBus<CoreEvents.GameInitialized>.Subscribe(_gameInitializedEventBinding);

            _levelCompleteEventBinding = new EventBinding<CoreEvents.LevelComplete>(NextLevel);
            EventBus<CoreEvents.LevelComplete>.Subscribe(_levelCompleteEventBinding);

            _levelRestartEventBinding = new EventBinding<CoreEvents.LevelRestart>(RestartLevel);
            EventBus<CoreEvents.LevelRestart>.Subscribe(_levelRestartEventBinding);
        }

        public void OnGameInitialized()
        {
            LoadSavedData();

            if (Game.Settings.isTest)
            {
                LoadComplete(new AsyncOperation());
            }
            else
            {
                LoadCurrentLevel();
            }
        }

        public void LoadCurrentLevel()
        {
            var levelIndex = GetLevelIndex(_currentLevel);
            var scene = Game.LevelData.levels[levelIndex - 1];
            LoadAsync(scene.name);
        }

        public void NextLevel()
        {
            var oldLevel = _currentLevel;
            LevelUp();
            var oldLevelIndex = GetLevelIndex(oldLevel);
            var oldScene = Game.LevelData.levels[oldLevelIndex - 1];
            UnloadAsync(oldScene.name);
        }

        public void RestartLevel()
        {
            var oldLevel = _currentLevel;
            var oldLevelIndex = GetLevelIndex(oldLevel);
            var oldScene = Game.LevelData.levels[oldLevelIndex - 1];
            UnloadAsync(oldScene.name);
        }

        public void LoadAsync(string sceneName)
        {
            var async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            async.completed += LoadComplete;
            EventBus<CoreEvents.LevelLoading>.Publish(new CoreEvents.LevelLoading());
        }

        public void LoadComplete(AsyncOperation asyncOperation)
        {
            EventBus<CoreEvents.LevelLoaded>.Publish(new CoreEvents.LevelLoaded());
        }

        public void UnloadAsync(string level)
        {
            var async = SceneManager.UnloadSceneAsync(level);
            async.completed += UnloadComplete;
        }

        public void UnloadComplete(AsyncOperation asyncOperation)
        {
            LoadCurrentLevel();
        }

        #region Utils

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

        private void LevelUp()
        {
            _currentLevel++;
            SaveData();
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

        public void Dispose()
        {
            EventBus<CoreEvents.GameInitialized>.Unsubscribe(_gameInitializedEventBinding);
            EventBus<CoreEvents.LevelComplete>.Unsubscribe(_levelCompleteEventBinding);
            EventBus<CoreEvents.LevelRestart>.Unsubscribe(_levelRestartEventBinding);
            SaveData();
        }

        #endregion
    }
}