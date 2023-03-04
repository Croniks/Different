using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCircleMover : MonoBehaviour
{
    [SerializeField] private Transform _pointAroundWhichToMove;
    [SerializeField] private float _radius;
    [SerializeField] private float _linearMoveSpeed = 5f;
    [SerializeField] private bool _moveClockwise = false;

    private float _directionToNewPostionModule = 0f;
    private Vector3 _aroundPointPosition = Vector3.zero;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}