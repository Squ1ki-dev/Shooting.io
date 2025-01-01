using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using UnityEngine;

public class PlayerCameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private float _shakeIntensity;
    [SerializeField] private float _shakeTime;

    private float _timer;
    private CinemachineBasicMultiChannelPerlin _cbmp;

    private void Awake()
    {
        _cbmp = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StopShake();
    }

    private void Update() => UpdateShakingTimer();

    private void UpdateShakingTimer()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;

            if(_timer <= 0)
                StopShake();
        }
    }

    public void StartShake()
    {
        _cbmp.m_AmplitudeGain = _shakeIntensity;
        _timer = _shakeTime;
    }

    private void StopShake()
    {
        _cbmp.m_AmplitudeGain = 0f;
        _timer = 0;
    }
}
