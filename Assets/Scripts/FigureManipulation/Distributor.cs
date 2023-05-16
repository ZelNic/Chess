using System;
using System.Collections.Generic;
using UnityEngine;

public class Distributor : MonoBehaviour
{
    public static Action onDestroyPieces;
    public static Action onPawnOnEdgeBoard;
    public static Action onWasMadeMove;

    public static Action<ChessPiece[,]> onStartDistribution;
    public static Action<ChessPiece, int, int> onChangePositionPiece;
    public static Action<ChessPiece> onSendToReplaceType;
    public static Action<ChessPiece, int, int, bool> onSetOnPlace;

    public static Func<ChessPiece, int, int, bool> onCheckBusyCellEnemy;
    public static Func<ChessPiece[,], ChessPiece, List<Vector2Int>> onShowAviableMoves;

    private ChessPiece[,] _mapCP;
    private Tile[,] _arrayTile;
    private readonly int _positionPieceZ = -5;
    private List<Vector2Int> _availableMove;

    private void OnEnable()
    {
        onStartDistribution += StartDistribute;
        onChangePositionPiece += CheckingCell;
        onDestroyPieces += DestroyPieces;
        onSetOnPlace += SetOnPlace;
        onCheckBusyCellEnemy += CheckBusyCellEnemy;
        Player.onChooseÑhessPiece += ShowAvailableMoves;
        RestartGame.onRestartGame += DestroyPieces;
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
    private void ShowAvailableMoves(ChessPiece chessPiece, bool checkmate)
    {
        _availableMove = onShowAviableMoves?.Invoke(_mapCP, chessPiece);
        if (Checkmate(chessPiece) == true) return;
        if (checkmate == true)
        {
            for (int i = 0; i < _availableMove.Count; i++)
            {
                _arrayTile[_availableMove[i].x, _availableMove[i].y].OnHighlight();
                if (_mapCP[_availableMove[i].x, _availableMove[i].y] != null)
                    if (_mapCP[_availableMove[i].x, _availableMove[i].y].team != chessPiece.team)
                    {
                        _arrayTile[_availableMove[i].x, _availableMove[i].y].ShowAttackEnemy();
                    }
            }
        }
    }
    private void CheckingCell(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {
        for (int i = 0; i < _availableMove.Count; i++)
            if (_availableMove[i] == new Vector2Int(posChangeOnX, posChangeOnY))
            {
                if (CheckBusyCellEnemy(chessPiece, posChangeOnX, posChangeOnY) == true)
                {
                    Changelog.onMovingChessPiece.Invoke(_mapCP[posChangeOnX, posChangeOnY]);
                    if (_mapCP[posChangeOnX, posChangeOnY].type == ChessPieceType.King)
                        break;
                }
                Changelog.onMovingChessPiece.Invoke(chessPiece);
             
                CheckPossibilityCastling(chessPiece, posChangeOnY, i);                

                if (chessPiece.GetComponent<Rook>() != null) chessPiece.GetComponent<Rook>().MakeStep();

                SetOnPlace(chessPiece, posChangeOnX, posChangeOnY, true);

                Changelog.onMovingChessPiece.Invoke(chessPiece);
                onWasMadeMove.Invoke();
                CheckPossibleReplacePawn(chessPiece);
                break;
            }
        ShowAvailableMoves(chessPiece, false);
        Checkmate(chessPiece);
    }

    private void CheckPossibilityCastling(ChessPiece chessPiece,  int posChangeOnY,int i)
    {
        if (chessPiece.type == ChessPieceType.King)
        {
            if (_availableMove[i] == new Vector2Int(2, posChangeOnY))
                if (chessPiece.GetComponent<King>().IsFirstStep == true && _mapCP[0, posChangeOnY].GetComponent<Rook>().IsFirstStep == true)
                {
                    _mapCP[0, posChangeOnY].GetComponent<Rook>().MakeStep();
                    Changelog.onMovingChessPiece.Invoke(_mapCP[0, posChangeOnY]);
                    SetOnPlace(_mapCP[0, posChangeOnY], 3, posChangeOnY, true);
                    Changelog.onMovingChessPiece.Invoke(_mapCP[3, posChangeOnY]);
                }
            if (_availableMove[i] == new Vector2Int(6, posChangeOnY))
                if (chessPiece.GetComponent<King>().IsFirstStep == true && _mapCP[7, posChangeOnY].GetComponent<Rook>().IsFirstStep == true)
                {
                    _mapCP[7, posChangeOnY].GetComponent<Rook>().MakeStep();
                    Changelog.onMovingChessPiece.Invoke(_mapCP[7, posChangeOnY]);
                    SetOnPlace(_mapCP[7, posChangeOnY], 5, posChangeOnY, true);
                    Changelog.onMovingChessPiece.Invoke(_mapCP[5, posChangeOnY]);
                }
            chessPiece.GetComponent<King>().MakeStep();
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
    private void CheckWhoWins(ChessPiece chessPiece) => OutcomeGame.onShowWhoWins.Invoke(chessPiece.team);
    private void CheckPossibleReplacePawn(ChessPiece chessPiece)
    {
        if (chessPiece.type == ChessPieceType.Pawn && (chessPiece.currentPositionY == _mapCP.GetLength(1) - 1 || chessPiece.currentPositionY == 0))
        {
            onSendToReplaceType?.Invoke(chessPiece);
            onPawnOnEdgeBoard?.Invoke();
        }
    }
    private bool Checkmate(ChessPiece chessPiece)
    {
        for (int i = 0; i < _availableMove.Count; i++)
        {
            int x = _availableMove[i].x;
            int y = _availableMove[i].y;
            if (_mapCP[x, y] != null)
                if (_mapCP[x, y].type == ChessPieceType.King && chessPiece.team != _mapCP[x, y].team)
                {
                    _arrayTile[x, y].ShowAttackEnemy();
                    return true;
                }
        }
        return false;
    }
}




