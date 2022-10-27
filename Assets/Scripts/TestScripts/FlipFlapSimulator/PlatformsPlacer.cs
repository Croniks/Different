using System;
using UnityEngine;


public class PlatformsPlacer
{
    public event Action<ReusablePlatform> PlatformCreated;

    private BoxCollider _gameBoundsCollider;
    private float _boundsLength, _boundsHight;
    private Bounds _gameBounds;

    private int _maxPltaformsCount;
    private ReusablePlatform _platformPrefab;

    private AbstractPlatform _firstPlatform = null;
    private AbstractPlatform _lastPlatform = null;
    
    
    public PlatformsPlacer(BoundsInfo boundsInfo, PlatformsInfo platformsInfo)
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
        for (int i = 0; i < _maxPltaformsCount; i++)
        {
            var platform = GameObject.Instantiate(_platformPrefab, platformsParent);
            
            if(_firstPlatform == null)
            {
                _firstPlatform = _lastPlatform = platform;
            }
            else
            {
                //_lastPlatform.Next = platform;
                //platform.Previous = _lastPlatform;
            }
            
            PlatformCreated?.Invoke(platform);
        }
    }

    public void PlacePlatforms(StartPlatform startPlatform)
    {
        //for (var platform = _firstPlatform; platform != null; platform = platform.Next)
        //{
        //    if(platform == _firstPlatform)
        //    {
                
        //    }
        //    else
        //    {
                
        //    }
        //}
    }
    
    public void ReplacePlatform(ReusablePlatform changeablePlatform)
    {
        //changeablePlatform.ChangeLocation(_lastPlatform, _gameBounds);
    }

    //public void MovePlatformToEndOfList(ReusablePlatform lastPlatform)
    //{
    //    if (Previous != null)
    //    {
    //        Previous.Next = Next;
    //    }

    //    if (Next != null)
    //    {
    //        Next.Previous = Previous;
    //    }

    //    Next = null;
    //    Previous = lastPlatform;
    //    lastPlatform.Next = this;
    //}

    //public void PlaceLastPlatform(IReadOnlyCollection<Transform> newPositions, Bounds gameBounds)
    //{
    //    int index = Random.Range(0, 2);
    //    Vector3 newPosition = newPositions.ElementAt(index).position;
    //    transform.localPosition = newPosition;

    //    //if (gameBounds.Contains(BoundaryPoints.ElementAt(0).position) == false
    //    //    || gameBounds.Contains(BoundaryPoints.ElementAt(1).position) == false)
    //    //{
    //    //    index = index == 0 ? 1 : 0;
    //    //    transform.localPosition = newPositions.ElementAt(index).position;
    //    //}
    //}

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