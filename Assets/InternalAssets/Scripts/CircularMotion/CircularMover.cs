using UnityEngine;


public class CircularMover : MonoBehaviour
{
    [SerializeField] private Transform _pointAroundWhichToMove;
    [SerializeField] private float _radius;
    [SerializeField] private float _linearMoveSpeed = 5f;
    [SerializeField] private bool _moveClockwise = false;

    private float _directionToNewPostionModule = 0f;
    private Vector3 _aroundPointPosition = Vector3.zero;
    
    private void Start()
    {
        _aroundPointPosition = _pointAroundWhichToMove.position;
        _aroundPointPosition.y = transform.position.y;

         Vector3 directionFromCenterToPostion = (transform.position - _aroundPointPosition).normalized;
        transform.position = _aroundPointPosition + directionFromCenterToPostion * _radius;

        directionFromCenterToPostion = GetNormalizeDirectionFromCenter(transform.position, _aroundPointPosition, _radius);
        Vector3 normalizeMoveDirection = GetNormalizeLinearMoveDirection(directionFromCenterToPostion);

        normalizeMoveDirection *= _linearMoveSpeed * Time.fixedDeltaTime;
        normalizeMoveDirection += transform.position;

        Vector3 directionFromCenterToNewPostion = normalizeMoveDirection - _aroundPointPosition;
        _directionToNewPostionModule = directionFromCenterToNewPostion.magnitude;
    }

    private void FixedUpdate()
    {
        _aroundPointPosition.y = transform.position.y;

        Vector3 normalizeDirectionFromCenter = GetNormalizeDirectionFromCenter(transform.position, _aroundPointPosition, _radius);
        Vector3 normalizeMoveDirection = GetNormalizeLinearMoveDirection(normalizeDirectionFromCenter, _moveClockwise);

        normalizeMoveDirection *= _linearMoveSpeed * Time.fixedDeltaTime;
        normalizeMoveDirection += transform.position;

        Vector3 directionFromCenterToNewPostion = normalizeMoveDirection - _aroundPointPosition;

        //directionFromCenterToNewPostion = new Vector3(
        //    directionFromCenterToNewPostion.x / _directionToNewPostionModule,
        //    directionFromCenterToNewPostion.y / _directionToNewPostionModule,
        //    directionFromCenterToNewPostion.z / _directionToNewPostionModule
        //);
        directionFromCenterToNewPostion.Normalize();

        directionFromCenterToNewPostion *= _radius;

        Vector3 newPositionOnCircle = _aroundPointPosition + directionFromCenterToNewPostion;
        transform.position = newPositionOnCircle;
    }
    
    private Vector3 GetNormalizeDirectionFromCenter(Vector3 position, Vector3 pointAroundWhichRotation, float radius)
    {
        Vector3 directionFromCenter = position - pointAroundWhichRotation;
        return new Vector3(directionFromCenter.x / radius, directionFromCenter.y / radius, directionFromCenter.z / radius);
    }

    private Vector3 GetNormalizeLinearMoveDirection(Vector3 normalDirectionFromCenter, bool clockWise = true)
    {
        Vector3 normalLinearMoveDirection = new Vector3(normalDirectionFromCenter.z, normalDirectionFromCenter.y, normalDirectionFromCenter.x);

        if (_moveClockwise == true)
        {
            normalLinearMoveDirection.z *= -1;
        }
        else
        {
            normalLinearMoveDirection.x *= -1;
        }

        return normalLinearMoveDirection;
    }
}