using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class King : ChessPiece
{
    private bool _firstStep = true;
    public bool FirstStep
    {
        get { return _firstStep; }
        private set { _firstStep = value; }
    }
    public void MakeMove()
    {
        FirstStep = false;
    }
    public override List<Vector2Int> ShowAviableMove(ChessPiece[,] _mapCP)
    {
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

        //Castling with Rock
        if (FirstStep == true)
        {
            for (int x = currentPositionX - 1; x >= 0; x--)
            {
                if (_mapCP[x, currentPositionY] != null)
                    break;
                if (x == 1 && _mapCP[x - 1, currentPositionY].type == ChessPieceType.Rook && _mapCP[x - 1, currentPositionY].GetComponent<Rock>().FirstStep == true) { }
                    avaibleMove.Add(new Vector2Int(2, currentPositionY));
            }

            for (int x = currentPositionX + 1; x < sizeMap - 1; x++)
            {
                if (_mapCP[x, currentPositionY] != null)
                    break;
                if (x == sizeMap - 2 && _mapCP[sizeMap - 1, currentPositionY].type == ChessPieceType.Rook && _mapCP[sizeMap - 1, currentPositionY].GetComponent<Rock>().FirstStep == true)
                    avaibleMove.Add(new Vector2Int(sizeMap - 2, currentPositionY));
            }
        }
        
        return avaibleMove;
    }
}
