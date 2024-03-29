using UnityEngine;

namespace Settings
{
    public class SoundSettings
    {
        #region --- Constants ---
        
        private const string MAIN_VOL = "mainVolume";
        private const string SFX_VOL = "sfxVolume";
        private const string MAIN_MUTE = "mainMute";
        private const string SFX_MUTE = "sfxMute";
        private const string ALL_MUTE = "allMute";
        
        #endregion
        
        
        #region --- Fields ---

        private readonly AudioSource _sfxAudioSource;
        private readonly AudioSource _mainAudioSource;
        private readonly AudioSource _mainTrack;
        
        #endregion
        
        
        #region --- Constructor ---
    
        public SoundSettings(AudioSource sfxAudioSource, AudioSource mainAudioSource, AudioSource mainTrack)
        {
            _sfxAudioSource = sfxAudioSource;
            _mainAudioSource = mainAudioSource;
            _mainTrack = mainTrack;

            Load();
        }
        
        #endregion
        
        
        #region --- Public Methods ---

        public void SetMainVolume(float value)
        {
            _mainAudioSource.volume = value;
            PlayerPrefs.SetFloat(MAIN_VOL, value);
        }

        public void SetSfxVolume(float value)
        {
            _sfxAudioSource.volume = value;
            PlayerPrefs.SetFloat(SFX_VOL, value);
        }

        public void SetMainMute(bool isMute)
        {
            _mainAudioSource.mute = isMute;
            _mainTrack.mute = isMute;
            PlayerPrefs.SetInt(MAIN_MUTE, isMute ? 1 : 0);
        }

        public void SetSfxMute(bool isMute)
        {
            _sfxAudioSource.mute = isMute;
            PlayerPrefs.SetInt(SFX_MUTE, isMute ? 1 : 0);
        }

        public void SetAllMute(bool isMute)
        {
            SetMainMute(isMute);
            SetSfxMute(isMute);
            PlayerPrefs.SetInt(ALL_MUTE, isMute ? 1 : 0);
        }
        
        public AudioSource SetMainAudioClip(AudioClip audioClip)
        {
            _mainAudioSource.clip = audioClip;
            return _mainAudioSource;
        }
        
        public AudioSource SetSfxAudioClip(AudioClip audioClip)
        {
            _sfxAudioSource.clip = audioClip;
            return _sfxAudioSource;
        }
        
        public void PlayMainAudioClip()
        {
            _mainTrack.Play();
            _mainAudioSource.Play();
        }
        
        public void PlaySfxAudioClip()
        {
            _sfxAudioSource.Play();
        }
        
        public void StopMainAudioClip()
        {
            _mainTrack.Stop();
            _mainAudioSource.Stop();
        }
        
        public void StopSfxAudioClip()
        {
            _sfxAudioSource.Stop();
        }

        public void Load()
        {
            _mainAudioSource.volume = PlayerPrefs.GetFloat(MAIN_VOL, 0.5f);
            _mainTrack.volume = PlayerPrefs.GetFloat(MAIN_VOL, 0.75f);
            _sfxAudioSource.volume = PlayerPrefs.GetFloat(SFX_VOL, 0.75f);

            bool allMute = PlayerPrefs.GetInt(ALL_MUTE, 0) == 1;
            if (allMute)
            {
                _mainAudioSource.mute = true;
                _mainTrack.mute = true;
                _sfxAudioSource.mute = true;
            }
            else
            {
                _mainAudioSource.mute = PlayerPrefs.GetInt(MAIN_MUTE, 0) == 1;
                _mainTrack.mute = PlayerPrefs.GetInt(MAIN_MUTE, 0) == 1;
                _sfxAudioSource.mute = PlayerPrefs.GetInt(SFX_MUTE, 0) == 1;
            }
        }
        
        #endregion
    }
}