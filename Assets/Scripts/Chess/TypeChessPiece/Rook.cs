public class Rook : ChessPiece
{
    private bool _isZeroStep = true;
    public bool IsFirstStep
    {
        get { return _isZeroStep; }
        private set { _isZeroStep = value; }
    }
    public void MakeStep() => IsFirstStep = false;

    public void ResetStep() => IsFirstStep = true;
}
