using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class AttackRange : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    [SerializeField] private TriggerObserver _triggerObserver;

    private void Start()
    {
        _attack.DisableAttack();
        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;

    }

    private void TriggerEnter(Collider collider)
    {
        _attack.EnableAttack();
    }
    private void TriggerExit(Collider collider)
    {
        _attack.DisableAttack();
    }
}
