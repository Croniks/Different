using System;
using System.Collections.Generic;

using UnityEngine;


public class UIToggleGroup : MonoBehaviour
{
    public event Action<LevelDifficulty> PlatformSizeSettingChanged;

    [SerializeField] private List<UIToggle> _toggles;

    
    private void Start()
    {
        _toggles.ForEach(t => 
        {
            t.IsOn = t.LevelDifficulty == LevelDifficulty.Hard;
            t.ToggleOn += OnToggle; 
        });
    }

    public void SetPlatformSize(LevelDifficulty levelDifficulty)
    {
        _toggles.ForEach(t =>
        {
            t.IsOn = t.LevelDifficulty == levelDifficulty;
        });
    }

    private void OnToggle(LevelDifficulty levelDifficulty)
    {
        PlatformSizeSettingChanged?.Invoke(levelDifficulty);
    }
}