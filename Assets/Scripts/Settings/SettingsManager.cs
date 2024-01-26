using UnityEngine;

namespace Settings
{
    public class SettingsManager : MonoBehaviour
    {
        #region --- Inspector ---
        
        [SerializeField] private AudioSource _mainAudio;
        [SerializeField] private AudioSource _sfxAudio;
        
        #endregion
        
        
        #region --- Singleton ---
        
        public static SettingsManager Instance { get; private set; }

        public SoundSettings SoundSettings { get; private set; }
        
        
        #endregion
        
        
        #region --- Unity Methods ---

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            } else 
            {
                Destroy(gameObject);
                return;
            }
       
            DontDestroyOnLoad(gameObject);

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