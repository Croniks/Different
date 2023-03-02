using UnityEngine;


public class CircularMover : MonoBehaviour
{
    [SerializeField] private Transform _pointAroundWhichToMove;
    [SerializeField] private float _radius;
    [SerializeField] private float _linearMoveSpeed = 5f;
    [SerializeField] private bool _moveClockwise = false;

    private float _directionToOffsetModule = 0f;
    private Vector3 _aroundPointPosition = Vector3.zero;
    
    private void Start()
    {
        _aroundPointPosition = _pointAroundWhichToMove.position;

        Vector3 directionFromCenter = GetNormalDirectionFromCenter(transform.position, _pointAroundWhichToMove.position, _radius);
        transform.position = _pointAroundWhichToMove.position + directionFromCenter * _radius;

        Vector3 normalMoveDirection = GetNormalLinearMoveDirection(directionFromCenter);
        normalMoveDirection *= _linearMoveSpeed * Time.fixedDeltaTime;
        normalMoveDirection += transform.position;

        Vector3 newDirectionFromCenter = normalMoveDirection - _pointAroundWhichToMove.position;
        _directionToOffsetModule = newDirectionFromCenter.magnitude;

        Debug.Log($"_directionToOffset: {newDirectionFromCenter}");
        Debug.Log($"_directionToOffsetModule: {_directionToOffsetModule}");
    }

    private void FixedUpdate()
    {
        Vector3 normalDirectionFromCenter = GetNormalDirectionFromCenter(transform.position, _pointAroundWhichToMove.position, _radius);
        Vector3 normalMoveDirection = GetNormalLinearMoveDirection(normalDirectionFromCenter, _moveClockwise);
        
        normalMoveDirection *= _linearMoveSpeed * Time.fixedDeltaTime;
        normalMoveDirection += transform.position;

        Vector3 newDirectionFromCenter = normalMoveDirection - _pointAroundWhichToMove.position;
        Debug.Log($"newDirectionFromCenter: {newDirectionFromCenter}");
        Debug.Log($"newDirectionFromCenter.magnitue: {newDirectionFromCenter.magnitude}");

        newDirectionFromCenter.Normalize();
        //newDirectionFromCenter = new Vector3(
        //    newDirectionFromCenter.x / _directionToOffsetModule,
        //    newDirectionFromCenter.y / _directionToOffsetModule,
        //    newDirectionFromCenter.z / _directionToOffsetModule
        //);

        //Debug.Log($"newDirectionFromCenter: {newDirectionFromCenter}");

        newDirectionFromCenter *= _radius;

        Vector3 newPosition = _pointAroundWhichToMove.position + newDirectionFromCenter;
        transform.position = newPosition;
    }

    private Vector3 GetNormalDirectionFromCenter(Vector3 position, Vector3 pointAroundWhichRotation, float radius)
    {
        Vector3 directionFromCenter = position - pointAroundWhichRotation;
        return new Vector3(directionFromCenter.x / radius, directionFromCenter.y / radius, directionFromCenter.z / radius);
    }

    private Vector3 GetNormalLinearMoveDirection(Vector3 normalDirectionFromCenter, bool clockWise = true)
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