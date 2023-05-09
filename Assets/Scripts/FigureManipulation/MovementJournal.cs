using System;
using System.Collections.Generic;
using UnityEngine;
public class MovementJournal : MonoBehaviour
{
    public static Action<ChessPiece> onMovingChessPiece;
    private List<Vector3> _LogVector = new();
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
        _LogVector.Clear();
    }
    private void AddInListMovement(ChessPiece chessPiece)
    {
        CutOffLog();
        _chessPiece.Add(chessPiece);
        _LogVector.Add(chessPiece.transform.position);
        _stepIndex++;
    }
    private void CutOffLog()
    {
        int index = _stepIndex + 1;
        if (index < _chessPiece.Count)
        {
            _chessPiece.RemoveRange(index, _chessPiece.Count - index);
            _LogVector.RemoveRange(index, _LogVector.Count - index);
        }
    }
    public void UndoMovement()
    {
        if (!CanUndo)
            return;

        if (_chessPiece[_stepIndex].transform.position.x == _LogVector[_stepIndex].x
           && _chessPiece[_stepIndex].transform.position.y == _LogVector[_stepIndex].y)
            _stepIndex--;

        Distributor.onSetOnPlace.Invoke(_chessPiece[_stepIndex], (int)_LogVector[_stepIndex].x, (int)_LogVector[_stepIndex].y, true);

        _stepIndex--;

        if (!CanUndo)
            return;

        if (_chessPiece[_stepIndex].gameObject.activeInHierarchy == false)
        {
            _chessPiece[_stepIndex].gameObject.SetActive(true);
            Distributor.onSetOnPlace.Invoke(_chessPiece[_stepIndex], (int)_LogVector[_stepIndex].x, (int)_LogVector[_stepIndex].y, false);
            if (_chessPiece[_stepIndex].transform.position.x == _LogVector[_stepIndex].x
           && _chessPiece[_stepIndex].transform.position.y == _LogVector[_stepIndex].y)
                _stepIndex--;
        }
    }
    public void RedoMovement()
    {
        if (!CanRedo)
            return;

        _stepIndex++;

        if (_chessPiece[_stepIndex].transform.position.x == _LogVector[_stepIndex].x)
            if (_chessPiece[_stepIndex].transform.position.y == _LogVector[_stepIndex].y)
                _stepIndex++;

        if (_chessPiece[_stepIndex].transform.position.x == _LogVector[_stepIndex].x)
            if (_chessPiece[_stepIndex].transform.position.y == _LogVector[_stepIndex].y)
                _stepIndex++;

        Distributor.onCheckBusyCellEnemy.Invoke(_chessPiece[_stepIndex], (int)_LogVector[_stepIndex].x, (int)_LogVector[_stepIndex].y);
        Distributor.onSetOnPlace.Invoke(_chessPiece[_stepIndex], (int)_LogVector[_stepIndex].x, (int)_LogVector[_stepIndex].y, true);
    }
}