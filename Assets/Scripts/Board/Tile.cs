using System;
using UnityEngine;


public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _baseColor, _offSetColor;    
    [SerializeField] private Color _highlight;
    private Color _defaultColor;


    public void ChangeColor(bool isOffSet)
    {
        _renderer.color = isOffSet ? _baseColor : _offSetColor;
        _defaultColor = _renderer.color;
    }

    public void OnHighlight()
    {
        _renderer.color = _highlight;
    }

    public void SetDefaultColor()
    {
        _renderer.color = _defaultColor;
    }
}
