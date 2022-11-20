using System;

using DG.Tweening;

using UnityEngine;


public enum MoveDirection { Right = 0, Left = 1 };

public class SphereController : MonoBehaviour
{
    public event Action OutsideGround;

    public float Speed { get => _speed; set => _speed = value; }
    [SerializeField] private float _speed;
    [SerializeField] private MoveDirection _initialDirection = MoveDirection.Right;
    [SerializeField] private Vector3 _rightMoveDirection;
    [SerializeField] private float _fallDuration = 2.5f;
    [SerializeField] private float _fallHeight = 5f;
    [SerializeField] private float _fallRange = 5f;
    [SerializeField] private LayerMask _groundCheckLayer;

    private Transform _selfTransform;
    private Vector3 _currentMoveDirecton;
    private Vector3 _initialPosition;
    

    private void Awake()
    {
        _selfTransform = GetComponent<Transform>();
        _currentMoveDirecton = _initialDirection == MoveDirection.Right ? _rightMoveDirection : _rightMoveDirection * -1;
        _initialPosition = _selfTransform.position;
    }

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _currentMoveDirecton *= -1;
        }
        
        _selfTransform.Translate(_currentMoveDirecton * _speed * Time.deltaTime, Space.Self);

        if(CheckGround() == false)
        {
            OutsideGround?.Invoke();
        }
    }

    public bool CheckGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 20f, _groundCheckLayer);
    }

    public void LaunchFallAnimation(Action actionAfter)
    {
        DOTween.Sequence()
            .SetRecyclable(true)
            .OnComplete(() => { actionAfter?.Invoke(); })
            .Insert(0f, _selfTransform.DOMoveY(-_fallHeight, _fallDuration).SetRelative(true).SetEase(Ease.InCubic))
            .Insert(0f, _selfTransform.DOMoveX(_currentMoveDirecton.x * _fallRange / 2, _fallDuration).SetRelative(true))
            .Insert(0f, _selfTransform.DOMoveZ(_currentMoveDirecton.z * _fallRange / 2, _fallDuration).SetRelative(true))
            .Play();
    }

    public void ResetState()
    {
        _currentMoveDirecton = _initialDirection == MoveDirection.Right ? _rightMoveDirection : _rightMoveDirection * -1;
        _selfTransform.position = _initialPosition;
    }
}