using UnityEngine;
public struct ConstantsLayer
{
    public const int TILE_LAYER = 7;
    public const int SELECTED_TILE_LAYER = 8;
    public const int PIECE = 10;
}
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _touchpointPiece;
    private ChessPiece _selectedPiece;
    private RaycastHit2D rayHit;
    private int _countClick = 0;

    private void Update()
    {
        SelectObject();
    }

    private void SelectObject()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rayHit = Physics2D.Raycast(cursorPosition, Vector2.zero);

        if (Input.GetMouseButtonDown(0)) // перести логику в скрипт фигур
        {
            Tile.onSetDefautColor?.Invoke();
            if (rayHit.collider != null && rayHit.collider.gameObject.layer == ConstantsLayer.PIECE)
            {
                if (_countClick == 0)
                {
                   
                    _touchpointPiece.gameObject.SetActive(true);
                    _selectedPiece = rayHit.collider.gameObject.GetComponent<ChessPiece>();
                    _touchpointPiece.transform.position = _selectedPiece.transform.position;
                    _countClick++;
                    Distributor.onSwonAviableMoves?.Invoke(_selectedPiece, (int)rayHit.collider.transform.position.x, (int)rayHit.collider.transform.position.y);
                }
            }

            if (_countClick > 0 && rayHit.collider.gameObject.layer == ConstantsLayer.TILE_LAYER)
            {
                
                _touchpointPiece.gameObject.SetActive(false);
                _touchpointPiece.transform.position = transform.position;
                _countClick = 0;
                Distributor.onChangePositionPiece?.Invoke(_selectedPiece, (int)rayHit.collider.transform.position.x,
                                                 (int)rayHit.collider.transform.position.y);                
            }

            if (_countClick > 0 && rayHit.collider.gameObject.layer == ConstantsLayer.PIECE)
            {
                if (_selectedPiece.team != rayHit.collider.gameObject.GetComponent<ChessPiece>().team)
                {
                    rayHit.collider.gameObject.SetActive(false); // перенести в скрипт фигурки
                    _touchpointPiece.gameObject.SetActive(false);

                    _touchpointPiece.transform.position = transform.position;
                    _countClick = 0;
                    Distributor.onChangePositionPiece?.Invoke(_selectedPiece, (int)rayHit.collider.transform.position.x, (int)rayHit.collider.transform.position.y);
                }

                if (_selectedPiece.team == rayHit.collider.gameObject.GetComponent<ChessPiece>().team)
                {
                    _touchpointPiece.gameObject.SetActive(true);
                    _selectedPiece = rayHit.collider.gameObject.GetComponent<ChessPiece>();
                    _touchpointPiece.transform.position = _selectedPiece.transform.position;



                    Distributor.onSwonAviableMoves?.Invoke(_selectedPiece, (int)rayHit.collider.transform.position.x, (int)rayHit.collider.transform.position.y);
                }
            }
        }
    }
}
