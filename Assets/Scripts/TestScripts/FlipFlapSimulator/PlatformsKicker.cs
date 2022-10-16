using System;
using System.Collections.Generic;

using UnityEngine;


public class PlatformsKicker : MonoBehaviour
{
    public event Action<Platform> PlatformKicked;

    public Dictionary<Collider, IKickable> platformsAndColliders = new Dictionary<Collider, IKickable>();

    
    public void RegisterByCollider(Collider collider, IKickable kickable)
    {
        if(platformsAndColliders.ContainsKey(collider) == false)
        {
            platformsAndColliders.Add(collider, kickable);  
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(platformsAndColliders.TryGetValue(other, out IKickable kickable))
        {
            if(kickable is Platform plaform)
            {
                plaform.OnPlatformKicked(OnPlatformKicked);
            }

            kickable.Kick();
        }
    }
    
    private void OnPlatformKicked(Platform platform)
    {
        PlatformKicked?.Invoke(platform);
    }
}