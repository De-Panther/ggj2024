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

        [SerializeField] private GameObject[] _prefabs; // Initialize with your original GameObjects in the Unity Editor
        private GameObject[] _currentInstances;
        [SerializeField] private GameObject _window;
        
        #endregion

        
        #region --- Constant ---
        
        private const string SETTINGS_MANAGER_PATH = "SettingsManager";
        private const string WIND = "wind";
        
        #endregion
        
        
        #region --- Properties ---

        public bool InProgress
        {
            get;
            private set;
        }
        
        #endregion
        
        
        #region --- Singleton Pattern ---

        public static GameController Instance { get; private set; } 

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
            _window.SetActive(false);
            _currentInstances = new GameObject[_prefabs.Length];
            ResetScene();
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
        
        private void ResetScene()
        {
            for (int i = 0; i < _currentInstances.Length; i++)
            {
                if (_currentInstances[i] != null)
                {
                    Destroy(_currentInstances[i]);
                }

                _currentInstances[i] = Instantiate(_prefabs[i], _prefabs[i].transform.parent);
                _currentInstances[i].SetActive(true);
            }

            _window.SetActive(false);
            _window.SetActive(true);
            InProgress = false;
        }

        private IEnumerator GameFlow()
        {
            Debug.LogError("Start Game " +Time.time);
            OnGameStart?.Invoke();
            InProgress = true;
            yield return new WaitForSeconds(_settingsManager.GameConfig.gameDuration); // Substitute with your game duration
            OnGameEnd?.Invoke();
            Debug.LogError("End Game " +Time.time);
        }

        [ContextMenu("ResetGameFlow %r")]
        public void ResetGameFlow()
        {
            _settingsManager.SoundSettings.StopSfxAudioClip();
            StopAllCoroutines();
            ResetScene();
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