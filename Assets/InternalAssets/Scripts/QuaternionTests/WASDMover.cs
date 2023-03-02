using UnityEngine;


public class WASDMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private int _delayInFrames = 15;

    private int _currentDelay = 0;

    private void OnEnable()
    {
        _currentDelay = 0;
    }

    private void Update()
    {
        _currentDelay--;
        _currentDelay = _currentDelay < 0 ? 0 : _currentDelay;

        Vector3 moveDirecton = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            moveDirecton += Vector3.forward;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            if(_currentDelay <= 0)
            {
                _currentDelay = _delayInFrames;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirecton += Vector3.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirecton += Vector3.back;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirecton += Vector3.right;
        }

        moveDirecton.Normalize();
        transform.position += _moveSpeed * moveDirecton * Time.deltaTime;
        
        if (moveDirecton.sqrMagnitude > 0f)
        {
            moveDirecton = Quaternion.Euler(0, -90f, 0) * moveDirecton;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirecton, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed);
        }
        else
        {
            _currentDelay = 0;
        }
    }

    private bool CheckDelay(ref int delay)
    {
        delay++;
        delay = Mathf.Clamp(delay, 0, _delayInFrames);

        return delay == _delayInFrames;
    }
}