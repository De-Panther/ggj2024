using Progress;
using Sound;
using UnityEngine;

namespace Settings
{
    public class SettingsManager : MonoBehaviour
    {
        #region --- Inspector ---
        
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private AudioSource _mainAudio;
        [SerializeField] private AudioSource _sfxAudio;
        
        #endregion
        
        
        #region --- Properties ---
        
        public SoundSettings SoundSettings { get; private set; }
        public SoundLibrary SoundLibrary { get; private set; }
        public GameConfig GameConfig { get { return _gameConfig; } }

        #endregion
        
        
        #region --- Unity Methods ---

        private void Awake()
        {
            SoundLibrary = Resources.Load<SoundLibrary>("Sound/SoundLibrary");
            SoundSettings = new SoundSettings(_sfxAudio, _mainAudio);
            LoadSettings();
        }
        
        #endregion
        
        
        #region --- Private Methods ---

        private void LoadSettings()
        {
            SoundSettings.Load();
        }
        
        #endregion
        
    }
}