using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DraggableTableObject : MonoBehaviour
{
    public Vector3 Offset;
    private Camera _mainCam;
    private int _sortIndex;
    private Bounds2D _bounds;
    private BoxBounds2D _container;
    private bool _isOnTable = true;
    private Collider2D _collider;
    private IObjectDropHanlder _dropHanlder;

    static private int TopSortingIndex = 0;

    [Header("Sorting Settings")]
    [Tooltip("Each object reserves the amount specified for sorting values")]
    [SerializeField]
    static private int SortReserve = 5;

    [Header("Bounds Settings")]
    [Tooltip("Object whose Colider2D will be used as a container, when not set parent will be used")]
    [SerializeField]
    private GameObject _containerObject;

    [Tooltip("Set to false to allow object to exit from specific side")]
    [Header("Clamp Sides")]
    public bool clampLeft = true;
    public bool clampRight = true;
    public bool clampTop = true;
    public bool clampBottom = true;


    void Awake()
    {
        _mainCam = Camera.main;
        _bounds = GetComponent<Bounds2D>();
        _dropHanlder = GetComponentInChildren<IObjectDropHanlder>();
        _collider = GetComponent<Collider2D>();

        if (_containerObject == null)
        {
            _container = transform.parent.gameObject.GetComponent<BoxBounds2D>();
            _containerObject = transform.parent.gameObject;
        } else
        {
            _container = _containerObject.GetComponent<BoxBounds2D>();
        }
    }

    private void Start()
    {
        ForceToTop();
    }

    void OnMouseDown()
    {
        BringToTop();
        Vector3 mouseWorldPos = GetMouseWorldPos();
        Offset = transform.position - mouseWorldPos;
    }

    private void OnMouseUp()
    {
        bool succesful = false;

        if (_dropHanlder != null)
        {
            _collider.enabled = false;
            Collider2D hit = Physics2D.OverlapPoint(GetMouseWorldPos());
            _collider.enabled = true;

            if (hit != null)
            {
                IDropZone z = hit.gameObject.GetComponent<IDropZone>();
                if (z != null)
                {
                    succesful = _dropHanlder.HandleObjectDropped(z);
                }
            }
        }

        if (!succesful && !_isOnTable)
        {
            transform.position = _containerObject.transform.position;
            _isOnTable = true;
            BroadcastMessage("OnEnterTable");
        }
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + Offset;
        HandleMouseOutsideTable();
    }

    public Vector3 GetMouseWorldPos()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = -_mainCam.transform.position.z;
        return _mainCam.ScreenToWorldPoint(mouseScreenPos);
    }

    private void LateUpdate()
    {
        ClampPosition();
    }

    private void ClampPosition()
    {
        UnityEngine.Bounds containerBounds = _container.Bounds;
        UnityEngine.Bounds objectBounds = _bounds.Bounds;

        Vector3 pos = transform.position;

        if (clampLeft)
            pos.x = Mathf.Max(pos.x, containerBounds.min.x + objectBounds.extents.x - _bounds.offset.x);
        if (clampRight)
            pos.x = Mathf.Min(pos.x, containerBounds.max.x - objectBounds.extents.x - _bounds.offset.x);
        if (clampBottom)
            pos.y = Mathf.Max(pos.y, containerBounds.min.y + objectBounds.extents.y - _bounds.offset.y);
        if (clampTop)
            pos.y = Mathf.Min(pos.y, containerBounds.max.y - objectBounds.extents.y - _bounds.offset.y);

        transform.position = pos;
    }

    /// <summary>
    /// Brings the object to the top of all sorting layers.
    /// Uses _sortReserve to avoid conflicts with other objects.
    /// Use this to avoid unnecesary calls when object already is on top :D
    /// </summary>
    public void BringToTop()
    {
        Debug.Log("GoingToTop " + TopSortingIndex + ", " + _sortIndex);
        if (_sortIndex < TopSortingIndex - SortReserve)
        {
            ForceToTop();
        }
    }

    /// <summary>
    /// Brings the object to the top of all sorting layers.
    /// Uses _sortReserve to avoid conflicts with other objects.
    /// </summary>
    public void ForceToTop()
    {
        _sortIndex = TopSortingIndex;
        TopSortingIndex += SortReserve;
        Debug.Log("GoingToTop");
        // Broadcast message so that object can set correct sorting index on self and children
        BroadcastMessage("SetSortingIndex", _sortIndex);
    }

    public int GetSortingIndex()
    {
        return _sortIndex;
    }

    public int GetTopSortingIndex()
    {
        return TopSortingIndex;
    }

    private void HandleMouseOutsideTable()
    {
        bool isMouseOnTable = UtilFunctions.Contains2D(_container.Bounds, GetMouseWorldPos());

        if (isMouseOnTable && !_isOnTable)
        {
            _isOnTable = true;
            BroadcastMessage("OnEnterTable");
        }
        else if (!isMouseOnTable && _isOnTable)
        {
            _isOnTable = false;
            BroadcastMessage("OnExitTable");
        }
    }
}
