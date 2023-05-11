public class Pawn : ChessPiece
{
    private ChessPieceType _defaultType;
    private ChessPieceType _newType;
    public ChessPieceType DefaultType
    {
        get { return _defaultType; }
        private set { _defaultType = value; }
    }
    public ChessPieceType NewType
    {
        get { return _newType; }
        private set { _newType = value; }
    }

    private void Start() => _defaultType = type;
    public void SaveNewType() => _newType = type;
}
