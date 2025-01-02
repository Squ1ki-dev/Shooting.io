using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using CandyCoded.HapticFeedback;
using CodeBase.UI.Audio;

namespace CodeBase.Service
{
    public class SoundSettingService : MonoBehaviour
    {
        [SerializeField] private SoundSettingsSO _soundSettings;
        [SerializeField] private AudioMixer _audioMixer;

        [SerializeField] private Toggle musicToggle, sfxToggle, vibroToggle;


        private void Start()
        {
            musicToggle.isOn = _soundSettings.IsMusicEnabled;
            sfxToggle.isOn = _soundSettings.IsSFXEnabled;

            musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
            sfxToggle.onValueChanged.AddListener(OnSFXToggleChanged);
            vibroToggle.onValueChanged.AddListener(OnVibroToggleChanged);
        }

        private void OnMusicToggleChanged(bool isOn)
        {
            _soundSettings.IsMusicEnabled = isOn;
            _audioMixer.SetFloat(Constants.MusicVolumeParameter, isOn ? _soundSettings.MusicVolume : -80f);
        }

        private void OnSFXToggleChanged(bool isOn)
        {
            _soundSettings.IsSFXEnabled = isOn;
            _audioMixer.SetFloat(Constants.SFXVolumeParameter, isOn ? _soundSettings.SfxVolume : -80f);
        }

        private void OnVibroToggleChanged(bool isOn)
        {
            _soundSettings.IsVibrationEnabled = isOn;
            PlayerPrefs.SetInt(Constants.VibrationParameter, isOn ? 1 : 0);

            if (PlayerPrefs.GetInt(Constants.VibrationParameter) == 1)
                HapticFeedback.MediumFeedback();
        }
    }
}