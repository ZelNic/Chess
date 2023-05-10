using System;
using UnityEngine;

public class CreaterAllChessPiece : MonoBehaviour
{
    public static Action onCreateAllChessPiece;
    private ChessPieceTemplate[,] _mapCP;

    private void OnEnable()
    {
        onCreateAllChessPiece += SendCreationChessPieces;        
        BoardCreator.onSendSize += GetSizeBoard;        
    }
    private void GetSizeBoard(int width, int height) => _mapCP = new ChessPieceTemplate[width, height];   
    private void SendCreationChessPieces()
    {
        //White
        for (int i = 0; i < _mapCP.GetLength(1); i++)
        {
            _mapCP[i, 1] = CreaterSingleChessPiece.onCreateSinglePiece?.Invoke(ChessPieceType.Pawn, 0);
        }
        _mapCP[0, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Rook, 0);
        _mapCP[1, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Knight, 0);
        _mapCP[2, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Bishop, 0);
        _mapCP[3, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Queen, 0);
        _mapCP[4, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.King, 0);
        _mapCP[5, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Bishop, 0);
        _mapCP[6, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Knight, 0);
        _mapCP[7, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Rook, 0);

        //Black
        for (int i = 0; i < _mapCP.GetLength(1); i++)
        {
            _mapCP[i, 6] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Pawn, 1);
        }
        _mapCP[0, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Rook, 1);
        _mapCP[1, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Knight, 1);
        _mapCP[2, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Bishop, 1);
        _mapCP[3, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Queen, 1);
        _mapCP[4, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.King, 1);
        _mapCP[5, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Bishop, 1);
        _mapCP[6, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Knight, 1);
        _mapCP[7, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Rook, 1);

        Distributor.onStartDistribution?.Invoke(_mapCP);
    }    
}
