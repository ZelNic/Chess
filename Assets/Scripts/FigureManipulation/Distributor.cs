using System;
using System.Collections.Generic;
using UnityEngine;

public class Distributor : MonoBehaviour
{
    public static Action onDestroyPieces;
    public static Action<ChessPiece[,]> onStartDistribution;
    public static Action<ChessPiece, int, int> onChangePositionPiece;
    public static Action<ChessPiece> onShowAviableMoves;
    public static Action<ChessPiece[,], ChessPiece> onSendToReplaceType;
    public static Action onPawnOnEdgeBoard;
    public static Action onWasMadeMove;
    public static Action<int, int, int, int> onSetOnPlace;

    private ChessPiece[,] _mapCP;
    private Tile[,] _arrayTile;
    private readonly int _positionPieceZ = -5;
    private List<Vector2Int> avaibleMove;

    private void OnEnable()
    {
        onStartDistribution += StartDistribute;
        onChangePositionPiece += PositionAvailabilityCheck;
        onShowAviableMoves += ShowAviableMoves;
        onDestroyPieces += DestroyPieces;
        onSetOnPlace += SetOnPlace;
    }

    private void Start() => _arrayTile = BoardCreator.onSendArrayTile?.Invoke();
    private void DestroyPieces()
    {
        for (int x = 0; x < _mapCP.GetLength(0); x++)
            for (int y = 0; y < _mapCP.GetLength(1); y++)
                if (_mapCP[x, y] != null)
                {
                    Destroy(_mapCP[x, y].gameObject);
                }
    }
    private void StartDistribute(ChessPiece[,] chessPieces)
    {
        _mapCP = chessPieces;
        for (int x = 0; x < _mapCP.GetLength(0); x++)
            for (int y = 0; y < _mapCP.GetLength(1); y++)
                if (_mapCP[x, y] != null)
                    SetOnPlaceInBegginig(x, y);
    }
    private void SetOnPlaceInBegginig(int x, int y)
    {
        _mapCP[x, y].currentPositionX = x;
        _mapCP[x, y].currentPositionY = y;
        _mapCP[x, y].transform.position = new Vector3(x, y, _positionPieceZ);
    }
    private void SetOnPlace(int x, int y, int newX, int newY)
    {
        _mapCP[newX, newY] = _mapCP[x, y];
        _mapCP[newX, newY].transform.position = new Vector3(newX, newY, _positionPieceZ);
        _mapCP[newX, newY].currentPositionX = newX;
        _mapCP[newX, newY].currentPositionY = newY;
        _mapCP[x, y] = null;
    }
    private void ShowAviableMoves(ChessPiece chessPiece)
    {
        avaibleMove = chessPiece.ShowAviableMove(_mapCP);
        for (int i = 0; i < avaibleMove.Count; i++)
            _arrayTile[avaibleMove[i].x, avaibleMove[i].y].OnHighlight();
    }
    private void PositionAvailabilityCheck(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {

        MovementJournal.onMovingChessPiece.Invoke(chessPiece);
        for (int i = 0; i < avaibleMove.Count; i++)
            if (avaibleMove[i] == new Vector2Int(posChangeOnX, posChangeOnY))
            {
                //Ñhecking for an enemy
                if (_mapCP[posChangeOnX, posChangeOnY] != null && _mapCP[posChangeOnX, posChangeOnY].team != chessPiece.team)
                {
                    //Checking which king was killed
                    if (_mapCP[posChangeOnX, posChangeOnY].type == ChessPieceType.King)
                        GameResult.onShowWhoWins.Invoke(chessPiece.team);
                    _mapCP[posChangeOnX, posChangeOnY].DestroyPiece();
                }

                SetOnPlace(chessPiece.currentPositionX, chessPiece.currentPositionY, posChangeOnX, posChangeOnY);
                onWasMadeMove.Invoke();
                //Castling
                if (chessPiece.type == ChessPieceType.King)
                {
                    if (avaibleMove[i] == new Vector2Int(2, posChangeOnY))
                        if (chessPiece.GetComponent<King>().FirstStep == true && _mapCP[0, posChangeOnY].GetComponent<Rock>().FirstStep == true)
                        {
                            _mapCP[0, posChangeOnY].GetComponent<Rock>().MakeMove();
                            SetOnPlace(0, posChangeOnY, 3, posChangeOnY);
                        }
                    if (avaibleMove[i] == new Vector2Int(6, posChangeOnY))
                        if (chessPiece.GetComponent<King>().FirstStep == true && _mapCP[7, posChangeOnY].GetComponent<Rock>().FirstStep == true)
                        {
                            _mapCP[7, posChangeOnY].GetComponent<Rock>().MakeMove();
                            SetOnPlace(7, posChangeOnY, 5, posChangeOnY);
                        }
                    chessPiece.GetComponent<King>().MakeMove();
                }

                //Checking for change type piece
                if (chessPiece.type == ChessPieceType.Pawn && (chessPiece.currentPositionY == _mapCP.GetLength(1) - 1 || chessPiece.currentPositionY == 0))
                {
                    onSendToReplaceType?.Invoke(_mapCP, chessPiece);
                    onPawnOnEdgeBoard?.Invoke();
                }
                break;
            }
    }
}