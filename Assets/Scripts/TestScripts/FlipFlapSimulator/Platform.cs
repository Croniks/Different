using System;
using System.Collections.Generic;

using UnityEngine;


public class Platform : MonoBehaviour
{
    public bool IsStartPlatform => _isStartPlatform;
    [SerializeField] private bool _isStartPlatform = false;
    
    public IReadOnlyCollection<Transform> BoundaryPoints => _boundaryPoints;
    [SerializeField] private Transform[] _boundaryPoints;
    
    public IReadOnlyCollection<Transform> NextPlatformPositions => _nextPlatformPositions;
    [SerializeField] private Transform[] _nextPlatformPositions;

    public Platform Previous { get; set; }
    public Platform Next { get; set; }

    private Action<Platform> afterKickAction;

    public Platform SetAfterKickAction(Action<Platform> afterKickAction) 
    {
        this.afterKickAction = afterKickAction;
        return this;
    }

    public void Kick()
    {
        afterKickAction?.Invoke(this);
    }
}