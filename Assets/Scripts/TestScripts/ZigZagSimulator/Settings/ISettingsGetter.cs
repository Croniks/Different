using System;

public interface ISettingsGetter 
{
    public event Action SettingsChanged;

    public float MoveSpeed { get; }
    public PlatformSize PlatformSize { get; }
    public ReusablePlatform PlatformPrefab { get; }
    public float BoundsLength { get; }
    public float BoundsHeight { get; }
    public int MaxPltaformsCount { get; }
}
