using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TicketController : MonoBehaviour, IObjectDropHanlder
{

    private Ticket _ticket;
    private CodeContainerController _codeController;
    private DraggableTableObject _draggableTableComponent;

    private SpriteRenderer _spriteRenderer;
    private List<TextMeshPro> _textElements;
    private ValidationStripController _validationStripController;

    private BoxBounds2D _boxBounds;

    void Awake()
    {
        _codeController = GetComponentInChildren<CodeContainerController>();
        _validationStripController = GetComponentInChildren<ValidationStripController>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _textElements = GetComponentsInChildren<TextMeshPro>().ToList();

        _draggableTableComponent = GetComponent<DraggableTableObject>();

        _boxBounds = GetComponent<BoxBounds2D>();
    }

    void Update()
    {
        
    }

    public void SetTicketData(Ticket t)
    {
        _ticket = t;
        UpdateTicket();
    }

    void UpdateTicket()
    {
        _codeController.SpawnIcons(_ticket.TripCode);

        UpdateBoundingBox();

        _validationStripController.gameObject.SetActive(!_ticket.IsValidated);
    }


    private void UpdateBoundingBox()
    {
        if (_ticket.IsValidated)
        {
            var ticketBounds = GetComponent<BoxBounds2D>();
            var colider = GetComponent<BoxCollider2D>();

            // Apply changes to the ticket collider
            ticketBounds.size = colider.size;
            ticketBounds.offset = colider.offset;
        }

    }

    public void ValidateTicket()
    {
        _ticket.IsValidated = true;
        UpdateBoundingBox();
        if (!_validationStripController.IsBeeingDestroyed())
        {
            _validationStripController.gameObject.SetActive(false);
        }
    }

    void SetSortingIndex(int index)
    {
        Debug.Log("SettingIndex");
        _spriteRenderer.sortingOrder = index;
        foreach (var t in _textElements)
        {
            t.sortingOrder = index + 1;
        }
    }

    void OnEnterTable()
    {
        transform.localScale = Vector3.one;
    }

    void OnExitTable()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        _draggableTableComponent.Offset = Vector2.zero;
    }

    public bool HandleObjectDropped(IDropZone zone)
    {
        Destroy(gameObject);
        return zone.OnObjectDropped(_ticket);
    }
}
