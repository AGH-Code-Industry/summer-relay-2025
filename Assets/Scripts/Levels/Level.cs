using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    [Tooltip("In sec")]
    public int totalTime;

    [Header("Generated Passengers settings")]
    public int totalGeneratedPassengers;
    [Tooltip("How many of the generated passengers should have invalid tickets")]
    public int generatedPasengersWithInvalidTicket;

    [Header("Passengers settings")]
    public Passenger[] predefinedPassengers;


    public int TotalPassengers { 
        get
        {
            return totalGeneratedPassengers + predefinedPassengers.Length;
        }
    }
}
