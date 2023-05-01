using System;
using UnityEngine;

public class ChangePiece : MonoBehaviour
{
    public static Action<ChessPiece[,], ChessPiece> onActiveChoose;
    [SerializeField] private GameObject _selectionBox;
    private ChessPiece[,] _mapCP;
    private ChessPiece _figureToReplace;

    private void OnEnable()
    {
        onActiveChoose += ActiveChoicePiece;
    }
    private void ActiveChoicePiece(ChessPiece[,] mapCP, ChessPiece chessPiece)
    {
        _selectionBox.SetActive(true);
        _mapCP = mapCP;
        _figureToReplace = chessPiece;
    }

    private void ReplacePiece(ChessPieceType type)
    {
        ChessPiece newChessPiece = CreaterSingleChessPiece.onCreateSinglePiece(type, _figureToReplace.team);
        newChessPiece.currentPositionX = _figureToReplace.currentPositionX;
        newChessPiece.currentPositionY = _figureToReplace.currentPositionY;
        _figureToReplace.DestroyPiece();
        _mapCP[_figureToReplace.currentPositionX, _figureToReplace.currentPositionY] = newChessPiece;
        _mapCP[_figureToReplace.currentPositionX, _figureToReplace.currentPositionY].transform.position =
        new Vector3(_figureToReplace.currentPositionX, _figureToReplace.currentPositionY);
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
        _selectionBox.SetActive(false);
    }
}
