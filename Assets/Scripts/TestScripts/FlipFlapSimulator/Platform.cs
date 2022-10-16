using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


public class Platform : MonoBehaviour, IChangeableLocation, IKickable
{
    public IReadOnlyCollection<Transform> BoundaryPoints => _boundaryPoints;
    [SerializeField] private Transform[] _boundaryPoints;
    
    public IReadOnlyCollection<Transform> NextPlatformPositions => _nextPlatformPositions;
    [SerializeField] private Transform[] _nextPlatformPositions;

    public Platform Previous { get; set; }
    public Platform Next { get; set; }

    private Action<Platform> afterKickAction;

    public Platform OnPlatformKicked(Action<Platform> afterKickAction) 
    {
        this.afterKickAction = afterKickAction;
        return this;
    }

    public void Kick()
    {
        afterKickAction?.Invoke(this);
        afterKickAction = null;
    }

    public void ChangeLocation(Platform previousPlatform, Bounds gameBounds)
    {
        if (Previous != null)
        {
            Previous.Next = Next;
        }

        if (Next != null)
        {
            Next.Previous = Previous;
        }

        Next = null;
        Previous = previousPlatform;
        previousPlatform.Next = this;

        var availablePositions = previousPlatform.NextPlatformPositions;
        int index = UnityEngine.Random.Range(0, 2);
        Vector3 newPosition = availablePositions.ElementAt(index).position;
        transform.localPosition = newPosition;

        if (gameBounds.Contains(BoundaryPoints.ElementAt(0).position) == false
            || gameBounds.Contains(BoundaryPoints.ElementAt(1).position) == false)
        {
            index = index == 0 ? 1 : 0;
            transform.localPosition = availablePositions.ElementAt(index).position;
        }
    }
}