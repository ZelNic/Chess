using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        int direction = (chessPiece.team == 0) ? 1 : -1;
        int posX = chessPiece.currentPositionX;
        int posY = chessPiece.currentPositionY;


        switch (type)
        {
            #region Pown
            case ChessPieceType.Pown:
                //Move on one tile
                if (_coordinatePiece[posX, posY + direction] == null)
                    avaibleMove.Add(new Vector2Int(posX, posY + direction));

                //Move on two tile
                if ((posY == 1 && direction == 1) || (posY == 6 && direction == -1) && _coordinatePiece[posX, posY + (direction * 2)] == null)
                    avaibleMove.Add(new Vector2Int(posX, posY + (direction * 2)));

                //Kill Move
                if (posX == 0)
                    if (_coordinatePiece[posX + 1, posY + direction] != null)
                        if (_coordinatePiece[posX + 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 1, posY + direction));

                if (posX == _coordinatePiece.GetLength(0) - 1)
                    if (_coordinatePiece[posX - 1, posY + direction] != null)
                        if (_coordinatePiece[posX - 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 1, posY + direction));

                if (posX != 0 && posX != _coordinatePiece.GetLength(0) - 1)
                {
                    if (_coordinatePiece[posX - 1, posY + direction] != null)
                        if (_coordinatePiece[posX - 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 1, posY + direction));

                    if (_coordinatePiece[posX + 1, posY + direction] != null)
                        if (_coordinatePiece[posX + 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 1, posY + direction));
                }

                break;
            #endregion

            case ChessPieceType.Rook:

                for (int x = posX; x < _coordinatePiece.GetLength(0); x++)
                {
                    if (x == posX) continue;
                    if (_coordinatePiece[x, posY] == null)
                        avaibleMove.Add(new Vector2Int(x, posY));

                    if (_coordinatePiece[x, posY] != null)
                    {
                        if (_coordinatePiece[x, posY].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(x, posY));
                        break;
                    }
                }

                for (int y = posY; y < _coordinatePiece.GetLength(1); y++)
                {
                    if (y == posY) continue;
                    if (_coordinatePiece[posX, y] == null)
                        avaibleMove.Add(new Vector2Int(posX, y));

                    if (_coordinatePiece[posX, y] != null)
                    {
                        if (_coordinatePiece[posX, y].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX, y));
                        break;
                    }

                }

                for (int k = posX; k >= 0; k--)
                {
                    if (k == posX) continue;

                    if (_coordinatePiece[k, posY] == null)
                        avaibleMove.Add(new Vector2Int(k, posY));

                    if (_coordinatePiece[k, posY] != null)
                    {
                        if (_coordinatePiece[k, posY].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(k, posY));
                        break;
                    }
                }

                for (int j = posY; j >= 0; j--)
                {
                    if (j == posY) continue;

                    if (_coordinatePiece[posX, j] == null)
                        avaibleMove.Add(new Vector2Int(posX, j));

                    if (_coordinatePiece[posX, j] != null)
                    {
                        if (_coordinatePiece[posX, j].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX, j));
                        break;
                    }
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
        for (int i = 0; i < avaibleMove.Count; i++)
        {
            if (avaibleMove[i] == new Vector2Int(posChangeOnX, posChangeOnY))
            {
                if (_coordinatePiece[posChangeOnX, posChangeOnY] != null && _coordinatePiece[posChangeOnX, posChangeOnY].team != chessPiece.team)
                    _coordinatePiece[posChangeOnX, posChangeOnY].gameObject.SetActive(false);


                _coordinatePiece[chessPiece.currentPositionX, chessPiece.currentPositionY] = null;
                _coordinatePiece[posChangeOnX, posChangeOnY] = chessPiece;
                _coordinatePiece[posChangeOnX, posChangeOnY].transform.position = new Vector3(posChangeOnX, posChangeOnY, _positionPieceZ);
                chessPiece.currentPositionX = posChangeOnX;
                chessPiece.currentPositionY = posChangeOnY;
                break;
            }
        }
    }
}
/*case ChessPieceType.Bishop:

               for (int x = posX; x < _coordinatePiece.GetLength(0); x++)
   if (_coordinatePiece[x, x] == null)
       avaibleMove.Add(new Vector2Int(x, x));
break;*/