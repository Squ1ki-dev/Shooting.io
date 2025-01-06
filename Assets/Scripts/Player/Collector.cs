using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Service;

namespace CodeBase.Player
{
    public class Collector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            ICollectible collectable = other.GetComponent<ICollectible>();
            
            if (collectable != null)
                collectable.Collect();
        }
    }
}