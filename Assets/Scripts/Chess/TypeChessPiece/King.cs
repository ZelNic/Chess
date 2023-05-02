using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public override List<Vector2Int> ShowAviableMove(ChessPiece[,] _mapCP)
    {
        int direction = (team == 0) ? 1 : -1;
        int sizeMap = _mapCP.GetLength(0);
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

        //Left upper
        if (currentPositionX - 1 >= 0 && currentPositionY + 1 < sizeMap)
        {
            if (_mapCP[currentPositionX - 1, currentPositionY + 1] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX - 1, currentPositionY + 1));
            if (_mapCP[currentPositionX - 1, currentPositionY + 1] != null && _mapCP[currentPositionX - 1, currentPositionY + 1].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX - 1, currentPositionY + 1));
        }
        //Top
        if (currentPositionY + 1 < sizeMap)
        {
            if (_mapCP[currentPositionX, currentPositionY + 1] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX, currentPositionY + 1));
            if (_mapCP[currentPositionX, currentPositionY + 1] != null && _mapCP[currentPositionX, currentPositionY + 1].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX, currentPositionY + 1));
        }
        //Right upper
        if (currentPositionX + 1 < sizeMap && currentPositionY + 1 < sizeMap)
        {
            if (_mapCP[currentPositionX + 1, currentPositionY + 1] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX + 1, currentPositionY + 1));
            if (_mapCP[currentPositionX + 1, currentPositionY + 1] != null && _mapCP[currentPositionX + 1, currentPositionY + 1].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX + 1, currentPositionY + 1));
        }
       
        //Left
        if (currentPositionX - 1 >= 0)
        {
            if (_mapCP[currentPositionX - 1, currentPositionY] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX - 1, currentPositionY));
            if (_mapCP[currentPositionX - 1, currentPositionY] != null && _mapCP[currentPositionX - 1, currentPositionY].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX - 1, currentPositionY));
        }
        //Right
        if (currentPositionX + 1 < sizeMap)
        {
            if (_mapCP[currentPositionX + 1, currentPositionY] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX + 1, currentPositionY));
            if (_mapCP[currentPositionX + 1, currentPositionY] != null && _mapCP[currentPositionX + 1, currentPositionY].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX + 1, currentPositionY));
        }
       
        //Bottom left
        if (currentPositionX - 1 >= 0 && currentPositionY - 1 >= 0)
        {
            if (_mapCP[currentPositionX - 1, currentPositionY - 1] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX - 1, currentPositionY - 1));
            if (_mapCP[currentPositionX - 1, currentPositionY - 1] != null && _mapCP[currentPositionX - 1, currentPositionY - 1].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX - 1, currentPositionY - 1));
        }
        //Bottom
        if (currentPositionY - 1 >= 0)
        {
            if (_mapCP[currentPositionX, currentPositionY - 1] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX, currentPositionY - 1));
            if (_mapCP[currentPositionX, currentPositionY - 1] != null && _mapCP[currentPositionX, currentPositionY - 1].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX, currentPositionY - 1));
        }   
        //Bottom right
        if (currentPositionX + 1 < sizeMap && currentPositionY - 1 >= 0)
        {
            if (_mapCP[currentPositionX + 1, currentPositionY - 1] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX + 1, currentPositionY - 1));

            if (_mapCP[currentPositionX + 1, currentPositionY - 1] != null && _mapCP[currentPositionX + 1, currentPositionY - 1].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX + 1, currentPositionY - 1));
        }
       
        return avaibleMove;
    }
}
