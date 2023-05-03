using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTypePiece : MonoBehaviour
{
    public static Action<ChessPiece> onActiveChoose;
    [SerializeField] private Image _selectionBox;
    private ChessPiece[,] _mapCP;
    private ChessPiece _figureToReplace;

    private void OnEnable() => onActiveChoose += ActiveChoicePiece;
   
    private void ActiveChoicePiece(ChessPiece chessPiece)
    {
        _selectionBox.gameObject.SetActive(true);        
        _figureToReplace = chessPiece;
    }

    private void ReplacePiece(ChessPieceType type)
    {
        ChessPiece newPiece = CreaterSingleChessPiece.onCreateSinglePiece(type, _figureToReplace.team);
        Distributor.onSetOnPlace.Invoke(newPiece.currentPositionX, newPiece.currentPositionY, _figureToReplace.currentPositionX, _figureToReplace.currentPositionY);
        _figureToReplace.DestroyPiece();        
        Player.onStopSelection.Invoke();
    }

    public void ChooseRock()
    {
        ReplacePiece(ChessPieceType.Rook);
        DeactiveChoicePiece();
    }
    public void ChooseKnight()
    {
        ReplacePiece(ChessPieceType.Knight);
        DeactiveChoicePiece();
    }
    public void ChooseBishop()
    {
        ReplacePiece(ChessPieceType.Bishop);
        DeactiveChoicePiece();
    }
    public void ChooseQueen()
    {
        ReplacePiece(ChessPieceType.Queen);
        DeactiveChoicePiece();
    }
    private void DeactiveChoicePiece()
    {
        _selectionBox.gameObject.SetActive(false);
    }
}
