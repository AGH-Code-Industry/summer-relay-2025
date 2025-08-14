using UnityEngine;

public class ValidationStripController : MonoBehaviour
{
    private TicketController _ticketControler;

    private Vector3 _startPosition;
    private Camera _mainCam;
    private SpriteRenderer _spriteRenderer;
    private float _rightTicketSidePos;
    private bool _isFlyingOff = false;
    private Vector3 _flyDirection;
    private float _timePassed = 0;

    [SerializeField]
    private float _breakPoint = 5;
    [SerializeField]
    private float _destroyDelay = 2f;
    [SerializeField]
    private float _rotationSpeed = 180f;
    [SerializeField]
    private float _flySpeed = 2f;
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    void Awake()
    {
        _mainCam = Camera.main;
        _ticketControler = transform.parent.gameObject.GetComponent<TicketController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rightTicketSidePos = transform.localPosition.x - _spriteRenderer.sprite.bounds.size.x / 2.0f;
        _flyDirection = new Vector3(0.6f, -1f, 0f);
    }

    void Update()
    {
        if (_isFlyingOff)
        {
            _timePassed += Time.deltaTime;

            transform.position += _flyDirection * _flySpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
            transform.localScale -= transform.localScale * (Time.deltaTime / _destroyDelay);
        }
    }

    void StartFlyingOff()
    {
        _isFlyingOff = true;
        _ticketControler.ValidateTicket();
        Invoke(nameof(DestroySelf), _destroyDelay);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        _startPosition = GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        float xPos = _rightTicketSidePos + _spriteRenderer.sprite.bounds.size.x / 2.0f;
        transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
    }

    void OnMouseDrag()
    {
        float distance = Vector2.Distance(_startPosition, GetMouseWorldPos());
        float t = distance / _breakPoint;

        float scaleX = Mathf.Lerp(1.0f, 1.3f, scaleCurve.Evaluate(t));
        float scaleY = Mathf.Lerp(1.0f, 0.95f, scaleCurve.Evaluate(t));

        transform.localScale = new Vector3(scaleX, scaleY, 1);

        float spriteSizeScaled = _spriteRenderer.sprite.bounds.size.x * scaleX;

        transform.localPosition = new Vector3(_rightTicketSidePos + spriteSizeScaled / 2.0f, transform.localPosition.y, transform.localPosition.z);

        if (distance > _breakPoint)
        {
            StartFlyingOff();
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = -_mainCam.transform.position.z;
        return _mainCam.ScreenToWorldPoint(mouseScreenPos);
    }

    void SetSortingIndex(int index)
    {
        _spriteRenderer.sortingOrder = index;
    }

    public bool IsBeeingDestroyed()
    {
        return _isFlyingOff;
    }
}
