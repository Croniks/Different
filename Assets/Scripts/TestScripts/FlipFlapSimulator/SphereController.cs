using UnityEngine;


public enum MoveDirection { Right = 0, Left = 1 };

public class SphereController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MoveDirection _initialDirection = MoveDirection.Right;
    [SerializeField] private Vector3 _rightMoveDirection;

    private Transform _selfTransform;
    private Vector3 _currentMoveDirecton;
    

    private void Awake()
    {
        _selfTransform = GetComponent<Transform>();
        _currentMoveDirecton = _initialDirection == MoveDirection.Right ? _rightMoveDirection : _rightMoveDirection * -1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _currentMoveDirecton *= -1;
        }

        _selfTransform.Translate(_currentMoveDirecton * _speed * Time.deltaTime, Space.Self);
    }
}