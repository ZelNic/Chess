using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangePiece : MonoBehaviour
{
    public static Action<ChessPiece[,], ChessPiece> onActiveChoose;
    [SerializeField] private Image _selectionBox;
    private ChessPiece[,] _mapCP;
    private ChessPiece _figureToReplace;

    private void OnEnable()
    {
        onActiveChoose += ActiveChoicePiece;
    }
    private void ActiveChoicePiece(ChessPiece[,] mapCP, ChessPiece chessPiece)
    {
        _selectionBox.gameObject.SetActive(true);
        _mapCP = mapCP;
        _figureToReplace = chessPiece;
    }

    private void ReplacePiece(ChessPieceType type)
    {
        ChessPiece newPiece = CreaterSingleChessPiece.onCreateSinglePiece(type, _figureToReplace.team);
        newPiece.currentPositionX = _figureToReplace.currentPositionX;
        newPiece.currentPositionY = _figureToReplace.currentPositionY;
        _mapCP[_figureToReplace.currentPositionX, _figureToReplace.currentPositionY] = newPiece;
        _figureToReplace.DestroyPiece();
        Distributor.onSetOnPlace.Invoke(newPiece.currentPositionX, newPiece.currentPositionY);
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
