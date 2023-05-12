using System;
using UnityEngine;

public class RotatePieces : MonoBehaviour
{
    private ChessPiece[,] _mapCP;

    private void OnEnable()
    {
        Distributor.onStartDistribution += GetMapChessPiece;
        CameraOptions.onRotateCamera += TurnPieces;
    }
    private void GetMapChessPiece(ChessPiece[,] mapCP)
    {
        _mapCP = mapCP;
    }

    private void TurnPieces()
    {
        for (int i = 0; i < _mapCP.GetLength(0); i++)        
            for (int j = 0; j < _mapCP.GetLength(1); j++)            
                if(_mapCP[i, j] != null)
                {
                    if (_mapCP[i, j].transform.rotation == new Quaternion(0f, 0f, 0f, 1f))                    
                        _mapCP[i, j].transform.rotation = new Quaternion(0f, 0f, 180f, 1f);
                      
                    else _mapCP[i, j].transform.rotation = new Quaternion(0f, 0f, 0f, 1f);                      
                }            
    }
}
