using UnityEngine;

[CreateAssetMenu(fileName = "Passenger", menuName = "Scriptable Objects/Passenger")]
public class Passenger : ScriptableObject
{
    public string firstName;
    public string surname;
    public int age;

    public PassengerController passengerPrefab;

    public Ticket ticket;

    // Prefab jest aktualnie bardzo temporary zrobiony, ig double the problem and give to the next person
    public static Passenger CreateRandom(PassengerController prefab)
    {
        Passenger newPassenger = CreateInstance<Passenger>();

        string[] names = { "Janek", "Patyk", "Mateusz", "Mikołaj", "Beata", "Maciej", "Filip" };
        string[] surnames = { "Krzyszkowski", "Lesiak", "Pawliczek", "Gosztyła", "Orlińska", "Pieniążek", "Kowalski" };

        newPassenger.firstName = names[Random.Range(0, names.Length)];
        newPassenger.surname = surnames[Random.Range(0, surnames.Length)];
        newPassenger.age = Random.Range(18, 65);

        newPassenger.passengerPrefab = prefab;

        return newPassenger;
    }

    public void CreateAndAssignTicket(int currentTripCode, int seat, int from, int to)
    {
        ticket = new Ticket
        {
            surname = surname,
            firstName = firstName,
            tripCode = currentTripCode,
            seatNumber = seat,
            to = to,
            from = from
        };
    }
}
