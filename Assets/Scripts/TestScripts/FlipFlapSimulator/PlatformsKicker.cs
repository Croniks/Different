using System;
using System.Collections.Generic;

using UnityEngine;


public class PlatformsKicker : MonoBehaviour
{
    public event Action<IKickable> PlatformKicked;

    public Dictionary<Collider, IKickable> kickablesDict = new Dictionary<Collider, IKickable>();

    
    public void RegisterByCollider(Collider collider, IKickable kickable)
    {
        if(kickablesDict.ContainsKey(collider) == false)
        {
            kickablesDict.Add(collider, kickable);  
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(kickablesDict.TryGetValue(other, out IKickable kickable))
        {
            kickable.Kick(OnPlatformKicked);
        }
    }
    
    private void OnPlatformKicked(IKickable kickable)
    {
        PlatformKicked?.Invoke(kickable);
    }
}