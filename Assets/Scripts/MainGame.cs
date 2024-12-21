using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum State
{
    None,
    Menu,
    Game,
    Lose,
    Finish,
    Upgrade
}

public class MainGame : MonoBehaviour
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private TMP_Text _countdownText;
    [SerializeField] private GameObject _blockerImg;

    public State CurrentState = State.None;
    public Action OnGameStateEntered;

    private void Start()
    {
        CurrentState = State.Menu;

        // Set up initial UI state
        _menuScreen.SetActive(true);
        _blockerImg.SetActive(false);

        // Subscribe to play button click
        _playBtn.onClick.AddListener(OnPlayButtonPressed);
    }

    private void OnPlayButtonPressed()
    {
        if (CurrentState == State.Menu)
        {
            ChangeState(State.Game);
        }
    }

    private void ChangeState(State newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case State.Menu:
                _menuScreen.SetActive(true);
                _blockerImg.SetActive(false);
                break;

            case State.Game:
                _menuScreen.SetActive(false);
                _blockerImg.SetActive(true);
                StartCoroutine(StartGameCountdown());
                break;

            case State.Lose:
                // Handle Lose state here
                break;

            case State.Finish:
                // Handle Finish state here
                break;

            case State.Upgrade:
                // Handle Upgrade state here
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