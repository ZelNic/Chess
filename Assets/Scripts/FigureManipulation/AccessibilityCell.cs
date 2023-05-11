using System.Collections.Generic;
using UnityEngine;

public class AccessibilityCell : MonoBehaviour
{
    private int posX;
    private int posY;
    private int sizeMap;

    private void OnEnable() => Distributor.onShowAviableMoves += ShowAvailableMoves;

    private List<Vector2Int> ShowAvailableMoves(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        sizeMap = _mapCP.GetLength(0);
        posX = chessPiece.currentPositionX;
        posY = chessPiece.currentPositionY;
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

        switch (chessPiece.type)
        {
            case ChessPieceType.Pawn: avaibleMove = ShowAvailableMovesPawn(_mapCP, chessPiece); break;
            case ChessPieceType.Rook: avaibleMove = ShowAvailableMovesRock(_mapCP, chessPiece); break;
            case ChessPieceType.Knight: avaibleMove = ShowAvailableMovesKnight(_mapCP, chessPiece); break;
            case ChessPieceType.Bishop: avaibleMove = ShowAvailableMovesBishop(_mapCP, chessPiece); break;
            case ChessPieceType.Queen: avaibleMove = ShowAvailableMovesQueen(_mapCP, chessPiece); break;
            case ChessPieceType.King: avaibleMove = ShowAvailableMovesKing(_mapCP, chessPiece); break;
        }
        return avaibleMove;
    }
    private List<Vector2Int> ShowAvailableMovesPawn(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        int direction = (chessPiece.team == 0) ? 1 : -1;
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

        //Move on one tile
        if (posY + direction < sizeMap && posY + direction >= 0 && _mapCP[posX, posY + direction] == null)
            avaibleMove.Add(new Vector2Int(posX, posY + direction));

        //Move on two tile
        if ((posY == 1 && direction == 1) || (posY == 6 && direction == -1))
            if (_mapCP[posX, posY + direction] == null)
                if (_mapCP[posX, posY + (direction * 2)] == null)
                    avaibleMove.Add(new Vector2Int(posX, posY + (direction * 2)));

        //Kill Move
        if (posX + 1 < sizeMap && posY + direction >= 0 && posY + direction < sizeMap)
            if (_mapCP[posX + 1, posY + direction] != null)
                if (_mapCP[posX + 1, posY + direction].team != chessPiece.team)
                    avaibleMove.Add(new Vector2Int(posX + 1, posY + direction));

        if (posX == sizeMap - 1)
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
        return avaibleMove;
    }
    private List<Vector2Int> ShowAvailableMovesRock(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

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

        return avaibleMove;
    }
    private List<Vector2Int> ShowAvailableMovesKnight(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        List<Vector2Int> avaibleMove = new List<Vector2Int>();
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

        return avaibleMove;
    }
    private List<Vector2Int> ShowAvailableMovesBishop(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

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

        return avaibleMove;
    }
    private List<Vector2Int> ShowAvailableMovesQueen(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        List<Vector2Int> avaibleMove = new List<Vector2Int>();
        avaibleMove = ShowAvailableMovesRock(_mapCP, chessPiece);
        avaibleMove.AddRange(ShowAvailableMovesBishop(_mapCP, chessPiece));
        return avaibleMove;
    }
    private List<Vector2Int> ShowAvailableMovesKing(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

        //Left upper
        if (posX - 1 >= 0 && posY + 1 < sizeMap)
        {
            if (_mapCP[posX - 1, posY + 1] == null)
                avaibleMove.Add(new Vector2Int(posX - 1, posY + 1));
            if (_mapCP[posX - 1, posY + 1] != null && _mapCP[posX - 1, posY + 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(posX - 1, posY + 1));
        }
        //Top
        if (posY + 1 < sizeMap)
        {
            if (_mapCP[posX, posY + 1] == null)
                avaibleMove.Add(new Vector2Int(posX, posY + 1));
            if (_mapCP[posX, posY + 1] != null && _mapCP[posX, posY + 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(posX, posY + 1));
        }
        //Right upper
        if (posX + 1 < sizeMap && posY + 1 < sizeMap)
        {
            if (_mapCP[posX + 1, posY + 1] == null)
                avaibleMove.Add(new Vector2Int(posX + 1, posY + 1));
            if (_mapCP[posX + 1, posY + 1] != null && _mapCP[posX + 1, posY + 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(posX + 1, posY + 1));
        }

        //Left
        if (posX - 1 >= 0)
        {
            if (_mapCP[posX - 1, posY] == null)
                avaibleMove.Add(new Vector2Int(posX - 1, posY));
            if (_mapCP[posX - 1, posY] != null && _mapCP[posX - 1, posY].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(posX - 1, posY));
        }
        //Right
        if (posX + 1 < sizeMap)
        {
            if (_mapCP[posX + 1, posY] == null)
                avaibleMove.Add(new Vector2Int(posX + 1, posY));
            if (_mapCP[posX + 1, posY] != null && _mapCP[posX + 1, posY].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(posX + 1, posY));
        }

        //Bottom left
        if (posX - 1 >= 0 && posY - 1 >= 0)
        {
            if (_mapCP[posX - 1, posY - 1] == null)
                avaibleMove.Add(new Vector2Int(posX - 1, posY - 1));
            if (_mapCP[posX - 1, posY - 1] != null && _mapCP[posX - 1, posY - 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(posX - 1, posY - 1));
        }
        //Bottom
        if (posY - 1 >= 0)
        {
            if (_mapCP[posX, posY - 1] == null)
                avaibleMove.Add(new Vector2Int(posX, posY - 1));
            if (_mapCP[posX, posY - 1] != null && _mapCP[posX, posY - 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(posX, posY - 1));
        }
        //Bottom right
        if (posX + 1 < sizeMap && posY - 1 >= 0)
        {
            if (_mapCP[posX + 1, posY - 1] == null)
                avaibleMove.Add(new Vector2Int(posX + 1, posY - 1));

            if (_mapCP[posX + 1, posY - 1] != null && _mapCP[posX + 1, posY - 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(posX + 1, posY - 1));
        }

        //Castling with Rock
        if (chessPiece.GetComponent<King>().FirstStep == true)
        {
            for (int x = posX - 1; x >= 0; x--)
            {
                if (_mapCP[x, posY] != null)
                    break;
                if (x == 1 && _mapCP[x - 1, posY].type == ChessPieceType.Rook && _mapCP[x - 1, posY].GetComponent<Rock>().FirstStep == true)
                    avaibleMove.Add(new Vector2Int(2, posY));
            }

            for (int x = posX + 1; x < sizeMap - 1; x++)
            {
                if (_mapCP[x, posY] != null)
                    break;
                if (x == sizeMap - 2 && _mapCP[sizeMap - 1, posY].type == ChessPieceType.Rook && _mapCP[sizeMap - 1, posY].GetComponent<Rock>().FirstStep == true)
                    avaibleMove.Add(new Vector2Int(sizeMap - 2, posY));
            }
        }

        return avaibleMove;
    }
}
