using System;
using System.Collections;
using CodeBase.Player;
using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private PlayerStatsSO _playerConfig;
    [SerializeField] private PlayerCameraShake _cameraShake;
    [SerializeField] private PlayerAnimator _playerAnimator;
    private float _current;
    private Coroutine _regenerationCoroutine;
    public event Action HealthChanged;

    public float Current
    {
        get => _current;
        set
        {
            _current = Mathf.Clamp(value, 0, Max);
            HealthChanged?.Invoke();
        }
    }

    public float Max
    {
        get => _playerConfig.MaxHP;
        set => _playerConfig.MaxHP = value;
    }

    private void Awake() => _current = _playerConfig.MaxHP;

    private void OnEnable()
    {
        if (_regenerationCoroutine == null)
            _regenerationCoroutine = StartCoroutine(RegenerateHealth());
    }

    private void OnDisable()
    {
        if (_regenerationCoroutine != null)
        {
            StopCoroutine(_regenerationCoroutine);
            _regenerationCoroutine = null;
        }
    }

    public void TakeDamage(float damage)
    {
        if (Current <= 0)
            return;

        Current -= damage;
        _playerAnimator.PlayHit();
        _damageText.text = $"-{(int)damage}";
        
        _cameraShake.StartShake();
        RepresentDamage();

        HealthChanged?.Invoke();
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            if (Current < Max && _playerConfig.RegenerationSpeed > 0)
            {
                Current += _playerConfig.RegenerationSpeed * Time.deltaTime;
                HealthChanged?.Invoke();
            }
            yield return null;
        }
    }

    private void RepresentDamage()
    {
        _damageText.color = Color.red;
        _damageText.transform.localScale = Vector3.one;
        _damageText.alpha = 1f;

        Vector3 initialPosition = _damageText.transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(0, 1, 0); // Move up in world space

        _damageText.transform.DOMove(targetPosition, 0.5f)
            .SetEase(Ease.OutQuad);

        _damageText.transform.DOScale(1.5f, 0.2f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                _damageText.transform.DOScale(1f, 0.3f);
            });
            
        _damageText.DOFade(0, 0.5f)
            .SetDelay(0.5f)
            .OnComplete(() =>
            {
                _damageText.text = string.Empty;
            });
    }
}
