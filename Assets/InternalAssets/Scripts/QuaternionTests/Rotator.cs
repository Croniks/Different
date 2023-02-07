using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform _first;
    [SerializeField] private Transform _second;

    private Quaternion _firstRotation;
    private Quaternion _secondRotation;
    
    private Quaternion _fromToRotation;

    private void Awake()
    {
        Setup();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) == true) 
        {
            //_first.rotation = Quaternion.Slerp(_first.rotation, _secondRotation, 0.05f);
            _first.rotation = Quaternion.RotateTowards(_first.rotation, _secondRotation, 0.5f);
        }
        else if (Input.GetKey(KeyCode.G) == true)
        {
            _first.rotation = Quaternion.Lerp(_first.rotation, _fromToRotation, 0.01f);
        }
        else if (Input.GetKeyDown(KeyCode.R) == true)
        {
            ResetRotations();
        }
    }

    private void Setup()
    {
        _firstRotation = _first.rotation;
        _secondRotation = _second.rotation;

        //_fromToRotation = Quaternion.FromToRotation(_first.forward, _first.forward * -1);
        //_fromToRotation = Quaternion.FromToRotation(_first.right, _second.right * -1f);
        _fromToRotation = Quaternion.FromToRotation(_first.right, Vector3.left);
    }

    private void ResetRotations()
    {
        _first.rotation = _firstRotation;
        _second.rotation = _secondRotation;

        //_fromToRotation = Quaternion.identity;
    }
}