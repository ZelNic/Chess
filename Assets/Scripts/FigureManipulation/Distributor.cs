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
    public static Action<ChessPiece, int, int, bool> onSetOnPlace;
    public static Func<ChessPiece, int, int, bool> onCheckBusyCellEnemy;

    private ChessPiece[,] _mapCP;
    private Tile[,] _arrayTile;
    private readonly int _positionPieceZ = -5;
    private List<Vector2Int> avaibleMove;

    private void OnEnable()
    {
        onStartDistribution += StartDistribute;
        onChangePositionPiece += CheckingCell;
        onShowAviableMoves += ShowAviableMoves;
        onDestroyPieces += DestroyPieces;
        onSetOnPlace += SetOnPlace;
        onCheckBusyCellEnemy += CheckBusyCellEnemy;
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
    private void SetOnPlace(ChessPiece chessPiece, int newX, int newY, bool clearSell)
    {
        int x = chessPiece.currentPositionX;
        int y = chessPiece.currentPositionY;

        if (_mapCP[x, y] == null)
            _mapCP[x, y] = chessPiece;

        _mapCP[x, y].currentPositionX = newX;
        _mapCP[x, y].currentPositionY = newY;
        _mapCP[x, y].transform.position = new Vector3(newX, newY, _positionPieceZ);
        _mapCP[newX, newY] = _mapCP[x, y];

        if (clearSell == true)
            _mapCP[x, y] = null;
    }
    private void ShowAviableMoves(ChessPiece chessPiece)
    {
        avaibleMove = chessPiece.ShowAviableMove(_mapCP);
        for (int i = 0; i < avaibleMove.Count; i++)
            _arrayTile[avaibleMove[i].x, avaibleMove[i].y].OnHighlight();
    }
    private void CheckingCell(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {
        for (int i = 0; i < avaibleMove.Count; i++)
            if (avaibleMove[i] == new Vector2Int(posChangeOnX, posChangeOnY))
            {

                if (CheckBusyCellEnemy(chessPiece, posChangeOnX, posChangeOnY) == true)
                {
                    MovementJournal.onMovingChessPiece.Invoke(_mapCP[posChangeOnX, posChangeOnY]);
                    if (_mapCP[posChangeOnX, posChangeOnY].type == ChessPieceType.King)
                        break;
                }

                MovementJournal.onMovingChessPiece.Invoke(chessPiece);
                SetOnPlace(chessPiece, posChangeOnX, posChangeOnY, true);
                MovementJournal.onMovingChessPiece.Invoke(chessPiece);
                onWasMadeMove.Invoke();

                //Castling
                if (chessPiece.type == ChessPieceType.King)
                {
                    if (avaibleMove[i] == new Vector2Int(2, posChangeOnY))
                        if (chessPiece.GetComponent<King>().FirstStep == true && _mapCP[0, posChangeOnY].GetComponent<Rock>().FirstStep == true)
                        {
                            _mapCP[0, posChangeOnY].GetComponent<Rock>().MakeMove();
                            SetOnPlace(chessPiece, 3, posChangeOnY, true);
                        }
                    if (avaibleMove[i] == new Vector2Int(6, posChangeOnY))
                        if (chessPiece.GetComponent<King>().FirstStep == true && _mapCP[7, posChangeOnY].GetComponent<Rock>().FirstStep == true)
                        {
                            _mapCP[7, posChangeOnY].GetComponent<Rock>().MakeMove();
                            SetOnPlace(chessPiece, 5, posChangeOnY, true);
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
    private bool CheckBusyCellEnemy(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {
        if (_mapCP[posChangeOnX, posChangeOnY] != null && _mapCP[posChangeOnX, posChangeOnY].team != chessPiece.team)
        {
            if (_mapCP[posChangeOnX, posChangeOnY].type == ChessPieceType.King)
            {
                CheckWhoWins(chessPiece);
            }
            _mapCP[posChangeOnX, posChangeOnY].gameObject.SetActive(false);
            return true;
        }
        return false;
    }
    private void CheckWhoWins(ChessPiece chessPiece) => Judge.onShowWhoWins.Invoke(chessPiece.team);
}