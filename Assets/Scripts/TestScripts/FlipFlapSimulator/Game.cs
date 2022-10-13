using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


public class Game : MonoBehaviour
{
    [SerializeField] private List<LevelInfo> _levelsInfo = new List<LevelInfo>();
    [SerializeField] private Platform _startPlatform;
    [SerializeField] private Transform _initialPointTransform;
    [SerializeField] private LevelDifficulty _currentLevelDifficulty;

    [SerializeField] private BoxCollider _gameBoundsCollider;
    [SerializeField] private float _boundsLength = 50f, _boundsHight = 2f;
    [SerializeField] private int _maxPltaformsCount = 40;

    [SerializeField] private PlatformsKicker _platformsKicker;

    private PlatformsSetter _platformsSetter;


    private void Awake()
    {
        var levelInfo = _levelsInfo.FirstOrDefault(li => li.levelDifficulty == _currentLevelDifficulty);
        var platformsInfo = new PlatformsInfo(_maxPltaformsCount, _startPlatform, _initialPointTransform.position, levelInfo.platformPrefab);
        var boundsInfo = new BoundsInfo(_gameBoundsCollider, _boundsLength, _boundsHight);
        
        _platformsSetter = new PlatformsSetter(boundsInfo, platformsInfo);

        //_platformsSetter.CreatePlatforms();
        //_platformsSetter.SetPlatforms();
    }

    private void OnEnable()
    {
        AddHandlers();
    }

    private void OnDisable()
    {
        RemoveHandlers();
    }

    private void AddHandlers()
    {
        _platformsSetter.PlatformCreated += OnPlatformCreated;
        _platformsKicker.PlatformKicked += OnPlatformKicked;
    }

    private void RemoveHandlers()
    {
        _platformsSetter.PlatformCreated -= OnPlatformCreated;
        _platformsKicker.PlatformKicked -= OnPlatformKicked;
    }

    private void OnPlatformCreated(Platform platform)
    {
        _platformsKicker.RegisterByCollider(platform.GetComponent<Collider>(), platform);
    }

    private void OnPlatformKicked(Platform platform)
    {
        _platformsSetter.SetPlatformAfterKicking(platform);
    }
}