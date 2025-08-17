using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerObject;

    public int currentTripCode;
    public int cicketCodeLength = 4;
    [Tooltip("Set to the number of icons for code")]
    public int maxTicketDigitIndex = 5;

    public TicketStationUIController ticketStationUIController;
    public ConductorTableController conductorTableController;

    public Level currentLevel;

    public int score;
    public float passedTime = 0;
    public bool isFinished;

    // temporary to be used for generating passengers
    public PassengerController defaultPassengerPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTripCode = GenerateTripCode();
        ticketStationUIController.DisplayCode(currentTripCode);

        GenerateLevel();

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinished)
        {
            passedTime += Time.deltaTime;
            ticketStationUIController.UpdateTimerUI((int)(currentLevel.totalTime - passedTime));

            if (passedTime >= currentLevel.totalTime)
            {
                Debug.Log("FINISHED!");
                isFinished = true;
            }
        }
    }

    // mostly all temporary just for basic gameplay implementation
    void GenerateLevel()
    {
        int x = 0;
        int offestX = 4;
    
        foreach (var pas in currentLevel.predefinedPassengers)
        {
            pas.ticket.tripCode = currentTripCode;
            pas.ticket.firstName = pas.firstName;
            pas.ticket.surname = pas.surname;

            if (pas.ticket.shouldInvalidityTypeBeGeneratedRandomly)
            {
                MakeTicketInvalid(pas.ticket);
            }

            InstanciatePassenger(pas, x);


            x += offestX;
        }

        for (int i = 0; i < currentLevel.totalGeneratedPassengers; i++)
        {
            Passenger pas = Passenger.CreateRandom(defaultPassengerPrefab);
            int seat = Random.Range(1, 100);
            pas.CreateAndAssignTicket(currentTripCode, seat, 0, 0);

            if (i < currentLevel.generatedPasengersWithInvalidTicket)
            {
                pas.ticket.invalidityType = UtilFunctions.GetRandomEnumValue<InvalidTicketReason>(1);
                Debug.Log(pas.ticket.invalidityType);
                MakeTicketInvalid(pas.ticket);
            }

            InstanciatePassenger(pas, x);
            x += offestX;
        }
    }

    PassengerController InstanciatePassenger(Passenger pas, int x)
    {
        PassengerController p = Instantiate(pas.passengerPrefab);
        p.SetData(pas);

        p.transform.position = new Vector3(x, p.transform.position.y, p.transform.position.z);

        return p;
    }

    void MakeTicketInvalid(Ticket t)
    {
        switch (t.invalidityType)
        {
            case InvalidTicketReason.InvalidTripCode:
                int index = Random.Range(0, cicketCodeLength);
                int currentDigit = UtilFunctions.GetDigitAtIndex(index, t.tripCode);
                int digit = ((currentDigit + Random.Range(1, maxTicketDigitIndex)) % maxTicketDigitIndex);
                if (digit == 0) digit = 1;
                t.tripCode = UtilFunctions.ReplaceDigitAtIndex(t.tripCode, index, digit);
                break;
        }
    }

    int GenerateTripCode()
    {
        int code = 0;

        for (int i = 0, multi = 1; i <= cicketCodeLength; i++, multi *= 10)
        {
            code += Random.Range(1, maxTicketDigitIndex + 1) * multi;
        }

        return code;
    }

    public void StartTicketChecking(Ticket t)
    {
        conductorTableController.AddTicketToCheck(t);
    }

    // ticket should be returned validated if is valid, and not valided with allpied correct reason
    public void CheckTicketValidation(Ticket t)
    {
        // picked arbirtarily, should be changed for better gameplay
        int punishemtValue = 10;
        if (t.invalidityType == InvalidTicketReason.None)
        {
            if (t.isValidated)
            {
                AddToScore(punishemtValue);
            }
            else
            {
                AddToScore(-punishemtValue);
            }
        }
        else
        {
            if (t.invalidityType == t.appliedInvalidReason)
            {
                AddToScore(punishemtValue);
            }
            else
            {
                AddToScore(-punishemtValue);
            }
        }
    }

    public void CommitedInvalidAction(InvalidActions a)
    {
        if (a == InvalidActions.GiveTicketBackToWrongOwner)
        {
            AddToScore(-10);
        }
    }

    private void AddToScore(int s)
    {
        if (!isFinished)
        {
            score += s;
            score = Mathf.Max(0, score);

            ticketStationUIController.UpdateScoreUI(score);
        }
    }
}
