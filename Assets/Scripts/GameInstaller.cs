using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public MainGame _game;
    public GameInitializer gameInitializerPrefab;
    public HUDService _hudService;
    public WaveSystem _waveSystem; 

    public override void InstallBindings()
    {
        BindGame();
        BindHUD();
        BindWave();
    }

    private void BindWave()
    {
        Container
            .Bind<WaveSystem>()
            .FromInstance(_waveSystem)
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
            .Bind<MainGame>()
            .FromInstance(_game)
            .AsSingle();

        Container
            .Bind<GameInitializer>()
            .FromInstance(gameInitializerPrefab)
            .AsSingle();
    }
}
