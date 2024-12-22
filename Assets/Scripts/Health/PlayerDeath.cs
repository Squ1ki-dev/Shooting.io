using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMove playerMove;
    private GameState gameState;
    //[SerializeField] private GameObject deathFx;
    private bool _isDead;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
        if (gameState == null)
            Debug.LogError("GameState not found in the scene!");

        playerHealth.HealthChanged += HealthChanged;
    }
    
    private void OnDestroy()
    {
        playerHealth.HealthChanged -= HealthChanged;
    }

    private void HealthChanged()
    {
        if(!_isDead && playerHealth.Current <= 0)
            Die();
    }

    private void Die()
    {
        _isDead = true;
        playerMove.enabled = false;
        gameState.ChangeState(GameStates.Lose);
        //Instantiate(deathFx, transform.position, Quaternion.identity);
    }
}
