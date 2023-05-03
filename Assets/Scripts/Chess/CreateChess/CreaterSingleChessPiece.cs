using System;
using UnityEngine;

public class CreaterSingleChessPiece : MonoBehaviour
{
    public static Func<ChessPieceType, int, ChessPiece> onCreateSinglePiece;
    [SerializeField] private ChessPiece[] chessPiece;
    [SerializeField] private Color[] _colorsTeam; // 0 - white / 1 - black

    private void OnEnable()
    {
        onCreateSinglePiece += CreateSinglePiece;
    }
    private ChessPiece CreateSinglePiece(ChessPieceType type, int team)
    {
        ChessPiece piece = Instantiate(chessPiece[(int)type - 1], transform).GetComponent<ChessPiece>();       
        piece.type = type;
        piece.team = team;
        if(team == 0)   
            piece.name = "0_" + type;
        else piece.name = "1_" + type;
        piece.GetComponent<SpriteRenderer>().color = _colorsTeam[team];
        if(piece.type == ChessPieceType.Knight &&  piece.team == 1)
            piece.GetComponent<SpriteRenderer>().flipX = true;
        return piece;
    }
}
