using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameStates
{
    None,
    Menu,
    Game,
    Lose,
    Finish,
    Upgrade
}

public class GameState : MonoBehaviour
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private GameObject _menuScreen, _upradeScreen, _finishScreen, _loseScreen;
    [SerializeField] private TMP_Text _countdownText;
    [SerializeField] private GameObject _blockerImg;

    public GameStates CurrentState = GameStates.None;
    public Action OnGameStateEntered;

    private void Start()
    {
        CurrentState = GameStates.Menu;

        _menuScreen.SetActive(true);
        _blockerImg.SetActive(false);

        _playBtn.onClick.AddListener(OnPlayButtonPressed);
    }

    private void OnPlayButtonPressed()
    {
        if (CurrentState == GameStates.Menu)
            ChangeState(GameStates.Game);
    }

    public void ChangeState(GameStates newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameStates.Menu:
                _menuScreen.SetActive(true);
                _blockerImg.SetActive(false);
                break;

            case GameStates.Game:
                _menuScreen.SetActive(false);
                _blockerImg.SetActive(true);
                StartCoroutine(StartGameCountdown());
                break;

            case GameStates.Lose:
                _finishScreen.SetActive(true);
                break;

            case GameStates.Finish:
                _finishScreen.SetActive(true);
                break;

            case GameStates.Upgrade:
                _upradeScreen.SetActive(true);
                break;
        }
    }

    private IEnumerator StartGameCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            _blockerImg.SetActive(true);
            _countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        _countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        _countdownText.text = "";
        _blockerImg.SetActive(false);

        OnGameStateEntered?.Invoke();
    }
}