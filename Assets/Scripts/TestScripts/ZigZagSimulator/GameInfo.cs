using System;
using UnityEngine;

public enum LevelDifficulty { Easy = 0, Medium = 1, Hard = 2 };
public enum UIViewType { Menu, MenuButtons, MenuSettings, Preparation, Game };

[Serializable]
public struct PointsInfo
{
    public Vector3 firsPoint;
    public Vector3 secondPoint;
}

[Serializable]
public class LevelInfo
{
    public LevelDifficulty levelDifficulty;
    public ReusablePlatform platformPrefab;
}

public class PlatformsInfo
{
    public int maxPlatformsCount;
    public ReusablePlatform platformPrefab;

    public PlatformsInfo(int maxPlatformsCount, ReusablePlatform platformPrefab)
    {
        this.maxPlatformsCount = maxPlatformsCount;
        this.platformPrefab = platformPrefab;
    }
}