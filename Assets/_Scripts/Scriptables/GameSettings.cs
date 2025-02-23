using UnityEngine;

namespace Giroo.Core.Scriptables
{
    [CreateAssetMenu(fileName = "DefaultGameSettings", menuName = "Core/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public bool isTest;
    }
}