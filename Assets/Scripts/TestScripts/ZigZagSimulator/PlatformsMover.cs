using UnityEngine;


public class PlatformsMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _moveDirecton;

    private Transform _selfTransform;

        
    private void Awake()
    {
        _selfTransform = GetComponent<Transform>();
    }

    public void Move()
    {
        _selfTransform.Translate(_moveDirecton * _speed * Time.deltaTime, Space.Self);
    }
}