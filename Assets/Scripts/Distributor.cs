using System;
using System.Collections.Generic;
using UnityEngine;

public class Distributor : MonoBehaviour
{
    public static Action onDestroyPieces;
    public static Action<ChessPiece[,]> onStartDistribution;
    public static Action<ChessPiece, int, int> onChangePositionPiece;
    public static Action<ChessPiece, int, int> onSwonAviableMoves;
    public static Action<int, int> onSetOnPlace;

    private ChessPiece[,] _mapCP;
    private Tile[,] _arrayTile;
    private readonly int _positionPieceZ = -5;
    private List<Vector2Int> avaibleMove;

    private void OnEnable()
    {
        onStartDistribution += Distribute;
        onChangePositionPiece += ChangePositionPiece;
        onSwonAviableMoves += ShowAviableMoves;
        onDestroyPieces += SendPiecesToScrap;
        onSetOnPlace += SetOnPlace;
    }

    private void Start()
    {
        _arrayTile = BoardCreator.onSendArrayTile?.Invoke();
    }

    private void SendPiecesToScrap()
    {
        for (int x = 0; x < _mapCP.GetLength(0); x++)
            for (int y = 0; y < _mapCP.GetLength(1); y++)
                if (_mapCP[x, y] != null)
                {
                    Destroy(_mapCP[x, y].gameObject);
                }
    }

    private void Distribute(ChessPiece[,] chessPieces)
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
            #region Pаwn
            case ChessPieceType.Pаwn:
                //Move on one tile
                if (posY + direction < sizeMap && posY + direction >= 0 && _mapCP[posX, posY + direction] == null)
                    avaibleMove.Add(new Vector2Int(posX, posY + direction));

                //Move on two tile
                if ((posY == 1 && direction == 1) || (posY == 6 && direction == -1) && _mapCP[posX, posY + (direction * 2)] == null)
                    avaibleMove.Add(new Vector2Int(posX, posY + (direction * 2)));

                //Kill Move
                if (posX + 1 < sizeMap && posY + direction >= 0 && posY + direction < sizeMap)
                    if (_mapCP[posX + 1, posY + direction] != null)
                        if (_mapCP[posX + 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 1, posY + direction));

                if (posX == _mapCP.GetLength(0) - 1)
                    if (_mapCP[posX - 1, posY + direction] != null)
                        if (_mapCP[posX - 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 1, posY + direction));

                if (posX - 1 >= 0 && posY + direction < sizeMap && posY + direction >= 0)
                    if (_mapCP[posX - 1, posY + direction] != null)
                        if (_mapCP[posX - 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 1, posY + direction));

                if (posX + 1 < sizeMap && posX + direction < sizeMap && posY + direction < sizeMap && posY + direction >= 0)
                    if (_mapCP[posX + 1, posY + direction] != null)
                        if (_mapCP[posX + 1, posY + direction].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 1, posY + direction));
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
                    //UpR
                    if (posX + 1 < sizeMap && posY + 2 < sizeMap)
                        if (_mapCP[posX + 1, posY + 2] == null ||
                            _mapCP[posX + 1, posY + 2] != null && _mapCP[posX + 1, posY + 2].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 1, posY + 2));

                    if (posX + 2 < sizeMap && posY + 1 < sizeMap)
                        if (_mapCP[posX + 2, posY + 1] == null ||
                            _mapCP[posX + 2, posY + 1] != null && _mapCP[posX + 2, posY + 1].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 2, posY + 1));

                    //UpL
                    if (posX - 1 >= 0 && posY + 2 < sizeMap)
                        if (_mapCP[posX - 1, posY + 2] == null ||
                            _mapCP[posX - 1, posY + 2] != null && _mapCP[posX - 1, posY + 2].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 1, posY + 2));


                    if (posX - 2 >= 0 && posY + 1 < sizeMap)
                        if (_mapCP[posX - 2, posY + 1] == null ||
                            _mapCP[posX - 2, posY + 1] != null && _mapCP[posX - 2, posY + 1].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 2, posY + 1));

                    //DwR
                    if (posX + 1 < sizeMap && posY - 2 >= 0)
                        if (_mapCP[posX + 1, posY - 2] == null ||
                            _mapCP[posX + 1, posY - 2] != null && _mapCP[posX + 1, posY - 2].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 1, posY - 2));

                    if (posX + 2 < sizeMap && posY - 1 >= 0)
                        if (_mapCP[posX + 2, posY - 1] == null ||
                            _mapCP[posX + 2, posY - 1] != null && _mapCP[posX + 2, posY - 1].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX + 2, posY - 1));

                    //DwL
                    if (posX - 1 >= 0 && posY - 2 >= 0)
                        if (_mapCP[posX - 1, posY - 2] == null ||
                            _mapCP[posX - 1, posY - 2] != null && _mapCP[posX - 1, posY - 2].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 1, posY - 2));
                    if (posX - 2 >= 0 && posY - 1 >= 0)
                        if (_mapCP[posX - 2, posY - 1] == null ||
                           _mapCP[posX - 2, posY - 1] != null && _mapCP[posX - 2, posY - 1].team != chessPiece.team)
                            avaibleMove.Add(new Vector2Int(posX - 2, posY - 1));
                }
                break;
            #endregion

            #region Bishop
            case ChessPieceType.Bishop:

                for (int UpR = 0; UpR < sizeMap; UpR++)
                    if (posX + UpR < sizeMap && posY + UpR < sizeMap)
                    {
                        if (posY + UpR == posY) continue;
                        if (_mapCP[posX + UpR, posY + UpR] == null)
                            avaibleMove.Add(new Vector2Int(posX + UpR, posY + UpR));

                        if (_mapCP[posX + UpR, posY + UpR] != null)
                        {
                            if (_mapCP[posX + UpR, posY + UpR].team != chessPiece.team)
                            {
                                avaibleMove.Add(new Vector2Int(posX + UpR, posY + UpR));
                                break;
                            }
                            if (_mapCP[posX + UpR, posY + UpR].team == chessPiece.team)
                                break;
                        }
                    }
                    else break;

                for (int DwR = 0; DwR < sizeMap; DwR++)
                    if (posX + DwR < sizeMap && posY - DwR >= 0)
                    {
                        if (posY - DwR == posY) continue;
                        if (_mapCP[posX + DwR, posY - DwR] == null)
                            avaibleMove.Add(new Vector2Int(posX + DwR, posY - DwR));

                        if (_mapCP[posX + DwR, posY - DwR] != null)
                        {
                            if (_mapCP[posX + DwR, posY - DwR].team != chessPiece.team)
                            {
                                avaibleMove.Add(new Vector2Int(posX + DwR, posY - DwR));
                                break;
                            }
                            if (_mapCP[posX + DwR, posY - DwR].team == chessPiece.team)
                                break;
                        }
                    }
                    else break;

                for (int UpL = 0; UpL < sizeMap; UpL++)
                    if (posX - UpL >= 0 && posY + UpL < sizeMap)
                    {
                        if (posX - UpL == posX) continue;
                        if (_mapCP[posX - UpL, posY + UpL] == null)
                            avaibleMove.Add(new Vector2Int(posX - UpL, posY + UpL));

                        if (_mapCP[posX - UpL, posY + UpL] != null)
                        {
                            if (_mapCP[posX - UpL, posY + UpL].team != chessPiece.team)
                            {
                                avaibleMove.Add(new Vector2Int(posX - UpL, posY + UpL));
                                break;
                            }
                            if (_mapCP[posX - UpL, posY + UpL].team == chessPiece.team)
                                break;
                        }
                    }
                    else break;

                for (int DwL = 0; DwL < sizeMap; DwL++)
                    if (posX - DwL >= 0 && posY - DwL >= 0)
                    {
                        if (posX - DwL == posX && posY - DwL == posY) continue;
                        if (_mapCP[posX - DwL, posY - DwL] == null)
                            avaibleMove.Add(new Vector2Int(posX - DwL, posY - DwL));

                        if (_mapCP[posX - DwL, posY - DwL] != null)
                        {
                            if (_mapCP[posX - DwL, posY - DwL].team != chessPiece.team)
                            {
                                avaibleMove.Add(new Vector2Int(posX - DwL, posY - DwL));
                                break;
                            }
                            if (_mapCP[posX - DwL, posY - DwL].team == chessPiece.team)
                                break;
                        }
                    }
                    else break;
                break;
            #endregion

            #region Queen
            case ChessPieceType.Queen:
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

                //Diagonal
                for (int UpR = 0; UpR < sizeMap; UpR++)
                    if (posX + UpR < sizeMap && posY + UpR < sizeMap)
                    {
                        if (posY + UpR == posY) continue;
                        if (_mapCP[posX + UpR, posY + UpR] == null)
                            avaibleMove.Add(new Vector2Int(posX + UpR, posY + UpR));

                        if (_mapCP[posX + UpR, posY + UpR] != null)
                        {
                            if (_mapCP[posX + UpR, posY + UpR].team != chessPiece.team)
                            {
                                avaibleMove.Add(new Vector2Int(posX + UpR, posY + UpR));
                                break;
                            }
                            if (_mapCP[posX + UpR, posY + UpR].team == chessPiece.team)
                                break;
                        }
                    }
                    else break;

                for (int DwR = 0; DwR < sizeMap; DwR++)
                    if (posX + DwR < sizeMap && posY - DwR >= 0)
                    {
                        if (posY - DwR == posY) continue;
                        if (_mapCP[posX + DwR, posY - DwR] == null)
                            avaibleMove.Add(new Vector2Int(posX + DwR, posY - DwR));

                        if (_mapCP[posX + DwR, posY - DwR] != null)
                        {
                            if (_mapCP[posX + DwR, posY - DwR].team != chessPiece.team)
                            {
                                avaibleMove.Add(new Vector2Int(posX + DwR, posY - DwR));
                                break;
                            }
                            if (_mapCP[posX + DwR, posY - DwR].team == chessPiece.team)
                                break;
                        }
                    }
                    else break;

                for (int UpL = 0; UpL < sizeMap; UpL++)
                    if (posX - UpL >= 0 && posY + UpL < sizeMap)
                    {
                        if (posX - UpL == posX) continue;
                        if (_mapCP[posX - UpL, posY + UpL] == null)
                            avaibleMove.Add(new Vector2Int(posX - UpL, posY + UpL));

                        if (_mapCP[posX - UpL, posY + UpL] != null)
                        {
                            if (_mapCP[posX - UpL, posY + UpL].team != chessPiece.team)
                            {
                                avaibleMove.Add(new Vector2Int(posX - UpL, posY + UpL));
                                break;
                            }
                            if (_mapCP[posX - UpL, posY + UpL].team == chessPiece.team)
                                break;
                        }
                    }
                    else break;

                for (int DwL = 0; DwL < sizeMap; DwL++)
                    if (posX - DwL >= 0 && posY - DwL >= 0)
                    {
                        if (posX - DwL == posX && posY - DwL == posY) continue;
                        if (_mapCP[posX - DwL, posY - DwL] == null)
                            avaibleMove.Add(new Vector2Int(posX - DwL, posY - DwL));

                        if (_mapCP[posX - DwL, posY - DwL] != null)
                        {
                            if (_mapCP[posX - DwL, posY - DwL].team != chessPiece.team)
                            {
                                avaibleMove.Add(new Vector2Int(posX - DwL, posY - DwL));
                                break;
                            }
                            if (_mapCP[posX - DwL, posY - DwL].team == chessPiece.team)
                                break;
                        }
                    }
                    else break;

                break;
            #endregion

            #region King
            case ChessPieceType.King:

                //правый верхний
                if (posX + 1 < sizeMap && posY + 1 < sizeMap)
                {
                    if (_mapCP[posX + 1, posY + 1] == null)
                        avaibleMove.Add(new Vector2Int(posX + 1, posY + 1));
                    if (_mapCP[posX + 1, posY + 1] != null && _mapCP[posX + 1, posY + 1].team != chessPiece.team)
                        avaibleMove.Add(new Vector2Int(posX + 1, posY + 1));
                }
                //верх
                if (posY + 1 < sizeMap)
                {
                    if (_mapCP[posX, posY + 1] == null)
                        avaibleMove.Add(new Vector2Int(posX, posY + 1));
                    if (_mapCP[posX, posY + 1] != null && _mapCP[posX, posY + 1].team != chessPiece.team)
                        avaibleMove.Add(new Vector2Int(posX, posY + 1));
                }
                //право
                if (posX + 1 < sizeMap)
                {
                    if (_mapCP[posX + 1, posY] == null)
                        avaibleMove.Add(new Vector2Int(posX + 1, posY));
                    if (_mapCP[posX + 1, posY] != null && _mapCP[posX + 1, posY].team != chessPiece.team)
                        avaibleMove.Add(new Vector2Int(posX + 1, posY));
                }
                //левый нижний
                if (posX - 1 >= 0 && posY - 1 >= 0)
                {
                    if (_mapCP[posX - 1, posY - 1] == null)
                        avaibleMove.Add(new Vector2Int(posX - 1, posY - 1));
                    if (_mapCP[posX - 1, posY - 1] != null && _mapCP[posX - 1, posY - 1].team != chessPiece.team)
                        avaibleMove.Add(new Vector2Int(posX - 1, posY - 1));
                }
                //низ
                if (posY - 1 >= 0)
                {
                    if (_mapCP[posX, posY - 1] == null)
                        avaibleMove.Add(new Vector2Int(posX, posY - 1));
                    if (_mapCP[posX, posY - 1] != null && _mapCP[posX, posY - 1].team != chessPiece.team)
                        avaibleMove.Add(new Vector2Int(posX, posY - 1));
                }
                //лево
                if (posX - 1 >= 0)
                {
                    if (_mapCP[posX - 1, posY] == null)
                        avaibleMove.Add(new Vector2Int(posX - 1, posY));
                    if (_mapCP[posX - 1, posY] != null && _mapCP[posX - 1, posY].team != chessPiece.team)
                        avaibleMove.Add(new Vector2Int(posX - 1, posY));
                }
                //верхний левый
                if (posX - 1 >= 0 && posY + 1 < sizeMap)
                {
                    if (_mapCP[posX - 1, posY + 1] == null)
                        avaibleMove.Add(new Vector2Int(posX - 1, posY + 1));
                    if (_mapCP[posX - 1, posY + 1] != null && _mapCP[posX - 1, posY + 1].team != chessPiece.team)
                        avaibleMove.Add(new Vector2Int(posX - 1, posY + 1));
                }
                //правый нижний
                if (posX + 1 < sizeMap && posY - 1 >= 0)
                {
                    if (_mapCP[posX + 1, posY - 1] == null)
                        avaibleMove.Add(new Vector2Int(posX + 1, posY - 1));

                    if (_mapCP[posX + 1, posY - 1] != null && _mapCP[posX + 1, posY - 1].team != chessPiece.team)
                        avaibleMove.Add(new Vector2Int(posX + 1, posY - 1));
                }


                break;
                #endregion
        }

        for (int i = 0; i < avaibleMove.Count; i++)
            _arrayTile[avaibleMove[i].x, avaibleMove[i].y].OnHighlight();
    }
    private void ChangePositionPiece(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {
        for (int i = 0; i < avaibleMove.Count; i++)
            if (avaibleMove[i] == new Vector2Int(posChangeOnX, posChangeOnY))
            {
                //Сhecking for an enemy
                if (_mapCP[posChangeOnX, posChangeOnY] != null && _mapCP[posChangeOnX, posChangeOnY].team != chessPiece.team)
                {
                    //Checking which king was killed
                    if (_mapCP[posChangeOnX, posChangeOnY].type == ChessPieceType.King)
                        GameResult.onShowWhoWins.Invoke(chessPiece.team);
                    _mapCP[posChangeOnX, posChangeOnY].DestroyPiece();
                }

                //Change position current chessPiece
                _mapCP[chessPiece.currentPositionX, chessPiece.currentPositionY] = null;
                _mapCP[posChangeOnX, posChangeOnY] = chessPiece;
                _mapCP[posChangeOnX, posChangeOnY].transform.position = new Vector3(posChangeOnX, posChangeOnY, _positionPieceZ);
                chessPiece.currentPositionX = posChangeOnX;
                chessPiece.currentPositionY = posChangeOnY;

                //Checking for change type piece
                if (chessPiece.type == ChessPieceType.Pаwn && (chessPiece.currentPositionY == _mapCP.GetLength(1) - 1 || chessPiece.currentPositionY == 0))
                {
                    ChangePiece.onActiveChoose.Invoke(_mapCP, chessPiece);
                    Player.onStopSelection.Invoke();
                }
                break;
            }
    }
}