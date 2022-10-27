using System;
using UnityEngine;


public class ReusablePlatform : AbstractPlatform
{
    [SerializeField] private PointsInfo _boundaryPoints;
    public PointsInfo BoundaryPoints => ConvertPoints(_boundaryPoints);

    public ReusablePlatform Previous { get; set; }
    public ReusablePlatform Next { get; set; }
    

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        var rad = transform.localScale.x / 10;

        var boundaryPoints = ConvertPoints(_boundaryPoints);

        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(boundaryPoints.firsPoint, rad);
        Gizmos.DrawSphere(boundaryPoints.secondPoint, rad);
    }
}