using UnityEngine;

public interface IChangeableLocation
{
    public void ChangeLocation(Platform previousPlatform, Bounds gameBounds);
}
