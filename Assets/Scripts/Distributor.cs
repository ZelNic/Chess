using System;
using System.Collections.Generic;
using UnityEngine;

public class Distributor : MonoBehaviour
{
    public static Action<ChessPiece[,]> onStartDistribution;
    public static Action<ChessPiece, int, int> onChangePositionPiece;
    public static Action<ChessPiece, int, int> onSwonAviableMoves;
    private ChessPiece[,] _coordinatePiece;
    private readonly int _positionPieceZ = -5;
    private List<Vector2Int> avaibleMove;


    private void OnEnable()
    {
        onStartDistribution += Distribute;
        onChangePositionPiece += ChangePositionPiece;
        onSwonAviableMoves += ShowAviableMoves;
    }

    public void Distribute(ChessPiece[,] chessPieces)
    {
        _coordinatePiece = chessPieces;
        for (int x = 0; x < _coordinatePiece.GetLength(0); x++)
            for (int y = 0; y < _coordinatePiece.GetLength(1); y++)
                if (_coordinatePiece[x, y] != null)
                    SetOnPlace(x, y);
    }
    private void SetOnPlace(int x, int y)
    {
        _coordinatePiece[x, y].currentPositionX = x;
        _coordinatePiece[x, y].currentPositionY = y;
        _coordinatePiece[x, y].transform.position = new Vector3(x, y, _positionPieceZ);
    }

    private void ShowAviableMoves(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {

        ChessPieceType type = chessPiece.type;
        avaibleMove = new List<Vector2Int>();
        Tile[,] _arrayTile = BoardCreator.onSendArrayTile?.Invoke();
        
        switch (type)
        {
            case ChessPieceType.Pown:

                int direction = (chessPiece.team == 0) ? 1 : -1;

                if (_coordinatePiece[chessPiece.currentPositionX, chessPiece.currentPositionY + direction] == null)
                    avaibleMove.Add(new Vector2Int(chessPiece.currentPositionX, chessPiece.currentPositionY + direction));
                break;
        }

        for (int i = 0; i < avaibleMove.Count; i++)
        {
            _arrayTile[avaibleMove[i].x, avaibleMove[i].y].OnHighlight();
        }
    }

    private void ChangePositionPiece(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {
        _coordinatePiece[chessPiece.currentPositionX, chessPiece.currentPositionY] = chessPiece;
        _coordinatePiece[posChangeOnX, posChangeOnY].currentPositionX = posChangeOnX;
        _coordinatePiece[posChangeOnX, posChangeOnY].currentPositionY = posChangeOnY;
        _coordinatePiece[posChangeOnX, posChangeOnY] = chessPiece;

        if (chessPiece.type == ChessPieceType.Pown && chessPiece.team == 0 && posChangeOnY == _coordinatePiece.GetLength(1) - 1)
            print("Change type Pown White");

        if (chessPiece.type == ChessPieceType.Pown && chessPiece.team == 1 && posChangeOnY == 0)
            print("black");


        _coordinatePiece[posChangeOnX, posChangeOnY].transform.position = new Vector3(posChangeOnX, posChangeOnY, _positionPieceZ);
    }
}
/*List<Vector2Int> avaibleMove = new List<Vector2Int>();

int direction = (team == 0) ? 1 : -1;

if (chessPieces[currentPositionX, currentPositionY + direction] == null)
    avaibleMove.Add(new Vector2Int(currentPositionX, currentPositionY + direction));


return avaibleMove;*/



