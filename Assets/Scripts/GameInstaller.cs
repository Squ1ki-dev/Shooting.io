using System;
using UnityEngine;
using Zenject;
using CodeBase.UI.Elements;
using CodeBase.Wave;

public class GameInstaller : MonoInstaller
{
    public GameState _game;
    public WaveInitializer gameInitializerPrefab;
    public HUDService _hudService;
    public WaveSystem _waveSystem;
    public PanelManager _panelManager;

    public override void InstallBindings()
    {
        BindGame();
        BindHUD();
        BindWave();
        BindUI();
    }

    private void BindWave()
    {
        Container
            .Bind<WaveSystem>()
            .FromInstance(_waveSystem)
            .AsSingle();
    }

    private void BindUI()
    {
        Container
            .Bind<PanelManager>()
            .FromInstance(_panelManager)
            .AsSingle();
    }

    private void BindHUD()
    {
        Container
            .Bind<HUDService>()
            .FromComponentInNewPrefab(_hudService)
            .AsSingle();
    }

    private void BindGame()
    {
        Container
            .Bind<GameState>()
            .FromInstance(_game)
            .AsSingle();

        Container
            .Bind<WaveInitializer>()
            .FromInstance(gameInitializerPrefab)
            .AsSingle();
    }
}
