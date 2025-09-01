using UnityEngine;

[System.Serializable]
public class Ticket //: ScriptableObject
{
    public int id;

    // temporarily set to int, person that will be implementing some sort of station controller should change this
    public int from;
    public int to;
    public int seatNumber;

    [Header("Invalidity")]
    public InvalidTicketReason invalidityType = InvalidTicketReason.None;
    public bool shouldInvalidityTypeBeGeneratedRandomly = false;

    [Header("Auto filled values, no need to set")]
    public int tripCode;
    public string firstName;
    public string surname;
    public bool isValidated = false;

    public InvalidTicketReason appliedInvalidReason;
}