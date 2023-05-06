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
    private bool CanRedo { get { return _moveLogVec.Count > 0 && _moveCount < _moveLogVec.Count - 1; } }
    private void OnEnable() => onMovingChessPiece += AddInListMovement;
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
        Distributor.onSetOnPlace.Invoke((int)_moveLogChess[_moveCount].transform.position.x,
                                        (int)_moveLogChess[_moveCount].transform.position.y,
                                        (int)_moveLogVec[_moveCount].x, (int)_moveLogVec[_moveCount].y, true);
        _moveCount--;
    }
    public void RedoMovement()
    {
        if (!CanRedo)
            return;
        _moveCount++;
        Distributor.onSetOnPlace.Invoke((int)_moveLogChess[_moveCount].transform.position.x,
                                        (int)_moveLogChess[_moveCount].transform.position.y,
                                        (int)_moveLogVec[_moveCount].x, (int)_moveLogVec[_moveCount].y, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Show();
    }

    private void Show()
    {
        print(_moveCount);
        for (int i = 0; i <= _moveLogChess.Count - 1; i++)
        {
            print(_moveLogVec[i] + " Figure");
        }
    }

}
/*_moveLogChess[_moveCount].currentPositionX = _moveLogChess[_moveCount].currentPositionX;
       _moveLogChess[_moveCount].currentPositionY = _moveLogChess[_moveCount].currentPositionY;
       _moveLogChess[_moveCount].transform.position = _moveLogVec[_moveCount];
       _mapCP[_moveLogChess[_moveCount].currentPositionX, _moveLogChess[_moveCount].currentPositionY] = _moveLogChess[_moveCount];*/


/* _moveLogChess[_moveCount].currentPositionX = _moveLogChess[_moveCount].currentPositionX;
      _moveLogChess[_moveCount].currentPositionY = _moveLogChess[_moveCount].currentPositionY;
      _moveLogChess[_moveCount].transform.position = _moveLogVec[_moveCount];
      _mapCP[_moveLogChess[_moveCount].currentPositionX, _moveLogChess[_moveCount].currentPositionY] = _moveLogChess[_moveCount];*/