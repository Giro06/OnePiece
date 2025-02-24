using System.Linq;
using _Scripts.EventBus;
using Giroo.Core;
using UnityEngine;

namespace _Scripts.Managers
{
    public class SoundManager : IInitializable, IDisposable
    {
        EventBinding<CoreEvents.FeelEvent> _feelEventBinding;

        private bool _isSoundOn;
        private const string _soundSaveKey = "SoundOn";

        public void Initialize()
        {
            _feelEventBinding = new EventBinding<CoreEvents.FeelEvent>(OnSoundEvent);
            EventBus<CoreEvents.FeelEvent>.Subscribe(_feelEventBinding);
            LoadSoundData();
        }

        public void Dispose()
        {
            EventBus<CoreEvents.FeelEvent>.Unsubscribe(_feelEventBinding);
        }


        private void OnSoundEvent(CoreEvents.FeelEvent feelEvent)
        {
            if (!IsSoundOn()) return;

            var clip = Game.Settings.feelDatas.First(x => x.feelType == feelEvent.FeelType);
            Game.AudioSource.PlayOneShot(clip.audioClip, 1);
        }


        public void SetSoundOn(bool isSoundOn)
        {
            _isSoundOn = isSoundOn;
            PlayerPrefs.SetInt(_soundSaveKey, isSoundOn ? 1 : 0);
            PlayerPrefs.Save();
        }

        public bool IsSoundOn()
        {
            return _isSoundOn;
        }

        private void LoadSoundData()
        {
            _isSoundOn = PlayerPrefs.GetInt(_soundSaveKey, 1) == 1;
        }
    }
}