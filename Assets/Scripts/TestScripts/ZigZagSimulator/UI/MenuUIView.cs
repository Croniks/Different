using System;

using UnityEngine;


public class MenuUIView : UIView
{
    public event Action<LevelDifficulty> SizePlatformChanged;
    public event Action<float> SphereSpeedChanged;

    [SerializeField] private UIScrollBar _scrollBar;
    [SerializeField] private UIToggleGroup _toggleGroup;


    private void Start()
    {
        _scrollBar.SphereSpeedSettingChanged += OnSphereSpeedSettingChanged;
        _toggleGroup.PlatformSizeSettingChanged += OnPlatformSizeSettingChanged;
    }
    
    private void OnSphereSpeedSettingChanged(float value)
    {
        SphereSpeedChanged?.Invoke(value);
    }

    private void OnPlatformSizeSettingChanged(LevelDifficulty value)
    {
        SizePlatformChanged?.Invoke(value);
    }
}