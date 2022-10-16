using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour, IKickable
{
    public IReadOnlyCollection<Transform> NextPlatformPositions => _nextPlatformPositions;
    [SerializeField] private Transform[] _nextPlatformPositions;

    public void Kick()
    {
        
    }
}
