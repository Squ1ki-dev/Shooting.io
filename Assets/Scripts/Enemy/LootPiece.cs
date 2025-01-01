using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        private bool _picked;
        [SerializeField] private EnemySO enemySO;
        public static event Action<int> OnLootPicked;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterController>())
                Pickup();
        }
        
        private void Pickup()
        {
            if (_picked)
                return;

            _picked = true;
            OnLootPicked?.Invoke(enemySO.XPValue);
            Destroy(gameObject);
        }
    }
}