using System.Collections.Generic;
using UnityEngine;

public class CodeContainerController : MonoBehaviour
{

    [SerializeField]
    private TicketData _ticketData;

    [SerializeField]
    private float _iconSpacing = 0.3f;

    [SerializeField]
    private float _scale = 0.6f;

    private List<SpriteRenderer> _icons;

    private void Awake()
    {
        _icons = new List<SpriteRenderer>();
    }

    public void SpawnIcons(int code)
    {
        Debug.Log("Spawning icons");
        int index = 0;
        int digit = UtilFunctions.GetDigitAtIndex(code, index);
        while (digit != -1)
        {
            // Create a new GameObject
            GameObject newObj = new("SpriteObject" + index);

            newObj.transform.parent = transform;

            newObj.transform.localScale = new Vector3(_scale, _scale, 1.0f);

            // Set its position in a row
            newObj.transform.position = transform.position + new Vector3(index * _iconSpacing, 0, 0);

            // Add a SpriteRenderer and assign a sprite
            SpriteRenderer sr = newObj.AddComponent<SpriteRenderer>();

            sr.sortingLayerName = "ConductorTable";
            sr.sprite = _ticketData.GetSpriteForDigit(digit - 1);

            _icons.Add(sr);

            digit = UtilFunctions.GetDigitAtIndex(code, ++index);
        }
    }

    void SetSortingIndex(int index)
    {
        Debug.Log("Setting icons sorting");
        foreach (var sr in _icons)
        {
            sr.sortingOrder = index + 1;
        }
    }
}
