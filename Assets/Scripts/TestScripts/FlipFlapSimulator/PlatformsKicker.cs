using System;
using System.Collections.Generic;

using UnityEngine;


public class PlatformsKicker : MonoBehaviour
{
    public event Action<Platform> PlatformKicked;

    public Dictionary<Collider, Platform> platformsAndColliders = new Dictionary<Collider, Platform>();

    
    public void RegisterByCollider(Collider collider, Platform platform)
    {
        if(platformsAndColliders.ContainsKey(collider) == false)
        {
            platformsAndColliders.Add(collider, platform);  
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(platformsAndColliders.TryGetValue(other, out Platform platform))
        {
            if(platform.IsStartPlatform != true)
            {
                platform.SetAfterKickAction(OnPlatformKicked);
            }

            platform.Kick();
        }
    }
    
    private void OnPlatformKicked(Platform platform)
    {
        PlatformKicked?.Invoke(platform);
    }
}