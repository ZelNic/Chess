using System;
using UnityEngine;

public class Distributor : MonoBehaviour
{
    public static Action<ChessPiece[,]> onStartDistribution;
    public static Action<ChessPiece, int, int, int, int> onSetOnPlace;
    private ChessPiece[,] _coordinatePiece;
    private void OnEnable()
    {
        onStartDistribution += Distribute;
        onSetOnPlace += ChangePositionPiece;
    }

    public void Distribute(ChessPiece[,] chessPieces)
    {
        _coordinatePiece = chessPieces;
        for (int x = 0; x < _coordinatePiece.GetLength(0); x++)
            for (int y = 0; y < _coordinatePiece.GetLength(1); y++)
                if (_coordinatePiece[x, y] != null)
                    SetOnPlace(x, y);
    }
    private void SetOnPlace(int x, int y)
    {
        _coordinatePiece[x, y].currentPositionX = x;
        _coordinatePiece[x, y].currentPositionY = y;
        _coordinatePiece[x, y].transform.position = new Vector3(x, y, -5);
    }

    private void ChangePositionPiece(ChessPiece chessPiece, int positionNowX, int positionNowY, int posChangeOnX, int posChangeOnY)
    {
        _coordinatePiece[positionNowX, positionNowY] = chessPiece;
        _coordinatePiece[posChangeOnX, posChangeOnY].currentPositionX = posChangeOnX;
        _coordinatePiece[posChangeOnX, posChangeOnY].currentPositionY = posChangeOnY;
        _coordinatePiece[posChangeOnX, posChangeOnY] = chessPiece;

        if (chessPiece.type == ChessPieceType.Pown && chessPiece.team == 0 && posChangeOnY == _coordinatePiece.GetLength(1) - 1)
            print("Change type Pown White");

        if (chessPiece.type == ChessPieceType.Pown && chessPiece.team == 1 && posChangeOnY == 0)
        {
            chessPiece.type = ChessPieceType.Queen;
            print("black");
        }
        _coordinatePiece[posChangeOnX, posChangeOnY].transform.position = new Vector3(posChangeOnX, posChangeOnY, -5);
    }
}
