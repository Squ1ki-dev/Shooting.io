using UnityEngine;

namespace CodeBase.UI.Audio
{
    [CreateAssetMenu(fileName = "SoundSettingSO")]
    public class SoundSettingsSO : ScriptableObject
    {
        public bool IsMusicEnabled;
        public bool IsSFXEnabled;
        public bool IsVibrationEnabled;
         // 0 for full volume, -80 for mute
        public float MusicVolume;
        public float SfxVolume;
    }
}
