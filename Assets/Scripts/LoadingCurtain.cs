using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingCurtain : MonoBehaviour
{
    public CanvasGroup Curtain;
    private readonly IGameFactory _gameFactory;

    private void Awake() => DontDestroyOnLoad(this);

    public void Show()
    {
        gameObject.SetActive(true);
        Curtain.alpha = 1;
    }

    public void Hide() => FadeIn();

    private void FadeIn()
    {
        Curtain.DOFade(0f, 0.6f)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
