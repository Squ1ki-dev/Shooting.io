using System.Collections.Generic;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private LoadingCurtain Curtain;
    private Game _game;

    public void Run()
    {
        _game = new Game(this, Curtain);
        _game.StateMachine.Enter<BootstrapState>();
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);
    }
}
