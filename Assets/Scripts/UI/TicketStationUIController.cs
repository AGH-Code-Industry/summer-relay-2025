using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TicketStationUIController : MonoBehaviour
{
    public Image spritePrefab;
    public Transform container;

    [SerializeField]
    private TicketData ticketData;

    public void DisplayCode(int code)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        int index = 0;
        int digit = UtilFunctions.GetDigitAtIndex(code, index);
        while (digit != -1)
        {

            Image img = Instantiate(spritePrefab, container);
            img.sprite = ticketData.GetSpriteForDigit(digit - 1);

            digit = UtilFunctions.GetDigitAtIndex(code, ++index);
        }
    }
}
