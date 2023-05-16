using System.Collections.Generic;
using UnityEngine;

public class AccessibilityMove : MonoBehaviour
{
    private int _posX;
    private int _posY;
    private int _sizeMap;

    private void OnEnable() => Distributor.onShowAviableMoves += ShowAvailableMoves;

    private List<Vector2Int> ShowAvailableMoves(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        _sizeMap = _mapCP.GetLength(0);
        _posX = chessPiece.currentPositionX;
        _posY = chessPiece.currentPositionY;
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
        if (_posY + direction < _sizeMap && _posY + direction >= 0 && _mapCP[_posX, _posY + direction] == null)
            avaibleMove.Add(new Vector2Int(_posX, _posY + direction));

        //Move on two tile
        if ((_posY == 1 && direction == 1) || (_posY == 6 && direction == -1))
            if (_mapCP[_posX, _posY + direction] == null)
                if (_mapCP[_posX, _posY + (direction * 2)] == null)
                    avaibleMove.Add(new Vector2Int(_posX, _posY + (direction * 2)));

        //Kill Move
        if (_posX + 1 < _sizeMap && _posY + direction >= 0 && _posY + direction < _sizeMap)
            if (_mapCP[_posX + 1, _posY + direction] != null)
                if (_mapCP[_posX + 1, _posY + direction].team != chessPiece.team)
                    avaibleMove.Add(new Vector2Int(_posX + 1, _posY + direction));

        if (_posX == _sizeMap - 1)
            if (_mapCP[_posX - 1, _posY + direction] != null)
                if (_mapCP[_posX - 1, _posY + direction].team != chessPiece.team)
                    avaibleMove.Add(new Vector2Int(_posX - 1, _posY + direction));

        if (_posX - 1 >= 0 && _posY + direction < _sizeMap && _posY + direction >= 0)
            if (_mapCP[_posX - 1, _posY + direction] != null)
                if (_mapCP[_posX - 1, _posY + direction].team != chessPiece.team)
                    avaibleMove.Add(new Vector2Int(_posX - 1, _posY + direction));

        if (_posX + 1 < _sizeMap && _posX + direction < _sizeMap && _posY + direction < _sizeMap && _posY + direction >= 0)
            if (_mapCP[_posX + 1, _posY + direction] != null)
                if (_mapCP[_posX + 1, _posY + direction].team != chessPiece.team)
                    avaibleMove.Add(new Vector2Int(_posX + 1, _posY + direction));
        return avaibleMove;
    }
    private List<Vector2Int> ShowAvailableMovesRock(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

        for (int x = _posX; x < _mapCP.GetLength(0); x++)
        {
            if (x == _posX) continue;
            if (_mapCP[x, _posY] == null)
                avaibleMove.Add(new Vector2Int(x, _posY));

            if (_mapCP[x, _posY] != null)
            {
                if (_mapCP[x, _posY].team != chessPiece.team)
                    avaibleMove.Add(new Vector2Int(x, _posY));
                break;
            }
        }

        for (int y = _posY; y < _mapCP.GetLength(1); y++)
        {
            if (y == _posY) continue;
            if (_mapCP[_posX, y] == null)
                avaibleMove.Add(new Vector2Int(_posX, y));

            if (_mapCP[_posX, y] != null)
            {
                if (_mapCP[_posX, y].team != chessPiece.team)
                    avaibleMove.Add(new Vector2Int(_posX, y));
                break;
            }

        }

        for (int k = _posX; k >= 0; k--)
        {
            if (k == _posX) continue;

            if (_mapCP[k, _posY] == null)
                avaibleMove.Add(new Vector2Int(k, _posY));

            if (_mapCP[k, _posY] != null)
            {
                if (_mapCP[k, _posY].team != chessPiece.team)
                    avaibleMove.Add(new Vector2Int(k, _posY));
                break;
            }
        }

        for (int j = _posY; j >= 0; j--)
        {
            if (j == _posY) continue;

            if (_mapCP[_posX, j] == null)
                avaibleMove.Add(new Vector2Int(_posX, j));

            if (_mapCP[_posX, j] != null)
            {
                if (_mapCP[_posX, j].team != chessPiece.team)
                    avaibleMove.Add(new Vector2Int(_posX, j));
                break;
            }
        }

        return avaibleMove;
    }
    private List<Vector2Int> ShowAvailableMovesKnight(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        List<Vector2Int> avaibleMove = new List<Vector2Int>();
        //UpR
        if (_posX + 1 < _sizeMap && _posY + 2 < _sizeMap)
            if (_mapCP[_posX + 1, _posY + 2] == null ||
                _mapCP[_posX + 1, _posY + 2] != null && _mapCP[_posX + 1, _posY + 2].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX + 1, _posY + 2));

        if (_posX + 2 < _sizeMap && _posY + 1 < _sizeMap)
            if (_mapCP[_posX + 2, _posY + 1] == null ||
                _mapCP[_posX + 2, _posY + 1] != null && _mapCP[_posX + 2, _posY + 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX + 2, _posY + 1));

        //UpL
        if (_posX - 1 >= 0 && _posY + 2 < _sizeMap)
            if (_mapCP[_posX - 1, _posY + 2] == null ||
                _mapCP[_posX - 1, _posY + 2] != null && _mapCP[_posX - 1, _posY + 2].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX - 1, _posY + 2));

        if (_posX - 2 >= 0 && _posY + 1 < _sizeMap)
            if (_mapCP[_posX - 2, _posY + 1] == null ||
                _mapCP[_posX - 2, _posY + 1] != null && _mapCP[_posX - 2, _posY + 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX - 2, _posY + 1));

        //DwR
        if (_posX + 1 < _sizeMap && _posY - 2 >= 0)
            if (_mapCP[_posX + 1, _posY - 2] == null ||
                _mapCP[_posX + 1, _posY - 2] != null && _mapCP[_posX + 1, _posY - 2].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX + 1, _posY - 2));

        if (_posX + 2 < _sizeMap && _posY - 1 >= 0)
            if (_mapCP[_posX + 2, _posY - 1] == null ||
                _mapCP[_posX + 2, _posY - 1] != null && _mapCP[_posX + 2, _posY - 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX + 2, _posY - 1));

        //DwL
        if (_posX - 1 >= 0 && _posY - 2 >= 0)
            if (_mapCP[_posX - 1, _posY - 2] == null ||
                _mapCP[_posX - 1, _posY - 2] != null && _mapCP[_posX - 1, _posY - 2].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX - 1, _posY - 2));
        if (_posX - 2 >= 0 && _posY - 1 >= 0)
            if (_mapCP[_posX - 2, _posY - 1] == null ||
               _mapCP[_posX - 2, _posY - 1] != null && _mapCP[_posX - 2, _posY - 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX - 2, _posY - 1));

        return avaibleMove;
    }
    private List<Vector2Int> ShowAvailableMovesBishop(ChessPiece[,] _mapCP, ChessPiece chessPiece)
    {
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

        for (int UpR = 0; UpR < _sizeMap; UpR++)
            if (_posX + UpR < _sizeMap && _posY + UpR < _sizeMap)
            {
                if (_posY + UpR == _posY) continue;
                if (_mapCP[_posX + UpR, _posY + UpR] == null)
                    avaibleMove.Add(new Vector2Int(_posX + UpR, _posY + UpR));

                if (_mapCP[_posX + UpR, _posY + UpR] != null)
                {
                    if (_mapCP[_posX + UpR, _posY + UpR].team != chessPiece.team)
                    {
                        avaibleMove.Add(new Vector2Int(_posX + UpR, _posY + UpR));
                        break;
                    }
                    if (_mapCP[_posX + UpR, _posY + UpR].team == chessPiece.team)
                        break;
                }
            }
            else break;

        for (int DwR = 0; DwR < _sizeMap; DwR++)
            if (_posX + DwR < _sizeMap && _posY - DwR >= 0)
            {
                if (_posY - DwR == _posY) continue;
                if (_mapCP[_posX + DwR, _posY - DwR] == null)
                    avaibleMove.Add(new Vector2Int(_posX + DwR, _posY - DwR));

                if (_mapCP[_posX + DwR, _posY - DwR] != null)
                {
                    if (_mapCP[_posX + DwR, _posY - DwR].team != chessPiece.team)
                    {
                        avaibleMove.Add(new Vector2Int(_posX + DwR, _posY - DwR));
                        break;
                    }
                    if (_mapCP[_posX + DwR, _posY - DwR].team == chessPiece.team)
                        break;
                }
            }
            else break;

        for (int UpL = 0; UpL < _sizeMap; UpL++)
            if (_posX - UpL >= 0 && _posY + UpL < _sizeMap)
            {
                if (_posX - UpL == _posX) continue;
                if (_mapCP[_posX - UpL, _posY + UpL] == null)
                    avaibleMove.Add(new Vector2Int(_posX - UpL, _posY + UpL));

                if (_mapCP[_posX - UpL, _posY + UpL] != null)
                {
                    if (_mapCP[_posX - UpL, _posY + UpL].team != chessPiece.team)
                    {
                        avaibleMove.Add(new Vector2Int(_posX - UpL, _posY + UpL));
                        break;
                    }
                    if (_mapCP[_posX - UpL, _posY + UpL].team == chessPiece.team)
                        break;
                }
            }
            else break;

        for (int DwL = 0; DwL < _sizeMap; DwL++)
            if (_posX - DwL >= 0 && _posY - DwL >= 0)
            {
                if (_posX - DwL == _posX && _posY - DwL == _posY) continue;
                if (_mapCP[_posX - DwL, _posY - DwL] == null)
                    avaibleMove.Add(new Vector2Int(_posX - DwL, _posY - DwL));

                if (_mapCP[_posX - DwL, _posY - DwL] != null)
                {
                    if (_mapCP[_posX - DwL, _posY - DwL].team != chessPiece.team)
                    {
                        avaibleMove.Add(new Vector2Int(_posX - DwL, _posY - DwL));
                        break;
                    }
                    if (_mapCP[_posX - DwL, _posY - DwL].team == chessPiece.team)
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
        if (_posX - 1 >= 0 && _posY + 1 < _sizeMap)
        {
            if (_mapCP[_posX - 1, _posY + 1] == null)
                avaibleMove.Add(new Vector2Int(_posX - 1, _posY + 1));
            if (_mapCP[_posX - 1, _posY + 1] != null && _mapCP[_posX - 1, _posY + 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX - 1, _posY + 1));
        }
        //Top
        if (_posY + 1 < _sizeMap)
        {
            if (_mapCP[_posX, _posY + 1] == null)
                avaibleMove.Add(new Vector2Int(_posX, _posY + 1));
            if (_mapCP[_posX, _posY + 1] != null && _mapCP[_posX, _posY + 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX, _posY + 1));
        }
        //Right upper
        if (_posX + 1 < _sizeMap && _posY + 1 < _sizeMap)
        {
            if (_mapCP[_posX + 1, _posY + 1] == null)
                avaibleMove.Add(new Vector2Int(_posX + 1, _posY + 1));
            if (_mapCP[_posX + 1, _posY + 1] != null && _mapCP[_posX + 1, _posY + 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX + 1, _posY + 1));
        }

        //Left
        if (_posX - 1 >= 0)
        {
            if (_mapCP[_posX - 1, _posY] == null)
                avaibleMove.Add(new Vector2Int(_posX - 1, _posY));
            if (_mapCP[_posX - 1, _posY] != null && _mapCP[_posX - 1, _posY].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX - 1, _posY));
        }
        //Right
        if (_posX + 1 < _sizeMap)
        {
            if (_mapCP[_posX + 1, _posY] == null)
                avaibleMove.Add(new Vector2Int(_posX + 1, _posY));
            if (_mapCP[_posX + 1, _posY] != null && _mapCP[_posX + 1, _posY].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX + 1, _posY));
        }

        //Bottom left
        if (_posX - 1 >= 0 && _posY - 1 >= 0)
        {
            if (_mapCP[_posX - 1, _posY - 1] == null)
                avaibleMove.Add(new Vector2Int(_posX - 1, _posY - 1));
            if (_mapCP[_posX - 1, _posY - 1] != null && _mapCP[_posX - 1, _posY - 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX - 1, _posY - 1));
        }
        //Bottom
        if (_posY - 1 >= 0)
        {
            if (_mapCP[_posX, _posY - 1] == null)
                avaibleMove.Add(new Vector2Int(_posX, _posY - 1));
            if (_mapCP[_posX, _posY - 1] != null && _mapCP[_posX, _posY - 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX, _posY - 1));
        }
        //Bottom right
        if (_posX + 1 < _sizeMap && _posY - 1 >= 0)
        {
            if (_mapCP[_posX + 1, _posY - 1] == null)
                avaibleMove.Add(new Vector2Int(_posX + 1, _posY - 1));

            if (_mapCP[_posX + 1, _posY - 1] != null && _mapCP[_posX + 1, _posY - 1].team != chessPiece.team)
                avaibleMove.Add(new Vector2Int(_posX + 1, _posY - 1));
        }

        //Castling with Rock
        if (chessPiece.GetComponent<King>().IsFirstStep == true)
        {
            for (int x = _posX - 1; x >= 0; x--)
            {
                if (_mapCP[x, _posY] != null)
                    break;
                if (x == 1 && _mapCP[x - 1, _posY].type == ChessPieceType.Rook && _mapCP[x - 1, _posY].GetComponent<Rook>().IsFirstStep == true)
                    avaibleMove.Add(new Vector2Int(2, _posY));
            }

            for (int x = _posX + 1; x < _sizeMap - 1; x++)
            {
                if (_mapCP[x, _posY] != null)
                    break;
                if (x == _sizeMap - 2 && _mapCP[_sizeMap - 1, _posY].type == ChessPieceType.Rook && _mapCP[_sizeMap - 1, _posY].GetComponent<Rook>().IsFirstStep == true)
                    avaibleMove.Add(new Vector2Int(_sizeMap - 2, _posY));
            }
        }

        return avaibleMove;
    }
}
