using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBar : MonoBehaviour
{
    private int _currentXP;
    private GameState _mainGame;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Image _imageCurrent;
    [SerializeField] private int _targetXP = 100;
    [SerializeField] private PlayerStatsSO playerConfig;

    private void Start()
    {
        _mainGame = FindAnyObjectByType<GameState>();

        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", playerConfig.Level);
            PlayerPrefs.Save();
        }

        _levelText.text = "Level: " + PlayerPrefs.GetInt("Level");

        // Load fillAmount
        if (PlayerPrefs.HasKey("FillAmount"))
        {
            _imageCurrent.fillAmount = PlayerPrefs.GetFloat("FillAmount");
            _currentXP = Mathf.RoundToInt(_imageCurrent.fillAmount * _targetXP);
        }
        else
        {
            _imageCurrent.fillAmount = 0; // Default value
            _currentXP = 0;
        }

        Debug.Log("Level " + PlayerPrefs.GetInt("Level"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _currentXP += 12;

        SetValue(_currentXP, _targetXP);
    }

    public void SetValue(float current, float target)
    {
        _levelText.text = "Level: " + playerConfig.Level.ToString();
        _imageCurrent.fillAmount = current / target;

        PlayerPrefs.SetFloat("FillAmount", _imageCurrent.fillAmount);
        PlayerPrefs.Save();

        if (_currentXP >= _targetXP)
        {
            _currentXP -= _targetXP;
            playerConfig.Level++;
            PlayerPrefs.SetInt("Level", playerConfig.Level);
            Debug.Log($"Level increased to: {playerConfig.Level}");
            _mainGame.ChangeState(GameStates.Upgrade);
            _targetXP += 50;
        }
    }
}