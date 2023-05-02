using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    public override List<Vector2Int> ShowAviableMove(ChessPiece[,] _mapCP)
    {
        int direction = (team == 0) ? 1 : -1;
        int sizeMap = _mapCP.GetLength(0);
        List<Vector2Int> avaibleMove = new List<Vector2Int>();

        //Move on one tile
        if (currentPositionY + direction < sizeMap && currentPositionY + direction >= 0 && _mapCP[currentPositionX, currentPositionY + direction] == null)
            avaibleMove.Add(new Vector2Int(currentPositionX, currentPositionY + direction));

        //Move on two tile
        if ((currentPositionY == 1 && direction == 1) || (currentPositionY == 6 && direction == -1))
            if (_mapCP[currentPositionX, currentPositionY + (direction * 2)] == null)
                avaibleMove.Add(new Vector2Int(currentPositionX, currentPositionY + (direction * 2)));

        //Kill Move
        if (currentPositionX + 1 < sizeMap && currentPositionY + direction >= 0 && currentPositionY + direction < sizeMap)
            if (_mapCP[currentPositionX + 1, currentPositionY + direction] != null)
                if (_mapCP[currentPositionX + 1, currentPositionY + direction].team != team)
                    avaibleMove.Add(new Vector2Int(currentPositionX + 1, currentPositionY + direction));

        if (currentPositionX == _mapCP.GetLength(0) - 1)
            if (_mapCP[currentPositionX - 1, currentPositionY + direction] != null)
                if (_mapCP[currentPositionX - 1, currentPositionY + direction].team != team)
                    avaibleMove.Add(new Vector2Int(currentPositionX - 1, currentPositionY + direction));

        if (currentPositionX - 1 >= 0 && currentPositionY + direction < sizeMap && currentPositionY + direction >= 0)
            if (_mapCP[currentPositionX - 1, currentPositionY + direction] != null)
                if (_mapCP[currentPositionX - 1, currentPositionY + direction].team != team)
                    avaibleMove.Add(new Vector2Int(currentPositionX - 1, currentPositionY + direction));

        if (currentPositionX + 1 < sizeMap && currentPositionX + direction < sizeMap && currentPositionY + direction < sizeMap && currentPositionY + direction >= 0)
            if (_mapCP[currentPositionX + 1, currentPositionY + direction] != null)
                if (_mapCP[currentPositionX + 1, currentPositionY + direction].team != team)
                    avaibleMove.Add(new Vector2Int(currentPositionX + 1, currentPositionY + direction));

        return avaibleMove;
    }
}
