using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Giroo.Core.Scriptables
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Core/LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public List<Scene> levels;
    }
}