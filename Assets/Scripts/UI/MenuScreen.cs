using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuScreen : WindowBase
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas canvas;

    private GameState _gameState;

    [Inject]
    private void Construct(GameState gameState)
    {
        _gameState = gameState;
    }

    private void Start() => _playBtn.onClick.AddListener(OnPlayButtonPressed);

    private void OnPlayButtonPressed()
    {
        if (_gameState.CurrentState == GameStates.Menu)
        {
            _gameState.ChangeState(GameStates.Game);
            _camera.gameObject.SetActive(false);
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }
}
