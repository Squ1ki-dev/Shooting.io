using System;
using UnityEngine;
using CodeBase.Service;

public class BootstrapState : IState
{
    private const string Init = "Init";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {
        _sceneLoader.Load(Init, onLoaded: EnterLoadLevel);
    }

    public void Exit() {}

    private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>("Main");

    private void RegisterServices()
    {
        _services.RegisterSingle<IInputService>(InputService());
        _services.RegisterSingle<IAssets>(new AssetProvider());
        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
    }

    private static IInputService InputService()
    {
        if(Application.isEditor)
            return new StandaloneInputService();
        else
            return new MobileInputService();
    }
}