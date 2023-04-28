using System;
using System.Collections.Generic;
using UnityEngine;

public class Distributor : MonoBehaviour
{
    public static Action<ChessPiece[,]> onStartDistribution;
    public static Action<ChessPiece, int, int> onChangePositionPiece;
    public static Action<ChessPiece, int, int> onSwonAviableMoves;
    private ChessPiece[,] _coordinatePiece;
    private Tile[,] _arrayTile;
    private readonly int _positionPieceZ = -5;
    private List<Vector2Int> avaibleMove;


    private void OnEnable()
    {
        onStartDistribution += Distribute;
        onChangePositionPiece += ChangePositionPiece;
        onSwonAviableMoves += ShowAviableMoves;
    }

    private void Start()
    {
        _arrayTile = BoardCreator.onSendArrayTile?.Invoke();
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
        _coordinatePiece[x, y].transform.position = new Vector3(x, y, _positionPieceZ);
    }

    private void ShowAviableMoves(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {
        ChessPieceType type = chessPiece.type;
        avaibleMove = new List<Vector2Int>();

        int posX = chessPiece.currentPositionX;
        int posY = chessPiece.currentPositionY;

        switch (type)
        {
            case ChessPieceType.Pown:

                int direction = (chessPiece.team == 0) ? 1 : -1;
                if (_coordinatePiece[posX, posY + direction] == null)
                    avaibleMove.Add(new Vector2Int(posX, posY + direction));
                switch (direction)
                {
                    case 1:
                        if (_coordinatePiece[posX, posY + direction++] == null && posY == 1)
                            avaibleMove.Add(new Vector2Int(posX, posY + direction++));
                        break;

                    case -1:
                        if (_coordinatePiece[posX, posY + direction--] == null && posY == 6)
                            avaibleMove.Add(new Vector2Int(posX, posY + direction--));
                        break;
                }
                break;
        }

        for (int i = 0; i < avaibleMove.Count; i++)
        {
            _arrayTile[avaibleMove[i].x, avaibleMove[i].y].OnHighlight();
        }
    }

    private void ChangePositionPiece(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {
        _coordinatePiece[chessPiece.currentPositionX, chessPiece.currentPositionY] = chessPiece;
        _coordinatePiece[posChangeOnX, posChangeOnY] = chessPiece;

        for (int i = 0; i < avaibleMove.Count; i++)
            if (avaibleMove[i] == new Vector2Int(posChangeOnX, posChangeOnY))
            {
                _coordinatePiece[posChangeOnX, posChangeOnY].transform.position = new Vector3(posChangeOnX, posChangeOnY, _positionPieceZ);
                chessPiece.currentPositionX = posChangeOnX;
                chessPiece.currentPositionY = posChangeOnY;
                break;
            }
    }
}




