using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offSetColor;    
    [SerializeField] private SpriteRenderer _renderer;  

    public void ChangeColor(bool isOffSet) => _renderer.color = isOffSet ? _baseColor : _offSetColor;
}
