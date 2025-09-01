using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TicketData", menuName = "Scriptable Objects/TicketData")]
public class TicketData : ScriptableObject
{
    [SerializeField]
    private List<Sprite> sprites;

    public Sprite GetSpriteForDigit(int d)
    {
        return sprites[d];
    }
}
