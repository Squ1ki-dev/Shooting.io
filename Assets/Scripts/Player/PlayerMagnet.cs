using System.Collections;
using System.Collections.Generic;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMagnet : MonoBehaviour
    {

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent<LootPiece>(out LootPiece loot))
                loot.SetTarget(transform.parent.position);
        }
    }
}

