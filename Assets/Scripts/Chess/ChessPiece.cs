using UnityEngine;

public enum ChessPieceType
{
    None = 0,
    Pown = 1,
    Rook = 2,
    Knight = 3,
    Bishop = 4,
    Queen = 5,
    King = 6
}

public class ChessPiece : MonoBehaviour
{
    public ChessPieceType type;
    public int team; // 0 - white / 1 - black
    public int currentPositionX;
    public int currentPositionY;
}
