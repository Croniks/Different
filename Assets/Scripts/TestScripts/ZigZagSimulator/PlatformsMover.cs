using UnityEngine;


public class PlatformsMover : MonoBehaviour
{
    public float Speed { get => _speed; set => _speed = value; }
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _moveDirecton;

    private Transform _selfTransform;
    private Vector3 _initialPosition;
    private ISettingsGetter _settingsGetter;


    private void Awake()
    {
        _selfTransform = GetComponent<Transform>();
        _initialPosition = transform.position;
    }

    private void Start()
    {
        _speed = _settingsGetter.MoveSpeed;
    }
    
    public void Construct(ISettingsGetter settingsGetter)
    {
        _settingsGetter = settingsGetter;
        _settingsGetter.SettingsChanged += OnSettingsChanged;
    }

    public void Move()
    {
        _selfTransform.Translate(_moveDirecton * _speed * Time.deltaTime, Space.Self);
    }

    public void ResetState()
    {
        _selfTransform.position = _initialPosition;
    }

    private void OnSettingsChanged()
    {
        _speed = _settingsGetter.MoveSpeed;
    }
}