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
        ChessPiece cp = Instantiate(chessPiece[(int)type - 1], transform); GetComponent<ChessPiece>();       
        cp.type = type;
        cp.team = team;
        if(team == 0)   
            cp.name = "0_" + gameObject.name;
        else cp.name = "1_" + gameObject.name;
        cp.GetComponent<SpriteRenderer>().color = _colorsTeam[team];
        return cp;
    }
}
