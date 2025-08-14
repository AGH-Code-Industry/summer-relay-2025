using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerObject;

    public int currentTripCode;
    public int cicketCodeLength = 4;
    public int maxTickerIndex = 5;

    public TicketStationUIController ticketStationUIController;
    public ConductorTableController conductorTableController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTripCode = GenerateTripCode();
        ticketStationUIController.DisplayCode(currentTripCode);
    }

    // Update is called once per frame
    void Update()
    {

    }

    int GenerateTripCode()
    {
        int code = 0;

        for (int i = 0, multi = 1; i <= cicketCodeLength; i++, multi *= 10)
        {
            code += Random.Range(1, maxTickerIndex + 1) * multi;
        }

        return code;
    }

    public void startTicketChecking(Ticket t)
    {
        conductorTableController.AddTicketToCheck(t);
    }
}
