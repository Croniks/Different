public interface IInputReader
{
    public bool IsMovingLeft { get; }

    public bool IsMovingRight { get; }

    public bool IsMovingUp { get; }

    public bool IsMovingDown { get; }

    public bool IsFiring { get; }
}

public interface IInputWriter
{
    bool IsMovingLeft { set; }

    public bool IsMovingRight { set; }

    public bool IsMovingUp { set; }

    public bool IsMovingDown { set; }

    public bool IsFiring { set; }
}