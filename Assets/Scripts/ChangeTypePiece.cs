using System;
using UnityEngine;

public class ChangeTypePiece : MonoBehaviour
{
    public static Action onActiveChoise;
    private ChessPiece _figureToReplace;

    private void OnEnable()
    {
        WindowChoiseTypePiece.onChosenType += ReplacePiece;
        Distributor.onSendToReplaceType += GetShapeForReplacement;
    }

    public void GetShapeForReplacement(ChessPiece piece)
    {
        _figureToReplace = piece;
        onActiveChoise?.Invoke();
    }

    private void ReplacePiece(ChessPieceType type)
    {
        ChessPiece newPiece = CreaterSingleChessPiece.onCreateSinglePiece(type, _figureToReplace.team);
        Distributor.onSetOnPlace.Invoke(newPiece.currentPositionX, newPiece.currentPositionY, _figureToReplace.currentPositionX, _figureToReplace.currentPositionY);
        _figureToReplace.DestroyPiece();
        Distributor.onPawnOnEdgeBoard.Invoke();
    }

}
