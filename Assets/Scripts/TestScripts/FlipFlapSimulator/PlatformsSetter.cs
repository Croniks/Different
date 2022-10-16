using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


public class PlatformsSetter
{
    public event Action<Platform> PlatformCreated;

    private BoxCollider _gameBoundsCollider;
    private float _boundsLength, _boundsHight;
    private Bounds _gameBounds;

    private int _maxPltaformsCount;
    private Platform _platformPrefab;

    private LinkedList<Platform> _platformsList;
    
    public PlatformsSetter(BoundsInfo boundsInfo, PlatformsInfo platformsInfo)
    {
        _gameBoundsCollider = boundsInfo.boundsCollider;
        _boundsLength = boundsInfo.boundsLength;
        _boundsHight = boundsInfo.boundsHight;

        _maxPltaformsCount = platformsInfo.maxPlatformsCount;
        _platformPrefab = platformsInfo.platformPrefab;

        CalculateGameBounds();
    }

    public void CreatePlatforms(Transform platformsParent)
    {
        _platformsList = new LinkedList<Platform>();

        for (int i = 0; i < _maxPltaformsCount; i++)
        {
            var platform = GameObject.Instantiate(_platformPrefab, platformsParent);
            var platformNode = _platformsList.AddLast(platform);

            platform.Previous = platformNode.Previous.Value;
            platform.Next = platformNode.Next.Value;

            PlatformCreated?.Invoke(platform);
        }
    }

    public void SetPlatforms(IReadOnlyCollection<Transform> firstNextPlatformPositions)
    {
        for (LinkedListNode<Platform> node = _platformsList.First; node != null; node = node.Next)
        {
            if (node == _platformsList.First)
            {
                SetPlatformPosition(node.Value, firstNextPlatformPositions);
            }
            else
            {
                SetPlatformPosition(node.Value, node.Previous.Value.NextPlatformPositions);
            }
        }
    }
    
    public void SetPlatformAfterKicking(Platform platform)
    {
        if(platform.Previous != null)
        {
            platform.Previous.Next = platform.Next;
        }

        if(platform.Next != null)
        {
            platform.Next.Previous = platform.Previous;
        }
        
        platform.Next = null;
        platform.Previous = _platformsList.Last<Platform>();

        SetPlatformPosition(platform, platform.Previous.NextPlatformPositions);
    }

    private void SetPlatformPosition(Platform platform, IReadOnlyCollection<Transform> nextPlatformPositions)
    {
        int index = UnityEngine.Random.Range(0, 2);
        Vector3 newPosition = nextPlatformPositions.ElementAt(index).position;
        platform.transform.position = newPosition;

        if (_gameBounds.Contains(platform.BoundaryPoints.ElementAt(0).position) == false
            || _gameBounds.Contains(platform.BoundaryPoints.ElementAt(1).position) == false)
        {
            index = index == 0 ? 1 : 0;
            platform.transform.position = nextPlatformPositions.ElementAt(index).position;
        }
    }
    
    private void CalculateGameBounds()
    {
        _gameBoundsCollider.center = Vector3.zero;
        _gameBoundsCollider.size = Vector3.one;

        Vector3 leftPoint = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f));

        leftPoint.y = 0f;
        rightPoint.y = 0f;

        float xDistance = Vector3.Distance(leftPoint, rightPoint);
        xDistance = xDistance == 0f ? 1f : xDistance;

        float xFactor = 1 / xDistance;
        xFactor = xFactor == 0f ? 1f : xFactor;

        _gameBoundsCollider.size = new Vector3(
            _gameBoundsCollider.size.x / xFactor,
            _gameBoundsCollider.size.y * _boundsHight,
            _gameBoundsCollider.size.z + _boundsLength
        );

        float centerY = _gameBoundsCollider.transform.TransformPoint(Vector3.zero).y;
        Vector3 centerOffset = new Vector3(0f, -centerY, _boundsLength / 2);

        _gameBoundsCollider.center = _gameBoundsCollider.center + centerOffset;
        _gameBounds = _gameBoundsCollider.bounds;
    }
}