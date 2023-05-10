using System;
using Unity.VisualScripting;
using UnityEngine;

public class ConverterType : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    public static Action onActiveChoise;
    private ChessPieceTemplate _pieceToReplace;

    private void OnEnable()
    {
        WindowChoiseTypePiece.onChosenType += ReplacePiece;
        Distributor.onSendToReplaceType += GetPieceForReplacement;
    }
    public void GetPieceForReplacement(ChessPieceTemplate piece)
    {
        _pieceToReplace = piece;
        onActiveChoise?.Invoke();
    }
    private void ReplacePiece(ChessPieceType type)
    {
        _pieceToReplace.type = type;
        int number = 0;

        switch (type)
        {
            case ChessPieceType.Rook: number = 1; break;
            case ChessPieceType.Knight: number = 2; break;
            case ChessPieceType.Bishop: number = 3; break;
            case ChessPieceType.Queen: number = 4; break;
        }
        _pieceToReplace.GetComponent<SpriteRenderer>().sprite = _sprites[number];
    }



}





//ChessPiece newCP = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(type, _pieceToReplace.team);
//newCP.currentPositionX = _mapCP[_pieceToReplace.currentPositionX, _pieceToReplace.currentPositionY].currentPositionX;
//    newCP.currentPositionY = _mapCP[_pieceToReplace.currentPositionX, _pieceToReplace.currentPositionY].currentPositionY;
//    newCP.transform.position = _pieceToReplace.transform.position;
//    _mapCP[_pieceToReplace.currentPositionX, _pieceToReplace.currentPositionY] = newCP;
//    _pieceToReplace.DestroyPiece();
