using System;
using UnityEngine;

public class ChangeTypePiece : MonoBehaviour
{
    public static Action onActiveChoise;
    private ChessPiece _pieceToReplace;
    private ChessPiece[,] _mapCP;
    private void OnEnable()
    {
        WindowChoiseTypePiece.onChosenType += ReplacePiece;
        Distributor.onSendToReplaceType += GetPieceForReplacement;
    }
    public void GetPieceForReplacement(ChessPiece[,] mapCP, ChessPiece piece)
    {
        _pieceToReplace = piece;
        _mapCP = mapCP;
        onActiveChoise?.Invoke();
    }
    private void ReplacePiece(ChessPieceType type)
    {
        ChessPiece newCP = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(type, _pieceToReplace.team);
        newCP.currentPositionX = _mapCP[_pieceToReplace.currentPositionX, _pieceToReplace.currentPositionY].currentPositionX;
        newCP.currentPositionY = _mapCP[_pieceToReplace.currentPositionX, _pieceToReplace.currentPositionY].currentPositionY;
        newCP.transform.position = _pieceToReplace.transform.position;
        _mapCP[_pieceToReplace.currentPositionX, _pieceToReplace.currentPositionY] = newCP;
        _pieceToReplace.DestroyPiece();
        Distributor.onPawnOnEdgeBoard.Invoke();
    }

}
