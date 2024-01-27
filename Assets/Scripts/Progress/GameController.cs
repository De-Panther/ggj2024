using System;
using System.Collections;
using Settings;
using UnityEngine;

namespace Progress
{
    public class GameController : MonoBehaviour
    {
        #region --- Fields ---
        
        public static event Action OnGameStart;
        public static event Action OnGameEnd;
        public static event Action OnGamePause;
        public static event Action OnGameResume;
        private SettingsManager _settingsManager;
        
        #endregion

        
        #region --- Constant ---
        
        private const string SETTINGS_MANAGER_PATH = "SettingsManager";
        private const string WIND = "wind";
        
        #endregion
        
        
        #region --- Singleton Pattern ---

        public static GameController Instance { get; private set; } // Singleton instance

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
            
            DontDestroyOnLoad(this.gameObject);

            LoadSettings();
        }

        #endregion
        
        
        #region --- Public Methods ---

        private void Start()
        {
            AudioSetup();
        }

        public void StartGame()
        {
            StartCoroutine(GameFlow());
        }
        
        public void PlaySfx(string soundName)
        {
            var sound = _settingsManager.SoundLibrary.GetSound(soundName);
            _settingsManager.SoundSettings.SetSfxAudioClip(sound.clip).Play();
        }
        
        public void StopPlayingSfx()
        {
            _settingsManager.SoundSettings.StopSfxAudioClip();
        }
        
        public void AddGameProgressionListener(IGameProgressListener listener)
        {
            OnGameStart -= listener.OnGameStart;
            OnGameStart += listener.OnGameStart;
            OnGameEnd -= listener.OnGameEnd;
            OnGameEnd += listener.OnGameEnd;
            OnGamePause -= listener.OnGamePause;
            OnGamePause += listener.OnGamePause;
            OnGameResume -= listener.OnGameResume;
            OnGameResume += listener.OnGameResume;
        }
        
        #endregion
        
        
        #region --- Private Methods ---

        private IEnumerator GameFlow()
        {
            Time.timeScale = 1; 
            OnGameStart?.Invoke();
            yield return new WaitForSeconds(_settingsManager.GameConfig.gameDuration);
            OnGameEnd?.Invoke();
        }

        public void PauseGame() 
        {
            Time.timeScale = 0; 
            OnGamePause?.Invoke();
        }

        public void ResumeGame() 
        {
            Time.timeScale = 1; 
            OnGameResume?.Invoke();
        }
        
        private void LoadSettings()
        {
            var settingsManager = Resources.Load<SettingsManager>(SETTINGS_MANAGER_PATH);
            _settingsManager = Instantiate(settingsManager, transform);
        }

        private void AudioSetup()
        {
            var main = _settingsManager.SoundLibrary.GetSound(WIND);
            _settingsManager.SoundSettings.SetMainAudioClip(main.clip).Play();
        }
        
        #endregion
    }
}