using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private EnemyDeath _enemyDeath;
    [SerializeField] private GameObject lootObj;
    private IGameFactory _factory;

    public void Construct(IGameFactory factory)
    {
        _factory = factory;
    }

    private void Start() => _enemyDeath.Happened += SpawnLoot;

    private void SpawnLoot()
    {
        GameObject loot = ObjectPool.SpawnObject(lootObj, transform.position, Quaternion.identity);;
        loot.transform.position = transform.position;
    }
}
