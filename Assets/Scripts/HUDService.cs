using TMPro;
using UnityEngine;
using Zenject;

public class HUDService : MonoBehaviour
{
    [SerializeField] private TMP_Text waveNumberText, timerText;
    private WaveSystem _waveSystem;

    [Inject]
    public void Construct(WaveSystem waveSystem)
    {
        _waveSystem = waveSystem;
    }

    private void Start()
    {
        if (_waveSystem == null) Debug.LogError("WaveSystem is not injected!");
        waveNumberText.text = _waveSystem.numberOfWave.ToString();
    }

    private void Update()
    {
        // Update the timerText if needed (e.g., to show a countdown or timer)
    }
}
