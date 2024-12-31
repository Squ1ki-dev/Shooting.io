using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.ProceduralMap
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [SerializeField] private Transform parentObject;
        [SerializeField] private ObstacleListSO obstacleListSO;
        [SerializeField] private int numItemsToSpawn;

        [SerializeField] private float itemXSpread, itemZSpread;
        private int itemsSpawned = 0;

        private MeshCombiner _meshCombiner;
        private void Awake()
        {
            _meshCombiner = parentObject.gameObject.AddComponent<MeshCombiner>();
            for (int i = 0; i < numItemsToSpawn; i++)
            {
                PickItem();
            }
        }

        private void PickItem()
        {
            int randomIndex = Random.Range(0, obstacleListSO.items.Count);

            AssetReference assetReference = obstacleListSO.items[randomIndex];
            assetReference.InstantiateAsync(SpreadItems(), Quaternion.identity, parentObject).Completed += OnItemInstantiated;
        }

        private void OnItemInstantiated(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError("Failed to load item.");
                return;
            }

            itemsSpawned++;

            if (itemsSpawned == numItemsToSpawn)
                CombineMeshes();
        }

        private void CombineMeshes()
        {
            _meshCombiner.CreateMultiMaterialMesh = true;
            _meshCombiner.DestroyCombinedChildren = true;
            _meshCombiner.CombineMeshes(false);
        }

        private Vector3 SpreadItems() =>
            new Vector3(Random.Range(-itemXSpread, itemXSpread), 0, Random.Range(-itemZSpread, itemZSpread)) + transform.position;
    }
}
