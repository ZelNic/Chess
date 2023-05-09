using System;
using System.Collections.Generic;
using UnityEngine;
public class MovementJournal : MonoBehaviour
{
    public static Action<ChessPiece> onMovingChessPiece;
    private List<Vector3> _moveLogVec = new();
    private List<ChessPiece> _chessPiece = new();
    private int _stepIndex = -1;
    private bool CanUndo { get { return _stepIndex >= 0; } }
    private bool CanRedo { get { return _chessPiece.Count > 0 && _stepIndex < _chessPiece.Count - 1; } }
    private void OnEnable()
    {
        onMovingChessPiece += AddInListMovement;
        RestartGame.onRestartGame += ClearMovementJournal;
    }
    private void ClearMovementJournal()
    {
        _stepIndex = -1;
        _chessPiece.Clear();
        _moveLogVec.Clear();
    }
    private void AddInListMovement(ChessPiece chessPiece)
    {
        CutOffLog();
        _chessPiece.Add(chessPiece);
        _moveLogVec.Add(chessPiece.transform.position);
        _stepIndex++;
    }
    private void CutOffLog()
    {
        int index = _stepIndex + 1;
        if (index < _chessPiece.Count)
        {
            _chessPiece.RemoveRange(index, _chessPiece.Count - index);
            _moveLogVec.RemoveRange(index, _moveLogVec.Count - index);
        }
    }
    public void UndoMovement()
    {
        if (!CanUndo)
            return;

        if (_chessPiece[_stepIndex].transform.position.x == _moveLogVec[_stepIndex].x
           && _chessPiece[_stepIndex].transform.position.y == _moveLogVec[_stepIndex].y)
            _stepIndex--;

        Distributor.onSetOnPlace.Invoke(_chessPiece[_stepIndex], (int)_moveLogVec[_stepIndex].x, (int)_moveLogVec[_stepIndex].y, true);

        _stepIndex--;

        if (!CanUndo)
            return;

        if (_chessPiece[_stepIndex].gameObject.activeInHierarchy == false)
        {
            _chessPiece[_stepIndex].gameObject.SetActive(true);
            Distributor.onSetOnPlace.Invoke(_chessPiece[_stepIndex], (int)_moveLogVec[_stepIndex].x, (int)_moveLogVec[_stepIndex].y, false);
            if (_chessPiece[_stepIndex].transform.position.x == _moveLogVec[_stepIndex].x
           && _chessPiece[_stepIndex].transform.position.y == _moveLogVec[_stepIndex].y)
                _stepIndex--;
        }
    }
    public void RedoMovement()
    {
        if (!CanRedo)
            return;

        if (_chessPiece[_stepIndex].transform.position.x == _moveLogVec[_stepIndex].x)
            if (_chessPiece[_stepIndex].transform.position.y == _moveLogVec[_stepIndex].y)
                _stepIndex++;

        if (Distributor.onCheckBusyCellEnemy(_chessPiece[_stepIndex], (int)_moveLogVec[_stepIndex + 1].x, (int)_moveLogVec[_stepIndex + 1].y) == true)
        {
            if (_chessPiece[_stepIndex + 1].gameObject.activeInHierarchy == true)
            {
                _chessPiece[_stepIndex + 1].gameObject.SetActive(false);
                Distributor.onSetOnPlace.Invoke(_chessPiece[_stepIndex + 1], (int)_moveLogVec[_stepIndex + 1].x, (int)_moveLogVec[_stepIndex + 1].y, true);
            }
        }
        _stepIndex++;      

        Distributor.onSetOnPlace.Invoke(_chessPiece[_stepIndex], (int)_moveLogVec[_stepIndex].x, (int)_moveLogVec[_stepIndex].y, true);
    }

    public void Update()
    {
        Show();
    }

    private void Show()
    {
        print(_stepIndex);
        if (Input.GetKeyDown(KeyCode.Space))
        {

            for (int i = 0; i < _chessPiece.Count; i++)
            {
                print(_moveLogVec[i]);
            }
        }
    }
}