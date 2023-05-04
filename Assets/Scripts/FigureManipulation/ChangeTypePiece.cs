using System;
using UnityEngine;

public class ChangeTypePiece : MonoBehaviour
{
    public static Action onActiveChoise;
    private ChessPiece _figureToReplace;
    private ChessPiece[,] _mapCP;
    private void OnEnable()
    {
        WindowChoiseTypePiece.onChosenType += ReplacePiece;
        Distributor.onSendToReplaceType += GetShapeForReplacement;
    }
    public void GetShapeForReplacement(ChessPiece[,] mapCP, ChessPiece piece)
    {
        _figureToReplace = piece;
        _mapCP = mapCP;
        onActiveChoise?.Invoke();
    }
    private void ReplacePiece(ChessPieceType type)
    {
        ChessPiece newCP = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(type, _figureToReplace.team);
        newCP.currentPositionX = _mapCP[_figureToReplace.currentPositionX, _figureToReplace.currentPositionY].currentPositionX;
        newCP.currentPositionY = _mapCP[_figureToReplace.currentPositionX, _figureToReplace.currentPositionY].currentPositionY;
        newCP.transform.position = _figureToReplace.transform.position;
        _mapCP[_figureToReplace.currentPositionX, _figureToReplace.currentPositionY] = newCP;
        _figureToReplace.DestroyPiece();
        Distributor.onPawnOnEdgeBoard.Invoke();
    }

}
