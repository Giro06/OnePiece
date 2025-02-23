using _Scripts.Managers;
using Giroo.Core.Scriptables;
using UnityEngine;

namespace Giroo.Core
{
    public static class Game
    {
        public static GameSettings Settings { get; private set; }
        public static LevelData LevelData { get; private set; }

        public static LevelManager LevelManager { get; private set; }
        public static GameManager GameManager { get; private set; }

        public static void InitializeSettings(GameSettings settings)
        {
            Settings = settings;
            Debug.Log("Settings Initialized");
        }

        public static void InitializeLevelData(LevelData levelData)
        {
            LevelData = levelData;
            Debug.Log("LevelData Initialized");
        }

        public static void InitializeManagers(LevelManager levelManager, GameManager gameManager)
        {
            LevelManager = levelManager;
            GameManager = gameManager;
            Debug.Log("Managers Initialized");
        }


        public static void InitializeCustomManagers()
        {
            Debug.Log("Custom Managers Initialized");
        }
    }
}