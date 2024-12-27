using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

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
    [SerializeField] private int _menuIndex, _upradeIndex, _finishIndex, _loseIndex;
    [SerializeField] private TMP_Text _countdownText;
    [SerializeField] private GameObject _blockerImg;

    private PanelManager _panelManager;

    public GameStates CurrentState = GameStates.None;
    public Action OnGameStateEntered;

    [Inject]
    private void Construct(PanelManager panelManager)
    {
        _panelManager = panelManager;
    }

    private void Start()
    {
        CurrentState = GameStates.Menu;

        _panelManager.OpenPanelByIndex(_menuIndex);
        //_menuScreen.SetActive(true);
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
                _panelManager.OpenPanelByIndex(_menuIndex);
                //_menuScreen.SetActive(true);
                _blockerImg.SetActive(false);
                break;

            case GameStates.Game:
                _panelManager.CloseAllPanels();
                //_menuScreen.SetActive(false);
                _blockerImg.SetActive(true);
                StartCoroutine(StartGameCountdown());
                break;

            case GameStates.Upgrade:
                _panelManager.OpenPanelByIndex(_upradeIndex);
                break;

            case GameStates.Lose:
                _panelManager.OpenPanelByIndex(_loseIndex);
                break;

            case GameStates.Finish:
                _panelManager.OpenPanelByIndex(_finishIndex);
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