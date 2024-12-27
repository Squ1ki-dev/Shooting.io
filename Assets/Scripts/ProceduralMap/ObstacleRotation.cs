using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.ProceduralMap
{
    public class ObstacleRotation : MonoBehaviour
    {
        private void Start() => RandomiseRotation();

        private void RandomiseRotation()
        {
            Quaternion randRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            transform.rotation = randRotation;
        }
    }
}