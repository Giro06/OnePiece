using System;
using System.Collections.Generic;
using Giro.Core.Enum;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Giroo.Core.Scriptables
{
    [CreateAssetMenu(fileName = "DefaultGameSettings", menuName = "Core/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public bool isTest;

        [BoxGroup("Economy")] public int levelCompleteEarnAmount;
        [BoxGroup("Economy")] public int playOnAmount;
        [BoxGroup("Economy")] public int coinBuyAmount;


        [BoxGroup("Feeling")] public List<FeelData> feelDatas;

        public FeelData GetFeelData(FeelType feelType)
        {
            return feelDatas.Find(x => x.feelType == feelType);
        }
    }

    [Serializable]
    public class TutorialData
    {
        public int tutorialID;
        public int tutorialLevel;
    }

    [Serializable]
    public class FeelData
    {
        public FeelType feelType;

        [FormerlySerializedAs("useVibration")] public bool useHaptic;

        // [ShowIf("useHaptic")] public HapticPatterns.PresetType hapticPresetType;
        public bool useSound;
        [ShowIf("useSound")] public AudioClip audioClip;
        public bool useParticle;
        [ShowIf("useParticle")] public GameObject particlePrefab;
    }
}