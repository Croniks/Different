using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlatform : MonoBehaviour
{
    public IReadOnlyCollection<Transform> BoundaryPoints { get => _boundaryPoints; }
    [SerializeField] private Transform[] _boundaryPoints;

    public IReadOnlyCollection<Transform> NextPlatformPositions { get => _nextPlatformPositions; }
    [SerializeField] private Transform[] _nextPlatformPositions;


    public void Kick(Action<AbstractPlatform> afterKickAction, float time)
    {
        Kick(time);

        afterKickAction?.Invoke(this);
    }

    protected abstract void Kick(float time);
}