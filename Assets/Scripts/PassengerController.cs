using UnityEngine;

public class PassengerController : MonoBehaviour, IInteractable, IDropZone
{

    private bool _isInteractable = true;
    private Passenger _passengerData;

    public GameController gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
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
        if (_passengerData.ticket != null)
        {
            gameController.StartTicketChecking(_passengerData.ticket);
            _passengerData.ticket = null;
        }
    }

    public bool OnObjectDropped<T>(T item)
    {
        if (item is Ticket t)
        {
            // consume the ticket but invalid action
            if (_passengerData.ticket != null)
            {
                gameController.CommitedInvalidAction(InvalidActions.GiveTicketBackToWrongOwner);
                return true;
            }

            _passengerData.ticket = t;
            gameController.CheckTicketValidation(t);
            return true;
        }

        return false;
    }

    public bool IsOpen()
    {
        return true;
    }

    public void SetData(Passenger p)
    {
        _passengerData = p;
    }
}
