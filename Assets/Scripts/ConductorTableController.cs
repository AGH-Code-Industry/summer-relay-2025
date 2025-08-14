using UnityEngine;

public class ConductorTableController : MonoBehaviour
{
    public TicketController ticketPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTicketToCheck(Ticket t)
    {
        TicketController newTicket = Instantiate(ticketPrefab, transform);

        newTicket.SetTicketData(t);
    }
}
