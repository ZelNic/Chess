using System;
using UnityEngine;

public class ConverterType : MonoBehaviour
{
    public static Action onActiveChoise;

    [SerializeField] private Sprite[] _sprites;
    private ChessPiece _pieceToReplace;

    private void OnEnable()
    {
        WindowChoiseTypePiece.onChosenType += ReplacePiece;
        Distributor.onSendToReplaceType += GetPieceForReplacement;
        Changelog.onChangeType += ReplacePiece;
    }
    public void GetPieceForReplacement(ChessPiece piece)
    {
        _pieceToReplace = piece;
        onActiveChoise?.Invoke();
    }
    private void ReplacePiece(ChessPieceType type)
    {
        _pieceToReplace.type = type;
        _pieceToReplace.GetComponent<Pawn>().SaveNewType();
        SetSprite(type);
    }

    private void ReplacePiece(ChessPiece chessPiece, ChessPieceType type)
    {
        _pieceToReplace = chessPiece;
        _pieceToReplace.type = type;
        SetSprite(type);
    }

    private void SetSprite(ChessPieceType type)
    {
        int number = 0;

        switch (type)
        {
            case ChessPieceType.Pawn: number = 0; break;
            case ChessPieceType.Rook: number = 1; break;
            case ChessPieceType.Knight: number = 2; break;
            case ChessPieceType.Bishop: number = 3; break;
            case ChessPieceType.Queen: number = 4; break;
        }
        _pieceToReplace.GetComponent<SpriteRenderer>().sprite = _sprites[number];
    }
}