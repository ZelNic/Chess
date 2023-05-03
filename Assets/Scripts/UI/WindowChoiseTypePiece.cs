using System;
using UnityEngine;
using UnityEngine.UI;

public class WindowChoiseTypePiece : MonoBehaviour
{    
    public static Action<ChessPieceType> onChosenType;
    [SerializeField] private Image _selectionWindow;

    private void OnEnable() => ChangeTypePiece.onActiveChoise += SwitchActivity;
    private void SwitchActivity()
    {
        if (_selectionWindow.gameObject.activeInHierarchy == false)
            _selectionWindow.gameObject.SetActive(true);
        else if (_selectionWindow.gameObject.activeInHierarchy == true)
            _selectionWindow.gameObject.SetActive(false);
    }
    public void ChooseKnight()
    {
        onChosenType?.Invoke(ChessPieceType.Knight);
        SwitchActivity();
    }
    public void ChooseRock()
    {
        onChosenType?.Invoke(ChessPieceType.Rook);
        SwitchActivity();
    }   
    public void ChooseBishop()
    {
        onChosenType?.Invoke(ChessPieceType.Bishop);
        SwitchActivity();
    }
    public void ChooseQueen()
    {
        onChosenType?.Invoke(ChessPieceType.Queen);
        SwitchActivity();
    }
}
