using System;
using UnityEngine;

public interface IGameFactory : IService
{
    GameObject CreatePlayer(Vector3 initialPointPosition);
    GameObject PlayerObject { get; }

    event Action PlayerCreated;
    void CreateHud();
}