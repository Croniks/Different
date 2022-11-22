using System;

public class ReusablePlatform : AbstractPlatform
{
    private Capsule _capsule;

    public override void Kick(Action<AbstractPlatform> afterKickAction)
    {
        base.Kick(afterKickAction);

        if(_capsule != null)
        {
            ReleaseCapsule();
        }
    }

    public void SetCapsule(Capsule capsule)
    {
        _capsule = capsule;
    }

    private void ReleaseCapsule()
    {
        _capsule.ReturnToCapsulePlacer();
        _capsule = null;
    }
}