using System;
using System.Collections.Generic;
using _Scripts.EventBus;
using _Scripts.Managers;
using Giroo.Core.Scriptables;
using UnityEngine;

namespace Giroo.Core
{
    public class GameInit : MonoBehaviour
    {
        public GameSettings gameSettings;
        public LevelData levelData;

        private List<IUpdateable> _updateables = new List<IUpdateable>();
        private List<ILateUpdateable> _lateUpdateables = new List<ILateUpdateable>();
        private List<IFixedUpdateable> _fixedUpdateables = new List<IFixedUpdateable>();
        private List<IInitializable> _initializables = new List<IInitializable>();
        private List<IDisposable> _disposables = new List<IDisposable>();

        public void OnEnable()
        {
            Init();
        }

        public void Init()
        {
            Game.InitializeSettings(gameSettings);
            Game.InitializeLevelData(levelData);

            //Add all mandatory managers here
            LevelManager levelManager = new LevelManager();
            _initializables.Add(levelManager);
            _disposables.Add(levelManager);

            GameManager gameManager = new GameManager();
            _initializables.Add(gameManager);
            _disposables.Add(gameManager);


            Game.InitializeManagers(levelManager, gameManager);

            //Add all custom managers here
            Game.InitializeCustomManagers();

            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }

            Debug.Log("GameInit Initialized");
            EventBus<CoreEvents.GameInitialized>.Publish(new CoreEvents.GameInitialized());
        }

        public void Update()
        {
            foreach (var updateable in _updateables)
            {
                updateable.Update(Time.deltaTime);
            }
        }

        public void LateUpdate()
        {
            foreach (var lateUpdateable in _lateUpdateables)
            {
                lateUpdateable.LateUpdate(Time.deltaTime);
            }
        }

        public void FixedUpdate()
        {
            foreach (var fixedUpdateable in _fixedUpdateables)
            {
                fixedUpdateable.FixedUpdate(Time.deltaTime);
            }
        }

        public void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}