using System;
using System.Collections.Generic;
using UnityEngine;
public class MovementJournal : MonoBehaviour
{
    public static Action<ChessPiece> onMovingChessPiece;
    private List<Vector3> _moveLogVec = new();
    private List<ChessPiece> _moveLogChess = new();
    private int _moveCount = -1;
    private bool CanUndo { get { return _moveCount >= 0; } }
    private bool CanRedo { get { return _moveLogChess.Count > 0 && _moveCount < _moveLogChess.Count - 1; } }
    private void OnEnable()
    {
        onMovingChessPiece += AddInListMovement;
        RestartGame.onRestartGame += ClearMovementJournal;
    }
    private void ClearMovementJournal()
    {
        _moveCount = -1;
        _moveLogChess.Clear();
        _moveLogVec.Clear();
    }
    private void AddInListMovement(ChessPiece chessPiece)
    {
        CutOffLog();
        _moveLogChess.Add(chessPiece);
        _moveLogVec.Add(chessPiece.transform.position);
        _moveCount++;
    }
    private void CutOffLog()
    {
        int index = _moveCount + 1;
        if (index < _moveLogChess.Count)
        {
            _moveLogChess.RemoveRange(index, _moveLogChess.Count - index);
            _moveLogVec.RemoveRange(index, _moveLogVec.Count - index);
        }
    }
    public void UndoMovement()
    {
        if (!CanUndo)
            return;

        if (_moveLogChess[_moveCount].transform.position.x == _moveLogVec[_moveCount].x
           && _moveLogChess[_moveCount].transform.position.y == _moveLogVec[_moveCount].y)
            _moveCount--;


        Distributor.onSetOnPlace.Invoke(_moveLogChess[_moveCount], (int)_moveLogVec[_moveCount].x, (int)_moveLogVec[_moveCount].y, true);
        _moveCount--;
    }

    public void RedoMovement()
    {
        if (!CanRedo)
            return;

        _moveCount++;

        if (_moveLogChess[_moveCount].transform.position.x == _moveLogVec[_moveCount].x)
            if (_moveLogChess[_moveCount].transform.position.y == _moveLogVec[_moveCount].y)
                _moveCount++;

        Distributor.onSetOnPlace.Invoke(_moveLogChess[_moveCount], (int)_moveLogVec[_moveCount].x, (int)_moveLogVec[_moveCount].y, true);
    }
}