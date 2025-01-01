using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public enum GameStates
{
    None,
    Game,
    Lose,
    Finish,
    Upgrade
}

public class GameState : MonoBehaviour
{
    [SerializeField] private int _upradeIndex, _finishIndex, _loseIndex;
    [SerializeField] private TMP_Text _countdownText;
    [SerializeField] private GameObject _blockerImg;

    private PanelManager _panelManager;
    public GameStates CurrentState = GameStates.None;

    [Inject]
    private void Construct(PanelManager panelManager)
    {
        _panelManager = panelManager;
    }

    private void Start()
    {
        StartCoroutine(StartGameCountdown());
        //_blockerImg.SetActive(false);
        IsBlockerActive(false);
    }

    public void ChangeState(GameStates newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameStates.Game:
                _panelManager.CloseAllPanels();
                IsBlockerActive(true);
                //_blockerImg.SetActive(true);
                break;

            case GameStates.Upgrade:
                IsBlockerActive(false);
                _panelManager.OpenPanelByIndex(_upradeIndex);
                break;

            case GameStates.Lose:
                IsBlockerActive(false);
                _panelManager.OpenPanelByIndex(_loseIndex);
                break;

            case GameStates.Finish:
                IsBlockerActive(false);
                _panelManager.OpenPanelByIndex(_finishIndex);
                break;
        }
    }

    private IEnumerator StartGameCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            IsBlockerActive(true);
            _blockerImg.SetActive(true);
            _countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        _countdownText.text = "GO!";
        CurrentState = GameStates.Game;
        yield return new WaitForSeconds(1f);
        _countdownText.text = "";
        IsBlockerActive(false);
        //_blockerImg.SetActive(false);
    }

    private void IsBlockerActive(bool isActive)
    {
        _blockerImg.SetActive(isActive);
    }
}