using System;
using UnityEngine;

public enum LevelDifficulty { Easy = 0, Medium = 1, Hard = 2 };

[Serializable]
public class LevelInfo
{
    public LevelDifficulty levelDifficulty;
    public ReusablePlatform platformPrefab;
}

public class BoundsInfo
{
    public BoxCollider boundsCollider;
    public float boundsLength;
    public float boundsHight;

    public BoundsInfo(BoxCollider boundsCollider, float boundsLength, float boundsHight)
    {
        this.boundsCollider = boundsCollider;
        this.boundsLength = boundsLength;
        this.boundsHight = boundsHight;
    }
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