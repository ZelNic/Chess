using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Distributor : MonoBehaviour
{
    public static Action onDestroyPieces;
    public static Action<ChessPiece[,]> onStartDistribution;
    public static Action<ChessPiece, int, int> onChangePositionPiece;
    public static Action<ChessPiece> onShowAviableMoves;
    public static Action<int, int> onSetOnPlace;

    private ChessPiece[,] _mapCP;
    private Tile[,] _arrayTile;
    private readonly int _positionPieceZ = -5;
    private List<Vector2Int> avaibleMove;

    private void OnEnable()
    {
        onStartDistribution += StartDistribute;
        onChangePositionPiece += ChangePositionPiece;
        onShowAviableMoves += ShowAviableMoves;
        onDestroyPieces += SendPiecesToScrap;
        onSetOnPlace += SetOnPlace;
    }

    private void Start() => _arrayTile = BoardCreator.onSendArrayTile?.Invoke();
    private void SendPiecesToScrap()
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
                    SetOnPlace(x, y);
    }
    private void SetOnPlace(int x, int y)
    {
        _mapCP[x, y].currentPositionX = x;
        _mapCP[x, y].currentPositionY = y;
        _mapCP[x, y].transform.position = new Vector3(x, y, _positionPieceZ);
    }
    private void ShowAviableMoves(ChessPiece chessPiece)
    {
        avaibleMove = chessPiece.ShowAviableMove(_mapCP);
        for (int i = 0; i < avaibleMove.Count; i++)
            _arrayTile[avaibleMove[i].x, avaibleMove[i].y].OnHighlight();
    }
    private void ChangePositionPiece(ChessPiece chessPiece, int posChangeOnX, int posChangeOnY)
    {
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

                //Change position current chessPiece
                _mapCP[chessPiece.currentPositionX, chessPiece.currentPositionY] = null;
                _mapCP[posChangeOnX, posChangeOnY] = chessPiece;
                _mapCP[posChangeOnX, posChangeOnY].transform.position = new Vector3(posChangeOnX, posChangeOnY, _positionPieceZ);
                chessPiece.currentPositionX = posChangeOnX;
                chessPiece.currentPositionY = posChangeOnY;

                //Castling
                if (avaibleMove[i] == new Vector2Int(2, posChangeOnY) && chessPiece.type == ChessPieceType.King)
                    if (chessPiece.GetComponent<King>().FirstStep == true)
                    {
                        _mapCP[0, posChangeOnY].transform.position = new Vector3(3, posChangeOnY, _positionPieceZ);
                        chessPiece.currentPositionX = 3;
                        chessPiece.currentPositionY = posChangeOnY;
                    }
                        

                //Checking for change type piece
                if (chessPiece.type == ChessPieceType.Pawn && (chessPiece.currentPositionY == _mapCP.GetLength(1) - 1 || chessPiece.currentPositionY == 0))
                {
                    ChangePiece.onActiveChoose.Invoke(_mapCP, chessPiece);
                    Player.onStopSelection.Invoke();
                }
                break;
            }
    }
}