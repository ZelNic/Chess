using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Distributor : MonoBehaviour
{
    public static Action<ChessPiece[,]> onStartDistribution;
    public static Action<ChessPiece, int, int> onChangePositionPiece;
    public static Action<ChessPiece, int, int> onSwonAviableMoves;
    private ChessPiece[,] _mapCP;
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
        _mapCP = chessPieces;
        for (int x = 0; x < _mapCP.GetLength(0); x++)
            for (int y = 0; y < _mapCP.GetLength(1); y++)
                if (_mapCP[x, y] != null)
                    SetOnPlace(x, y);
    }
    private void SetOnPlace(int x, int y)
    {
        _mapCP[x, y].currentPositionX = x;
        _mapCP[x, y].currentPositionY = y;
        _mapCP[x, y].transform.position = new Vector3(x, y, _positionPieceZ);
    }

    private void ShowAviableMoves(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {
        ChessPieceType type = chessPiece.type;
        avaibleMove = new List<Vector2Int>();

        int direction = (chessPiece.team == 0) ? 1 : -1;
        int posX = chessPiece.currentPositionX;
        int posY = chessPiece.currentPositionY;

        int sizeMap = _mapCP.GetLength(0);

        switch (type)
        {
            #region Pown
            case ChessPieceType.Pown:
                //Move on one tile
                if (_mapCP[posX, posY + direction] == null)
                    avaibleMove.Add(new Vector2Int(posX, posY + direction));

                //Move on two tile
                if ((posY == 1 && direction == 1) || (posY == 6 && direction == -1) && _mapCP[posX, posY + (direction * 2)] == null)
                    avaibleMove.Add(new Vector2Int(posX, posY + (direction * 2)));

                //Kill Move
                if (posX == 0)
                    if (_mapCP[posX + 1, posY + direction] != null)
                        if (_mapCP[posX + 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 1, posY + direction));

                if (posX == _mapCP.GetLength(0) - 1)
                    if (_mapCP[posX - 1, posY + direction] != null)
                        if (_mapCP[posX - 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 1, posY + direction));

                if (posX != 0 && posX != _mapCP.GetLength(0) - 1)
                {
                    if (_mapCP[posX - 1, posY + direction] != null)
                        if (_mapCP[posX - 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 1, posY + direction));

                    if (_mapCP[posX + 1, posY + direction] != null)
                        if (_mapCP[posX + 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 1, posY + direction));
                }

                break;
            #endregion

            #region Rock
            case ChessPieceType.Rook:

                for (int x = posX; x < _mapCP.GetLength(0); x++)
                {
                    if (x == posX) continue;
                    if (_mapCP[x, posY] == null)
                        avaibleMove.Add(new Vector2Int(x, posY));

                    if (_mapCP[x, posY] != null)
                    {
                        if (_mapCP[x, posY].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(x, posY));
                        break;
                    }
                }

                for (int y = posY; y < _mapCP.GetLength(1); y++)
                {
                    if (y == posY) continue;
                    if (_mapCP[posX, y] == null)
                        avaibleMove.Add(new Vector2Int(posX, y));

                    if (_mapCP[posX, y] != null)
                    {
                        if (_mapCP[posX, y].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX, y));
                        break;
                    }

                }

                for (int k = posX; k >= 0; k--)
                {
                    if (k == posX) continue;

                    if (_mapCP[k, posY] == null)
                        avaibleMove.Add(new Vector2Int(k, posY));

                    if (_mapCP[k, posY] != null)
                    {
                        if (_mapCP[k, posY].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(k, posY));
                        break;
                    }
                }

                for (int j = posY; j >= 0; j--)
                {
                    if (j == posY) continue;

                    if (_mapCP[posX, j] == null)
                        avaibleMove.Add(new Vector2Int(posX, j));

                    if (_mapCP[posX, j] != null)
                    {
                        if (_mapCP[posX, j].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX, j));
                        break;
                    }
                }
                break;
            #endregion

            #region Knight
            case ChessPieceType.Knight:
                {
                    if (posX + 1 < sizeMap && posY + 2 < sizeMap)
                        if (_mapCP[posX + 1, posY + 2] == null ||
                            _mapCP[posX + 1, posY + 2] != null && _mapCP[posX + 1, posY + 2].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 1, posY + 2));


                    if (posX + 1 < sizeMap && posY - 2 > 0)
                        if (_mapCP[posX + 1, posY - 2] == null ||
                            _mapCP[posX + 1, posY - 2] != null && _mapCP[posX + 1, posY - 2].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 1, posY - 2));

                    if (posX - 1 > 0 && posY + 2 < sizeMap)
                        if (_mapCP[posX - 1, posY + 2] == null ||
                            _mapCP[posX - 1, posY + 2] != null && _mapCP[posX - 1, posY + 2].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 1, posY + 2));

                    if (posX - 1 > 0 && posY - 2 > 0)
                        if (_mapCP[posX - 1, posY - 2] == null ||
                            _mapCP[posX - 1, posY - 2] != null && _mapCP[posX - 1, posY - 2].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 1, posY - 2));

                    if (posX + 2 < sizeMap && posY + 1 < sizeMap)
                        if (_mapCP[posX + 2, posY + 1] == null ||
                            _mapCP[posX + 2, posY + 1] != null && _mapCP[posX + 2, posY + 1].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 2, posY + 1));

                    if (posX + 2 < sizeMap && posY - 1 < sizeMap)
                        if (_mapCP[posX + 2, posY - 1] == null ||
                            _mapCP[posX + 2, posY - 1] != null && _mapCP[posX + 2, posY - 1].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 2, posY - 1));

                    if (posX - 2 > 0 && posY + 1 < sizeMap)
                        if (_mapCP[posX - 2, posY + 1] == null ||
                            _mapCP[posX - 2, posY + 1] != null && _mapCP[posX - 2, posY + 1].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 2, posY + 1));

                    if (posX - 2 > 0 && posY - 1 > 0)
                        if (_mapCP[posX - 2, posY - 1] == null ||
                           _mapCP[posX - 2, posY - 1] != null && _mapCP[posX - 2, posY - 1].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 2, posY - 1));
                }
                break;
            #endregion

            #region Bishop
            case ChessPieceType.Bishop:

                for (int x = 0; x < sizeMap; x++)
                    if (posX + x < sizeMap && posY + x < sizeMap)
                    {
                        if (_mapCP[posX + x, posY + x] == null)
                            avaibleMove.Add(new Vector2Int(posX + x, posY + x));

                        if (_mapCP[posX + x, posY + x] != null && _mapCP[posX + x, posY + x].team != chessPiece.team)
                        {
                            avaibleMove.Add(new Vector2Int(posX + x, posY + x));
                            break;
                        }
                    }
                    else break;

               













                break;
                #endregion


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
                if (_mapCP[posChangeOnX, posChangeOnY] != null && _mapCP[posChangeOnX, posChangeOnY].team != chessPiece.team)
                    _mapCP[posChangeOnX, posChangeOnY].gameObject.SetActive(false);


                _mapCP[chessPiece.currentPositionX, chessPiece.currentPositionY] = null;
                _mapCP[posChangeOnX, posChangeOnY] = chessPiece;
                _mapCP[posChangeOnX, posChangeOnY].transform.position = new Vector3(posChangeOnX, posChangeOnY, _positionPieceZ);
                chessPiece.currentPositionX = posChangeOnX;
                chessPiece.currentPositionY = posChangeOnY;
                break;
            }
        }
    }
}