using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class HUDService : MonoBehaviour
    {
        [SerializeField] private TMP_Text waveNumberText;

        private void Start()
        {
            waveNumberText.text = "Wave: " + PlayerPrefs.GetInt("WaveNumber").ToString();
        }
    }
}
