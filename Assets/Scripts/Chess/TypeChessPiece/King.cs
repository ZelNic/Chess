public class King : ChessPiece
{
    private bool _firstStep = true;
    public bool FirstStep
    {
        get { return _firstStep; }
        private set { _firstStep = value; }
    }
    public void MakeMove() => FirstStep = false;


}
