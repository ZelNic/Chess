using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    public override List<Vector2Int> ShowAviableMove(ChessPiece[,] _mapCP)
    {
        int direction = (team == 0) ? 1 : -1;
        int sizeMap = _mapCP.GetLength(0);
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

        //Right
        for (int rightX = currentPositionX; rightX < _mapCP.GetLength(0); rightX++)
        {
            if (rightX == currentPositionX) continue;
            if (_mapCP[rightX, currentPositionY] == null)
                avaibleMove.Add(new Vector2Int(rightX, currentPositionY));

            if (_mapCP[rightX, currentPositionY] != null)
            {
                if (_mapCP[rightX, currentPositionY].team != team)
                    avaibleMove.Add(new Vector2Int(rightX, currentPositionY));
                break;
            }
        }
        //Top
        for (int topY = currentPositionY; topY < _mapCP.GetLength(1); topY++)
        {
            if (topY == currentPositionY) continue;
            if (_mapCP[currentPositionX, topY] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX, topY));

            if (_mapCP[currentPositionX, topY] != null)
            {
                if (_mapCP[currentPositionX, topY].team != team)
                    avaibleMove.Add(new Vector2Int(currentPositionX, topY));
                break;
            }

        }
        //Bottom
        for (int leftX = currentPositionX; leftX >= 0; leftX--)
        {
            if (leftX == currentPositionX) continue;

            if (_mapCP[leftX, currentPositionY] == null)
                avaibleMove.Add(new Vector2Int(leftX, currentPositionY));

            if (_mapCP[leftX, currentPositionY] != null)
            {
                if (_mapCP[leftX, currentPositionY].team != team)
                    avaibleMove.Add(new Vector2Int(leftX, currentPositionY));
                break;
            }
        }
        //Left
        for (int ottomY = currentPositionY; ottomY >= 0; ottomY--)
        {
            if (ottomY == currentPositionY) continue;

            if (_mapCP[currentPositionX, ottomY] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX, ottomY));

            if (_mapCP[currentPositionX, ottomY] != null)
            {
                if (_mapCP[currentPositionX, ottomY].team != team)
                    avaibleMove.Add(new Vector2Int(currentPositionX, ottomY));
                break;
            }
        }

        //Diagonal
        //Right Upper
        for (int UpR = 0; UpR < sizeMap; UpR++)
            if (currentPositionX + UpR < sizeMap && currentPositionY + UpR < sizeMap)
            {
                if (currentPositionY + UpR == currentPositionY) continue;
                if (_mapCP[currentPositionX + UpR, currentPositionY + UpR] == null)
                    avaibleMove.Add(new Vector2Int(currentPositionX + UpR, currentPositionY + UpR));

                if (_mapCP[currentPositionX + UpR, currentPositionY + UpR] != null)
                {
                    if (_mapCP[currentPositionX + UpR, currentPositionY + UpR].team != team)
                    {
                        avaibleMove.Add(new Vector2Int(currentPositionX + UpR, currentPositionY + UpR));
                        break;
                    }
                    if (_mapCP[currentPositionX + UpR, currentPositionY + UpR].team == team)
                        break;
                }
            }
            else break;
        //Bottom Right
        for (int DwR = 0; DwR < sizeMap; DwR++)
            if (currentPositionX + DwR < sizeMap && currentPositionY - DwR >= 0)
            {
                if (currentPositionY - DwR == currentPositionY) continue;
                if (_mapCP[currentPositionX + DwR, currentPositionY - DwR] == null)
                    avaibleMove.Add(new Vector2Int(currentPositionX + DwR, currentPositionY - DwR));

                if (_mapCP[currentPositionX + DwR, currentPositionY - DwR] != null)
                {
                    if (_mapCP[currentPositionX + DwR, currentPositionY - DwR].team != team)
                    {
                        avaibleMove.Add(new Vector2Int(currentPositionX + DwR, currentPositionY - DwR));
                        break;
                    }
                    if (_mapCP[currentPositionX + DwR, currentPositionY - DwR].team == team)
                        break;
                }
            }
            else break;
        //Left Upper
        for (int UpL = 0; UpL < sizeMap; UpL++)
            if (currentPositionX - UpL >= 0 && currentPositionY + UpL < sizeMap)
            {
                if (currentPositionX - UpL == currentPositionX) continue;
                if (_mapCP[currentPositionX - UpL, currentPositionY + UpL] == null)
                    avaibleMove.Add(new Vector2Int(currentPositionX - UpL, currentPositionY + UpL));

                if (_mapCP[currentPositionX - UpL, currentPositionY + UpL] != null)
                {
                    if (_mapCP[currentPositionX - UpL, currentPositionY + UpL].team != team)
                    {
                        avaibleMove.Add(new Vector2Int(currentPositionX - UpL, currentPositionY + UpL));
                        break;
                    }
                    if (_mapCP[currentPositionX - UpL, currentPositionY + UpL].team == team)
                        break;
                }
            }
            else break;
        //Bottom Left
        for (int DwL = 0; DwL < sizeMap; DwL++)
            if (currentPositionX - DwL >= 0 && currentPositionY - DwL >= 0)
            {
                if (currentPositionX - DwL == currentPositionX && currentPositionY - DwL == currentPositionY) continue;
                if (_mapCP[currentPositionX - DwL, currentPositionY - DwL] == null)
                    avaibleMove.Add(new Vector2Int(currentPositionX - DwL, currentPositionY - DwL));

                if (_mapCP[currentPositionX - DwL, currentPositionY - DwL] != null)
                {
                    if (_mapCP[currentPositionX - DwL, currentPositionY - DwL].team != team)
                    {
                        avaibleMove.Add(new Vector2Int(currentPositionX - DwL, currentPositionY - DwL));
                        break;
                    }
                    if (_mapCP[currentPositionX - DwL, currentPositionY - DwL].team == team)
                        break;
                }
            }
            else break;

        return avaibleMove;
    }
}
