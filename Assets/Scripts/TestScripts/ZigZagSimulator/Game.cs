using System;
using System.Collections;

using DG.Tweening;

using UnityEngine;


public class Game : MonoBehaviour
{
    public event Action GameStart;
    public event Action GameFinish;
    public event Action<int> ScoresReceived;

    [SerializeField] private UI _ui;
    [SerializeField] private SettingsObject _settings;
    
    [SerializeField] private AbstractPlatform _startPlatform;

    [SerializeField, Space] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;
    [SerializeField] private LayerMask _borderCheckLayer;
    private float _boundsHight;
    private float _boundsLength;

    [SerializeField, Space] private PlatformsKicker _platformsKicker;
    [SerializeField] private PlatformsMover _platformsMover;
    private PlatformsPlacer _platformsPlacer;

    [SerializeField, Space] private SphereController _sphereController;
    
    private float _currentSphereSpeed = 1f;
    private LevelDifficulty _currentLevelDifficulty;
    private bool _playGame = false;


    #region UnityCalls

    private void Awake()
    {
        _settings.LoadSettings();
    }
    
    private IEnumerator Start()
    {
        CreateGameBounds(Camera.main);

        // Пропускаем один кадр, что бы посчитались границы уровня
        yield return null;

        Setup();
    }

    private void Update()
    {
        if(_playGame == true)
        {
            _platformsMover.Move();
            _sphereController.Move();
        }
    }

    private void OnEnable()
    {
        AddHandlers();
    }

    private void OnDisable()
    {
        RemoveHandlers();
    }

    #endregion
    
    #region PrivateMethods

    private void AddHandlers()
    {
        _ui.SizePlatformChanged += OnPlatformSizeChanged;
        _ui.SphereSpeedChanged += OnSphereSpeedChanged;
        _ui.TapedToStart += OnTapToStart;
        _ui.SaveSettings += OnSaveSettings;
        _platformsKicker.PlatformKicked += OnPlatformKicked;
        _sphereController.OutsideGround += OnGroundOutside;
    }

    private void RemoveHandlers()
    {
        _ui.SizePlatformChanged -= OnPlatformSizeChanged;
        _ui.SphereSpeedChanged -= OnSphereSpeedChanged;
        _ui.TapedToStart -= OnTapToStart;
        _ui.SaveSettings -= OnSaveSettings;
        _platformsKicker.PlatformKicked -= OnPlatformKicked;
        _sphereController.OutsideGround -= OnGroundOutside;
    }

    private void Setup()
    {
        _sphereController.Speed = _platformsMover.Speed = _settings.spehereSpeed;
        _sphereController.ResetState();

        _platformsMover.transform.localPosition = Vector3.zero;

        if (_platformsPlacer != null)
        {
            _platformsPlacer.PlatformCreated -= OnPlatformCreated;
            _platformsPlacer.ResetPlatforms();
        }

        var platformsInfo = new PlatformsInfo(_settings.MaxPltaformsCount, _settings.PlatformPrefab);
        _platformsPlacer = new PlatformsPlacer(platformsInfo, _borderCheckLayer);
        _platformsPlacer.PlatformCreated += OnPlatformCreated;
        
        _platformsKicker.ResetPlatforms();
        _platformsKicker.RegisterPlatformByCollider(_startPlatform.GetComponent<Collider>(), _startPlatform);
        _platformsPlacer.PlacePlatforms(_platformsMover.transform, _startPlatform);
    }

    private void CreateGameBounds(Camera mainCamera)
    {
        _boundsHight = _settings.BoundsHight;
        _boundsLength = _settings.BoundsLength;

        float zDistance = Vector3.Distance(mainCamera.transform.position, _startPlatform.transform.position);
        Vector3 leftBoundPosition = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, zDistance));
        Vector3 rightBoundPosition = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, zDistance));

        Vector3 localScale = new Vector3(1, _boundsHight, _boundsLength);
        Vector3 xPositionOffest = new Vector3(localScale.x / 1.5f, 0f, 0f);

        leftBoundPosition = leftBoundPosition - xPositionOffest;
        rightBoundPosition = rightBoundPosition + xPositionOffest;

        _leftBound.position = leftBoundPosition;
        _rightBound.position = rightBoundPosition;

        _leftBound.localScale = _rightBound.localScale = localScale;

        Vector3 eulerAngles = new Vector3(0f, mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.z);

        _leftBound.eulerAngles = _rightBound.eulerAngles = eulerAngles;
    }

    private void GameOver()
    {
        DOTween.Sequence()
            .SetRecyclable(true)
            .AppendInterval(5f)
            .AppendCallback(() => { Setup(); })
            .OnComplete(() => { GameFinish?.Invoke(); })
            .Play();
    }

    #endregion

    #region EventHandlers

    private void OnTapToStart()
    {
        _playGame = true;
        GameStart?.Invoke();
    }

    private void OnSphereSpeedChanged(float value)
    {
        _settings.spehereSpeed = value;
    }

    private void OnPlatformSizeChanged(LevelDifficulty value)
    {
        _settings.levelDifficulty = value;
    }

    private void OnSaveSettings()
    {
        bool settignsWasChanged = false;

        if(_currentSphereSpeed != _settings.spehereSpeed)
        {
            _currentSphereSpeed = _settings.spehereSpeed;
            settignsWasChanged = true;
        }

        if(_currentLevelDifficulty != _settings.levelDifficulty)
        {
            _currentLevelDifficulty = _settings.levelDifficulty;
            settignsWasChanged = true;
        }

        if(settignsWasChanged == true)
        {
            _settings.SaveSettings();
            Setup();
        }
    }

    private void OnPlatformCreated(ReusablePlatform platform)
    {
        _platformsKicker.RegisterPlatformByCollider(platform.GetComponent<Collider>(), platform);
    }

    private void OnPlatformKicked(AbstractPlatform platform)
    {
        if (platform is ReusablePlatform reusablePlatform)
        {
            _platformsPlacer.ReplacePlatform(reusablePlatform);
        }
    }

    private void OnGroundOutside()
    {
        _playGame = false;
        
        _sphereController.LaunchFallAnimation(() => 
        {
            GameOver();
        });
    }

    #endregion
}