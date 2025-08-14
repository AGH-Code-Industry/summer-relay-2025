using UnityEngine;

public class Passenger : MonoBehaviour, IInteractable, IDropZone
{

    private bool _isInteractable = true;
    private Ticket _ticket;

    public GameController gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ticket = new Ticket(0, 123, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CanInteract()
    {
        return _isInteractable;
    }

    public void Interact()
    {
        if (_ticket != null)
        {
            gameController.startTicketChecking(_ticket);
            _ticket = null;
        }
    }

    public bool OnObjectDropped<T>(T item)
    {
        if (item is Ticket t)
        {
            _ticket = t;
            return true;
        }

        return false;
    }

    public bool IsOpen()
    {
        return true;
    }
}
