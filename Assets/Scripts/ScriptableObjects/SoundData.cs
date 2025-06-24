using System.Linq;
using Enums;
using Serializable;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Sound Data", menuName = "Configs/Sound Data")]
    public class SoundData : ScriptableObject
    {
        [SerializeField] private Sound[] sounds;

        public Sound[] Sounds => sounds;
        
        public AudioClip this[SoundName soundName]
        {
            get
            {
                var sound = sounds.FirstOrDefault(s => s.Name == soundName);
                if (sound == null)
                {
                    Debug.LogError($"Sound with name {soundName} not found!");
                    return null;
                }
                return sound.AudioClip;
            }
        }
    }
}