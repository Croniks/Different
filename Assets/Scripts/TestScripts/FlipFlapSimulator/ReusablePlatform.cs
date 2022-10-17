using System.Collections.Generic;
using System.Linq;

using UnityEngine;


public class ReusablePlatform : AbstractPlatform
{
    public ReusablePlatform Previous { get; set; }
    public ReusablePlatform Next { get; set; }


    protected override void Kick(float time)
    {
        
    }

    public void MovePlatformToEndOfList(ReusablePlatform lastPlatform)
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
        Previous = lastPlatform;
        lastPlatform.Next = this;
    }

    public void PlaceLastPlatform(IReadOnlyCollection<Transform> newPositions, Bounds gameBounds)
    {
        int index = Random.Range(0, 2);
        Vector3 newPosition = newPositions.ElementAt(index).position;
        transform.localPosition = newPosition;

        if (gameBounds.Contains(BoundaryPoints.ElementAt(0).position) == false
            || gameBounds.Contains(BoundaryPoints.ElementAt(1).position) == false)
        {
            index = index == 0 ? 1 : 0;
            transform.localPosition = newPositions.ElementAt(index).position;
        }
    }
}