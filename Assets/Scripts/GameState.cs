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
    [SerializeField] private int _upradeIndex, _upradeFinishIndex, _finishIndex, _loseIndex;
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
        IsBlockerActive(false);
    }

    public void ChangeState(GameStates newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameStates.Game:
                _panelManager.CloseAllPanels();
                break;

            case GameStates.Upgrade:
                _panelManager.OpenPanelByIndex(_upradeIndex);
                break;

            case GameStates.Lose:
                _panelManager.OpenPanelByIndex(_loseIndex);
                break;

            case GameStates.Finish:
                _panelManager.OpenPanelByIndex(_upradeFinishIndex);
                break;
        }
    }

    private void IsBlockerActive(bool isActive) => _blockerImg.SetActive(isActive);

    private IEnumerator StartGameCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            IsBlockerActive(true);
            _countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        _countdownText.text = "GO!";
        CurrentState = GameStates.Game;
        yield return new WaitForSeconds(1f);
        _countdownText.text = string.Empty;
        IsBlockerActive(false);
    }
}