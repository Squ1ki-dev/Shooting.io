using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private EnemySO enemySO;
    
    public abstract void Attack();
}
