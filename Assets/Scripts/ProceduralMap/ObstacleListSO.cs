using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.ProceduralMap
{
    [CreateAssetMenu(fileName = "ObstacleListSO", menuName = "ScriptableObjects/ObstacleListSO")]
    public class ObstacleListSO : ScriptableObject
    {
        public List<AssetReference> items = new List<AssetReference>();
    }
}
