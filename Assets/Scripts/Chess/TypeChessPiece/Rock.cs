using System.Collections.Generic;
using UnityEngine;

public class Rock : ChessPiece
{
    private bool _firstStep = true;
    public bool FirstStep
    {
        get { return _firstStep; }
        private set { _firstStep = value; }
    }
    private void MakeMove()
    {
        FirstStep = false;
    }

    public override List<Vector2Int> ShowAviableMove(ChessPiece[,] _mapCP)
    {
        int sizeMap = _mapCP.GetLength(0);
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

        for (int x = currentPositionX; x < _mapCP.GetLength(0); x++)
        {
            if (x == currentPositionX) continue;
            if (_mapCP[x, currentPositionY] == null)
                avaibleMove.Add(new Vector2Int(x, currentPositionY));

            if (_mapCP[x, currentPositionY] != null)
            {
                if (_mapCP[x, currentPositionY].team != team)
                    avaibleMove.Add(new Vector2Int(x, currentPositionY));
                break;
            }
        }

        for (int y = currentPositionY; y < _mapCP.GetLength(1); y++)
        {
            if (y == currentPositionY) continue;
            if (_mapCP[currentPositionX, y] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX, y));

            if (_mapCP[currentPositionX, y] != null)
            {
                if (_mapCP[currentPositionX, y].team != team)
                    avaibleMove.Add(new Vector2Int(currentPositionX, y));
                break;
            }

        }

        for (int k = currentPositionX; k >= 0; k--)
        {
            if (k == currentPositionX) continue;

            if (_mapCP[k, currentPositionY] == null)
                avaibleMove.Add(new Vector2Int(k, currentPositionY));

            if (_mapCP[k, currentPositionY] != null)
            {
                if (_mapCP[k, currentPositionY].team != team)
                    avaibleMove.Add(new Vector2Int(k, currentPositionY));
                break;
            }
        }

        for (int j = currentPositionY; j >= 0; j--)
        {
            if (j == currentPositionY) continue;

            if (_mapCP[currentPositionX, j] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX, j));

            if (_mapCP[currentPositionX, j] != null)
            {
                if (_mapCP[currentPositionX, j].team != team)
                    avaibleMove.Add(new Vector2Int(currentPositionX, j));
                break;
            }
        }

        //Castling for white
        if (FirstStep == true)
        {
            for (int x = currentPositionX; x == 3; x++)
            {
                if (_mapCP[x, currentPositionY] != null)
                    break;
                if (x == 3 && _mapCP[4, currentPositionY].type == ChessPieceType.King && _mapCP[4, currentPositionY].GetComponent<King>().FirstStep == true)
                    avaibleMove.Add(new Vector2Int(3, currentPositionY));
            }

            for (int x = currentPositionX; x == sizeMap - 3; x--)
            {
                if (_mapCP[x, currentPositionY] != null)
                    break;
                if (x == sizeMap - 3 && _mapCP[4, currentPositionY].type == ChessPieceType.King && _mapCP[4, currentPositionY].GetComponent<King>().FirstStep == true)
                    avaibleMove.Add(new Vector2Int(sizeMap - 3, 0));
            }
        }
        return avaibleMove;
    }
}
