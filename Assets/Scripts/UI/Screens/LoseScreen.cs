using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScreen : WindowBase
{
    private const string Init = "Init";
    [SerializeField] private Button _restartBtn, _exitBtn;
    private void Start() 
    {
        _restartBtn.onClick.AddListener(Restart);
        _exitBtn.onClick.AddListener(Restart);
    }

    private void Restart() => SceneManager.LoadScene(Init);
}
