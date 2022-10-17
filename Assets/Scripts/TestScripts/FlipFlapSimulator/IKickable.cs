using System;

public interface IKickable
{ 
    public void Kick(Action<IKickable> afterKick);
}
