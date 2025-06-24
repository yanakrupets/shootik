using System;
using Enums;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class Sound
    {
        [SerializeField] private SoundName soundName;
        [SerializeField] private AudioClip audioClip;

        public SoundName Name => soundName;
        public AudioClip AudioClip => audioClip;
    }
}