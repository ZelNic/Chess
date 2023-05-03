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

    public void GetShapeForReplacement(ChessPiece[,] mapCP,ChessPiece piece)
    {
        _figureToReplace = piece;
        _mapCP = mapCP;
        onActiveChoise?.Invoke();
    }

    private void ReplacePiece(ChessPieceType type)
    {
        ChessPiece newCP = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(type, _figureToReplace.team);
        _mapCP[_figureToReplace.currentPositionX, _figureToReplace.currentPositionY] = newCP;
        Distributor.onSetOnPlace(_figureToReplace.currentPositionX, _figureToReplace.currentPositionY, _figureToReplace.currentPositionX, _figureToReplace.currentPositionY);  
        _figureToReplace.DestroyPiece();
        Distributor.onPawnOnEdgeBoard.Invoke();
    }

}
