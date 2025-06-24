using DI;
using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class SoundController
    {
        private readonly AudioSource _source;
        private readonly SoundData _soundData;
        
        [Inject]
        public SoundController(AudioSource audioSource, SoundData soundData)
        {
            _source = audioSource;
            _soundData = soundData;
        }

        public void Play(SoundName name)
        {
            var clip = _soundData[name];
            if (!clip)
                return;
            
            _source.PlayOneShot(clip);
        }
    }
}