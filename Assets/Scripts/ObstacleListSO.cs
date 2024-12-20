using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "ObstacleListSO", menuName = "ScriptableObjects/ObstacleListSO")]
public class ObstacleListSO : ScriptableObject
{
    public List<AssetReference> items = new List<AssetReference>();
}

// [Serializable]
// public class Item
// {
//     public GameObject prefab;
// }