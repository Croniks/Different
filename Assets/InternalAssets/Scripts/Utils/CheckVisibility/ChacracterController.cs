using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChacracterController : MonoBehaviour
{
    [SerializeField] private Vector3 _firstVector;
    [SerializeField] private Vector3 _secondVector;
    [SerializeField, Range(0f, 1f)] private float _step;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DoLerp(_firstVector, _secondVector, _step);
            DoProject(_firstVector, _secondVector);
        }
    }
    
    private void DoLerp(Vector3 firstVector, Vector3 secondVector, float step)
    {
        var resutlVector = Vector3.Lerp(firstVector, secondVector, step);
        Debug.Log($"Lerp : {resutlVector}, resutlVector.magnitude: {resutlVector.magnitude}");
        resutlVector.Normalize();
        Debug.Log($"Normalize(), resutlVector.magnitude: {resutlVector.magnitude}");
        Debug.Log($"resutlVector: {resutlVector}");
    }

    private void DoProject(Vector3 firstVector, Vector3 secondVector)
    {
        Debug.Log($"projectedVector: {Vector3.Project(firstVector, secondVector)}");
    }
}
