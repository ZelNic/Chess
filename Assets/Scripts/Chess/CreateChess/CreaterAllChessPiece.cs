using System;
using UnityEngine;

public class CreaterAllChessPiece : MonoBehaviour
{
    public static Action onCreateAllChessPiece;
    private ChessPiece[,] _keeperOfCoordinates;

    private void OnEnable()
    {
        onCreateAllChessPiece += SendCreationChessPieces;        
        BoardCreator.onSendSize += GetSizeBoard;        
    }

    private void GetSizeBoard(int width, int height)
    {
        _keeperOfCoordinates = new ChessPiece[width, height];
    }
    private void SendCreationChessPieces()
    {
        for (int i = 0; i < _keeperOfCoordinates.GetLength(1); i++)
        {
            _keeperOfCoordinates[i, 1] = CreaterSingleChessPiece.onCreateSinglePiece?.Invoke(ChessPieceType.Pown, 0);
        }
        _keeperOfCoordinates[0, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Rook, 0);
        _keeperOfCoordinates[1, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Knight, 0);
        _keeperOfCoordinates[2, 4] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Bishop, 0);
        _keeperOfCoordinates[3, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Queen, 0);
        _keeperOfCoordinates[4, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.King, 0);
        _keeperOfCoordinates[5, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Bishop, 0);
        _keeperOfCoordinates[6, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Knight, 0);
        _keeperOfCoordinates[7, 0] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Rook, 0);


        for (int i = 0; i < _keeperOfCoordinates.GetLength(1); i++)
        {
            _keeperOfCoordinates[i, 6] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Pown, 1);
        }
        _keeperOfCoordinates[0, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Rook, 1);
        _keeperOfCoordinates[1, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Knight, 1);
        _keeperOfCoordinates[2, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Bishop, 1);
        _keeperOfCoordinates[3, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Queen, 1);
        _keeperOfCoordinates[4, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.King, 1);
        _keeperOfCoordinates[5, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Bishop, 1);
        _keeperOfCoordinates[6, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Knight, 1);
        _keeperOfCoordinates[7, 7] = CreaterSingleChessPiece.onCreateSinglePiece.Invoke(ChessPieceType.Rook, 1);

        Distributor.onStartDistribution?.Invoke(_keeperOfCoordinates);
    }    
}
