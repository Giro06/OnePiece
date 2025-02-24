using _Scripts.Managers;
using Giroo.Core.Scriptables;
using UnityEngine;

namespace Giroo.Core
{
    public static class Game
    {
        public static GameSettings Settings { get; private set; }
        public static LevelData LevelData { get; private set; }

        public static AudioSource AudioSource { get; private set; }

        public static LevelManager LevelManager { get; private set; }
        public static GameManager GameManager { get; private set; }

        public static InputManager InputManager { get; private set; }

        public static CurrencyManager CurrencyManager { get; private set; }

        public static AdManager AdManager { get; private set; }

        public static SoundManager SoundManager { get; private set; }

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

        public static void InitializeAudioSource(AudioSource audioSource)
        {
            AudioSource = audioSource;
            Debug.Log("AudioSource Initialized");
        }

        public static void InitializeManagers(LevelManager levelManager, GameManager gameManager,
            InputManager inputManager, CurrencyManager currencyManager, AdManager adManager, SoundManager soundManager)
        {
            LevelManager = levelManager;
            GameManager = gameManager;
            InputManager = inputManager;
            CurrencyManager = currencyManager;
            AdManager = adManager;
            SoundManager = soundManager;
            Debug.Log("Managers Initialized");
        }


        public static void InitializeCustomManagers()
        {
            Debug.Log("Custom Managers Initialized");
        }
    }
}