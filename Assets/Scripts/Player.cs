using System;
using UnityEngine;
public struct ConstantsLayer
{
    public const int TILE_LAYER = 7;
    public const int PIECE = 10;
}
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _touchpointPiece;
    private ChessPiece _selectedPiece;
    private RaycastHit2D _rayHit;
    private GameObject _rayHitGO;
    private int _countClick = 0;
    private bool _isWhite = true;
    private bool _isStopSelection = false;

    private void OnEnable()
    {
        Distributor.onDestroyPieces += SetIsWhiteTrue;
        Distributor.onPawnOnEdgeBoard += StopSelection;
    }
    private void SetIsWhiteTrue() => _isWhite = true;
    private void PlayerSwitch() => _isWhite = _isWhite == true ? false : true;
    private void StopSelection() => _isStopSelection = _isStopSelection == false ? true : false;
    private void Update() => SelectObject();

    private void SelectObject()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _rayHit = Physics2D.Raycast(cursorPosition, Vector2.zero);
        if (_rayHit.collider != null)
            _rayHitGO = _rayHit.collider.gameObject;

        if (Input.GetMouseButtonDown(0) && _isStopSelection == false)
        {
            _touchpointPiece.gameObject.SetActive(false);
            Tile.onSetDefautColor?.Invoke();
            if (_rayHit.collider != null && _rayHitGO.layer == ConstantsLayer.PIECE && _countClick == 0)
                if ((_isWhite == true && _rayHitGO.GetComponent<ChessPiece>().team == 0) || (_isWhite == false && _rayHitGO.GetComponent<ChessPiece>().team == 1))
                {
                    _touchpointPiece.gameObject.SetActive(true);
                    _selectedPiece = _rayHitGO.GetComponent<ChessPiece>();
                    _touchpointPiece.transform.position = _selectedPiece.transform.position;
                    _countClick++;
                    Distributor.onShowAviableMoves?.Invoke(_selectedPiece);
                }

            if (_countClick > 0 && _rayHitGO.layer == ConstantsLayer.TILE_LAYER)
            {
                _countClick = 0;
                Distributor.onChangePositionPiece?.Invoke(_selectedPiece, (int)_rayHitGO.transform.position.x, (int)_rayHitGO.transform.position.y);
                PlayerSwitch();
            }

            if (_countClick > 0 && _rayHitGO.layer == ConstantsLayer.PIECE)
            {
                if (_selectedPiece.team != _rayHitGO.GetComponent<ChessPiece>().team)
                {
                    _countClick = 0;
                    Distributor.onChangePositionPiece?.Invoke(_selectedPiece, (int)_rayHitGO.transform.position.x, (int)_rayHitGO.transform.position.y);
                    PlayerSwitch();
                }

                if (_selectedPiece.team == _rayHitGO.GetComponent<ChessPiece>().team)
                {
                    _touchpointPiece.gameObject.SetActive(true);
                    _touchpointPiece.transform.position = _selectedPiece.transform.position;
                    _selectedPiece = _rayHitGO.GetComponent<ChessPiece>();
                    Distributor.onShowAviableMoves?.Invoke(_selectedPiece);
                }
            }
        }
    }
}
