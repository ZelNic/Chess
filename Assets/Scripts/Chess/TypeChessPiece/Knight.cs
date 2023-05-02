using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public override List<Vector2Int> ShowAviableMove(ChessPiece[,] _mapCP)
    {        
        int sizeMap = _mapCP.GetLength(0);
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

        //UpR
        if (currentPositionX + 1 < sizeMap && currentPositionY + 2 < sizeMap)
            if (_mapCP[currentPositionX + 1, currentPositionY + 2] == null ||
                _mapCP[currentPositionX + 1, currentPositionY + 2] != null && _mapCP[currentPositionX + 1, currentPositionY + 2].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX + 1, currentPositionY + 2));

        if (currentPositionX + 2 < sizeMap && currentPositionY + 1 < sizeMap)
            if (_mapCP[currentPositionX + 2, currentPositionY + 1] == null ||
                _mapCP[currentPositionX + 2, currentPositionY + 1] != null && _mapCP[currentPositionX + 2, currentPositionY + 1].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX + 2, currentPositionY + 1));

        //UpL
        if (currentPositionX - 1 >= 0 && currentPositionY + 2 < sizeMap)
            if (_mapCP[currentPositionX - 1, currentPositionY + 2] == null ||
                _mapCP[currentPositionX - 1, currentPositionY + 2] != null && _mapCP[currentPositionX - 1, currentPositionY + 2].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX - 1, currentPositionY + 2));

        if (currentPositionX - 2 >= 0 && currentPositionY + 1 < sizeMap)
            if (_mapCP[currentPositionX - 2, currentPositionY + 1] == null ||
                _mapCP[currentPositionX - 2, currentPositionY + 1] != null && _mapCP[currentPositionX - 2, currentPositionY + 1].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX - 2, currentPositionY + 1));

        //DwR
        if (currentPositionX + 1 < sizeMap && currentPositionY - 2 >= 0)
            if (_mapCP[currentPositionX + 1, currentPositionY - 2] == null ||
                _mapCP[currentPositionX + 1, currentPositionY - 2] != null && _mapCP[currentPositionX + 1, currentPositionY - 2].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX + 1, currentPositionY - 2));

        if (currentPositionX + 2 < sizeMap && currentPositionY - 1 >= 0)
            if (_mapCP[currentPositionX + 2, currentPositionY - 1] == null ||
                _mapCP[currentPositionX + 2, currentPositionY - 1] != null && _mapCP[currentPositionX + 2, currentPositionY - 1].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX + 2, currentPositionY - 1));

        //DwL
        if (currentPositionX - 1 >= 0 && currentPositionY - 2 >= 0)
            if (_mapCP[currentPositionX - 1, currentPositionY - 2] == null ||
                _mapCP[currentPositionX - 1, currentPositionY - 2] != null && _mapCP[currentPositionX - 1, currentPositionY - 2].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX - 1, currentPositionY - 2));
        if (currentPositionX - 2 >= 0 && currentPositionY - 1 >= 0)
            if (_mapCP[currentPositionX - 2, currentPositionY - 1] == null ||
               _mapCP[currentPositionX - 2, currentPositionY - 1] != null && _mapCP[currentPositionX - 2, currentPositionY - 1].team != team)
                avaibleMove.Add(new Vector2Int(currentPositionX - 2, currentPositionY - 1));    

        return avaibleMove;
    }
}

