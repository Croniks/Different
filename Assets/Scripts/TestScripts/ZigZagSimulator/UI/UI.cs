using System;

using UnityEngine;

public class UI : MonoBehaviour
{
    public event Action<LevelDifficulty> SizePlatformChanged;
    public event Action<float> SphereSpeedChanged;
    public event Action TapedToStart;
    public event Action SaveSettings;
    
    [SerializeField] private Game _game;
    [SerializeField] private UIViewSwitcher _uIViewSwitcher;
    [SerializeField] private GameUIView _gameUIView;
    [SerializeField] private MenuUIView _menuUIView;
    [SerializeField] private SaveSettingsButton _saveSettingsButton;
    [SerializeField] private TapToStartButton _tapToStartButton;
    

    private void Start()
    {
        _game.GameStart += OnGameStarted;
        _game.GameFinish += OnGameFinished;
        _game.ScoresReceived += OnScoresReceived;
        _menuUIView.SphereSpeedChanged += OnSphereSpeedChanged;
        _menuUIView.SizePlatformChanged += OnPlatformSizeChanged;
        _saveSettingsButton.SaveSettings += OnSaveSettings;
        _tapToStartButton.TapToStart += OnTappedToStart;
    }

    private void OnGameStarted()
    {
        _gameUIView.ResetScores();
    }

    private void OnGameFinished()
    {
        _uIViewSwitcher.OpenView(UIViewType.Menu);
    }

    private void OnScoresReceived(int scores)
    {
        _gameUIView.AddScores(scores);
    }

    private void OnTappedToStart()
    {
        TapedToStart?.Invoke();
    }

    private void OnSphereSpeedChanged(float value)
    {
        SphereSpeedChanged?.Invoke(value);
    }

    private void OnPlatformSizeChanged(LevelDifficulty value)
    {
        SizePlatformChanged?.Invoke(value);
    }

    private void OnSaveSettings()
    {
        SaveSettings?.Invoke();
    }
}