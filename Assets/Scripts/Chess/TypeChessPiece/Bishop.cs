using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    public override List<Vector2Int> ShowAviableMove(ChessPiece[,] _mapCP)
    {
        int direction = (team == 0) ? 1 : -1;
        int sizeMap = _mapCP.GetLength(0);
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

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
