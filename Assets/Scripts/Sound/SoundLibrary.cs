using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Sound
{
    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "Game/Sound/SoundLibrary", order = 0)]
    public class SoundLibrary : ScriptableObject
    {
        #region --- Fields ---
        
        [SerializeField] private List<Sound> _sounds;
        
        #endregion
        
        
        #region --- Public Methods ---
        
        // Sound library sits under the resources folder
        public Sound GetSound(string soundName)
        {
            var sound = _sounds.Find(s => s.name == soundName);
            
            if (sound == null)
            {
                LoggerService.LogWarning($"Sound with name {soundName} not found!");
            }
            
            return sound;
        }
        
        #endregion
    }
}