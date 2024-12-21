using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAssets _assets;
    private MainGame _mainGame;

    public event Action PlayerCreated;
    public GameObject PlayerObject{ get; private set; }

    public GameFactory(IAssets assets)
    {
        _assets = assets;
    }

    public GameObject CreatePlayer(Vector3 initialPointPosition)
    {
        PlayerObject = InstantiateRegistred(AssetPath.PlayerPath, initialPointPosition);
        PlayerCreated?.Invoke();
        return PlayerObject;
    }

    public void CreateHud() => InstantiateRegistred(AssetPath.HudPath);

    private GameObject InstantiateRegistred(string prefabPath, Vector3 position)
    {
        GameObject gameObject = _assets.Instantiate(AssetPath.PlayerPath, position);
        return gameObject;
    }

    private GameObject InstantiateRegistred(string prefabPath)
    {
        GameObject gameObject = _assets.Instantiate(AssetPath.HudPath);
        return gameObject;
    }
}
