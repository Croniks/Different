using UnityEngine;


public class TestQuaternions : MonoBehaviour
{
    [SerializeField] private Transform _anotherObjectTransform;
    [SerializeField] private float _speed;

    private Quaternion _fromToRotation;
    private Matrix4x4 _initialTransform;
    

    void Start()
    {
        _initialTransform = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

        _fromToRotation = new Quaternion();
        _fromToRotation.SetFromToRotation(transform.forward, _anotherObjectTransform.forward);
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            DoLookRotation();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTransformOptions();
        }
    }

    private void DoFromToRotation()
    {
        transform.rotation = transform.rotation * _fromToRotation;
    }

    private void RotateTowards()
    {
        var step = _speed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _anotherObjectTransform.rotation, step);
    }

    private void DoLookRotation()
    {
        Quaternion lookRotation = Quaternion.LookRotation(
            _anotherObjectTransform.position - transform.position,
            Vector3.up
        );

        var step = _speed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
    }

    private float _timeStep = 0f;
    private void DoLerpRotation()
    {
        _timeStep += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, _anotherObjectTransform.rotation, _timeStep * _speed);
    }

    private void ResetTransformOptions()
    {
        transform.position = _initialTransform.GetPosition();
        transform.rotation = _initialTransform.rotation;
        transform.localScale = _initialTransform.lossyScale;

        _fromToRotation.SetFromToRotation(transform.forward, _anotherObjectTransform.forward);
        _timeStep = 0f;
    }
}