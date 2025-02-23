using System.Collections.Generic;
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

            Game.InitializeManagers(levelManager);

            //Add all custom managers here
            Game.InitializeCustomManagers();

            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }

            Debug.Log("GameInit Initialized");
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
    }
}