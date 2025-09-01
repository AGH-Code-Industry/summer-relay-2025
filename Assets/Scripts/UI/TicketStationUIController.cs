using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TicketStationUIController : MonoBehaviour
{
    public Image spritePrefab;
    public Transform container;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

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

    public void UpdateScoreUI(int s)
    {
        scoreText.text = s.ToString();
    }

    public void UpdateTimerUI(int timeLeft)
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
