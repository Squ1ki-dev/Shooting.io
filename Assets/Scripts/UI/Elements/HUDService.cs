using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class HUDService : MonoBehaviour
    {
        [SerializeField] private TMP_Text waveNumberText;
        [SerializeField] private GameObject gameObjectToDisable;
        private GameState _gameState;

        private void Start()
        {
            _gameState = FindObjectOfType<GameState>();
            waveNumberText.text = "Wave: " + PlayerPrefs.GetInt("WaveNumber").ToString();

            if(_gameState.CurrentState == GameStates.Menu)
                gameObjectToDisable.SetActive(false);
            else
                gameObjectToDisable.SetActive(true);
        }

        private void Update()
        {
            if(_gameState.CurrentState == GameStates.Game)
                gameObjectToDisable.SetActive(true);
            else
                gameObjectToDisable.SetActive(false);
        }
    }
}
