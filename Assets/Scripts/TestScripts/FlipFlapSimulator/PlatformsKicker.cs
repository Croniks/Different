using System;
using System.Collections.Generic;

using UnityEngine;


public class PlatformsKicker : MonoBehaviour
{
    public event Action<AbstractPlatform> PlatformKicked;

    public Dictionary<Collider, AbstractPlatform> kickablesDict = new Dictionary<Collider, AbstractPlatform>();

    
    public void RegisterPlatformByCollider(Collider collider, AbstractPlatform kickable)
    {
        if(kickablesDict.ContainsKey(collider) == false)
        {
            kickablesDict.Add(collider, kickable);  
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(kickablesDict.TryGetValue(other, out AbstractPlatform platform))
        {
            platform.Kick(AfterKickAction);
        }
    }

    private void AfterKickAction(AbstractPlatform platform)
    {
        PlatformKicked?.Invoke(platform);
    }
}