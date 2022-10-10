using UnityEngine;


public class PlatformsCreator : MonoBehaviour
{
    [SerializeField] private BoxCollider _gameBoundsCollider;
    [SerializeField] float _boundsLength = 50f, _boundsHight = 2f;

    private Bounds _gameBounds;


    private void Awake()
    {
        CalculateGameBounds();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CalculateGameBounds();
        }
    }

    private bool CheckPointWithinBounds(Vector3 point)
    {
        return _gameBounds.Contains(point);
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