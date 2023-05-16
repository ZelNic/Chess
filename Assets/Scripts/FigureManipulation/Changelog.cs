using System;
using System.Collections.Generic;
using UnityEngine;
public class Changelog : MonoBehaviour
{
    public static Action<ChessPiece> onMovingChessPiece;
    public static Action<ChessPiece, ChessPieceType> onChangeType;

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
        if (!CanUndo) return;

        if (_chessPiece[_stepIndex].transform.position == _LogVector[_stepIndex])
        {
            _stepIndex--;
            if (_chessPiece[_stepIndex].transform.position == _LogVector[_stepIndex])
                _stepIndex--;
        }

        Distributor.onSetOnPlace.Invoke(_chessPiece[_stepIndex], (int)_LogVector[_stepIndex].x, (int)_LogVector[_stepIndex].y, true);
        Distributor.onWasMadeMove.Invoke();
        ChangeFlagMovementKing();
        ChangeFlagMovementRock();

        _stepIndex--;

        if (!CanUndo) return;

        if (_chessPiece[_stepIndex].gameObject.activeInHierarchy == false)
        {
            _chessPiece[_stepIndex].gameObject.SetActive(true);
            Distributor.onSetOnPlace.Invoke(_chessPiece[_stepIndex], (int)_LogVector[_stepIndex].x, (int)_LogVector[_stepIndex].y, false);
            if (_chessPiece[_stepIndex].transform.position == _LogVector[_stepIndex])
                _stepIndex--;
        }

        CompareWithBaseType();
    }
    public void RedoMovement()
    {
        if (!CanRedo) return;

        _stepIndex++;

        if (_chessPiece[_stepIndex].transform.position == _LogVector[_stepIndex])
        {
            _stepIndex++;
            if (_chessPiece[_stepIndex].transform.position == _LogVector[_stepIndex])
                _stepIndex++;
        }

        Distributor.onCheckBusyCellEnemy.Invoke(_chessPiece[_stepIndex], (int)_LogVector[_stepIndex].x, (int)_LogVector[_stepIndex].y);
        Distributor.onSetOnPlace.Invoke(_chessPiece[_stepIndex], (int)_LogVector[_stepIndex].x, (int)_LogVector[_stepIndex].y, true);
        Distributor.onWasMadeMove.Invoke();
        CompareWithNewType();
        ChangeFlagMovementKing();
        ChangeFlagMovementRock();
    }
    private void CompareWithBaseType()
    {
        if (_chessPiece[_stepIndex].GetComponent<Pawn>() != null)
        {
            Pawn pawn = _chessPiece[_stepIndex].GetComponent<Pawn>();
            if (pawn.type != pawn.DefaultType)
            {
                onChangeType?.Invoke(_chessPiece[_stepIndex], pawn.DefaultType);
            }
        }
    }
    private void CompareWithNewType()
    {
        if (_chessPiece[_stepIndex].GetComponent<Pawn>() != null)
        {
            Pawn pawn = _chessPiece[_stepIndex].GetComponent<Pawn>();
            if (pawn.type != pawn.NewType)
            {
                onChangeType?.Invoke(_chessPiece[_stepIndex], pawn.NewType);
            }
        }
    }
    private void ChangeFlagMovementKing()
    {
        if (_chessPiece[_stepIndex].type == ChessPieceType.King)
        {
            if (_chessPiece[_stepIndex].team == 0 && _LogVector[_stepIndex] == new Vector3(4, 0, -5))
                _chessPiece[_stepIndex].GetComponent<King>().ResetStep();

            if (_chessPiece[_stepIndex].team == 1 && _LogVector[_stepIndex] == new Vector3(4, 7, -5))
                _chessPiece[_stepIndex].GetComponent<King>().ResetStep();
        }
    }
    private void ChangeFlagMovementRock()
    {
        if (_chessPiece[_stepIndex].type == ChessPieceType.Rook)
        {
            if (_chessPiece[_stepIndex].team == 0)
            {
                if (_LogVector[_stepIndex] == new Vector3(0, 0, -5) && _chessPiece[_stepIndex].transform.position == _LogVector[_stepIndex])
                    _chessPiece[_stepIndex].GetComponent<Rook>().ResetStep();

                if (_LogVector[_stepIndex] == new Vector3(7, 0, -5) && _chessPiece[_stepIndex].transform.position == _LogVector[_stepIndex])
                    _chessPiece[_stepIndex].GetComponent<Rook>().ResetStep();
            }

            if (_chessPiece[_stepIndex].team == 1)
            {
                if (_LogVector[_stepIndex] == new Vector3(0, 7, -5) && _chessPiece[_stepIndex].transform.position == _LogVector[_stepIndex])
                    _chessPiece[_stepIndex].GetComponent<Rook>().ResetStep();

                if (_LogVector[_stepIndex] == new Vector3(7, 7, -5) && _chessPiece[_stepIndex].transform.position == _LogVector[_stepIndex])
                    _chessPiece[_stepIndex].GetComponent<Rook>().ResetStep();
            }
        }
    }
}