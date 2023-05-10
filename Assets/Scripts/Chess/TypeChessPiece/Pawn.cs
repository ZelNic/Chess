using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPieceTemplate
{
    private ChessPieceType _defaultType;
    private void Start()
    {
        _defaultType = type;
    }
    public ChessPieceType DefaultType
    {
        get { return _defaultType; }
        private set { _defaultType = value; }
    }
}
