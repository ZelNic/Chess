using System;
using UnityEngine;

public class CreaterSingleChessPiece : MonoBehaviour
{
    public static Func<ChessPieceType, byte, ChessPiece> onCreateSinglePiece;
    [SerializeField] private ChessPiece[] chessPiece;
    [SerializeField] private Color[] _colorsTeam; // 0 - white / 1 - black

    private void OnEnable()
    {
        onCreateSinglePiece += CreateSinglePiece;
    }
    private ChessPiece CreateSinglePiece(ChessPieceType type, byte team)
    {
        ChessPiece cp = Instantiate(chessPiece[(int)type - 1], transform); GetComponent<ChessPiece>();
        cp.type = type;
        cp.team = team;
        cp.GetComponent<SpriteRenderer>().color = _colorsTeam[team];
        return cp;
    }
}
