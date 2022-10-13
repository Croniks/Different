using System;
using UnityEngine;

public enum LevelDifficulty { Easy = 0, Medium = 1, Hard = 2 };

[Serializable]
public class LevelInfo
{
    public LevelDifficulty levelDifficulty;
    public Platform platformPrefab;
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
    public Platform startPlatform;
    public Vector3 startPlatformPosition;
    public Platform platformPrefab;

    public PlatformsInfo(int maxPlatformsCount, Platform startPlatform, Vector3 initialPoint, Platform platformPrefab)
    {
        this.maxPlatformsCount = maxPlatformsCount;
        this.startPlatform = startPlatform;
        this.startPlatformPosition = initialPoint;
        this.platformPrefab = platformPrefab;
    }
}