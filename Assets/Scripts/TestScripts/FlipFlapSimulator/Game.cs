using System.Collections;
using System.Collections.Generic;
using System.Linq;

using DG.Tweening;

using UnityEngine;


public class Game : MonoBehaviour
{
    [SerializeField] private List<LevelInfo> _levelsInfo = new List<LevelInfo>();
    [SerializeField] private AbstractPlatform _startPlatform;
    [SerializeField] private LevelDifficulty _currentLevelDifficulty;

    [SerializeField] private float _boundsLength = 50f, _boundsHight = 2f;
    [SerializeField] private int _maxPltaformsCount = 40;

    [SerializeField, Space] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;
    [SerializeField] private LayerMask _borderCheckLayer;

    [SerializeField, Space] private PlatformsKicker _platformsKicker;
    [SerializeField] private PlatformsMover _platformsMover;
    private PlatformsPlacer _platformsPlacer;

    [SerializeField, Space] private SphereController _sphereController;

    
    #region UnityCalls

    private void Awake()
    {
        var levelInfo = _levelsInfo.FirstOrDefault(li => li.levelDifficulty == _currentLevelDifficulty);
        var platformsInfo = new PlatformsInfo(_maxPltaformsCount, levelInfo.platformPrefab);
        
        _platformsPlacer = new PlatformsPlacer(platformsInfo, _borderCheckLayer);
    }
    
    private IEnumerator Start()
    {
        CreateGameBounds(Camera.main);

        // Пропускаем один кадр, что бы посчитались границы уровня
        yield return null;

        _platformsKicker.RegisterPlatformByCollider(_startPlatform.GetComponent<Collider>(), _startPlatform);
        _platformsPlacer.PlacePlatforms(_platformsMover.transform, _startPlatform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        _platformsMover.Move();
        _sphereController.Move();
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
        _platformsPlacer.PlatformCreated += OnPlatformCreated;
        _platformsKicker.PlatformKicked += OnPlatformKicked;
        _sphereController.OutsideGround += OnGroundOutside;
    }

    private void RemoveHandlers()
    {
        _platformsPlacer.PlatformCreated -= OnPlatformCreated;
        _platformsKicker.PlatformKicked -= OnPlatformKicked;
        _sphereController.OutsideGround -= OnGroundOutside;
    }

    private void CreateGameBounds(Camera mainCamera)
    {
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

    private void RestartGame()
    {
        _sphereController.ResetState();
        _platformsPlacer.PlacePlatforms(_platformsMover.transform, _startPlatform);
        _platformsMover.transform.localPosition = Vector3.zero;

        enabled = true;
    }

    private void GameOver()
    {
        DOTween.Sequence()
            .SetRecyclable(true)
            .AppendInterval(5f)
            .AppendCallback(() => { RestartGame(); })
            .Play();
    }

    #endregion

    #region EventHandlers

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
        enabled = false;
        Debug.Log("Sphere fall!");
        
        _sphereController.LaunchFallAnimation(() => 
        {
            Debug.Log("Game over!");
            GameOver();
        });
    }

    #endregion
}