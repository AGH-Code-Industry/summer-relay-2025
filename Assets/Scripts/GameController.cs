using UnityEngine;

public class GamecONTROLLER : MonoBehaviour
{
    public GameObject playerObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void ClampPlayerMovement()
    {
        float clampedX = Mathf.Clamp(playerObject.transform.position.x, -64f, 64f);
        float clampedY = Mathf.Clamp(playerObject.transform.position.y, -1.15f, 0.29f);
        playerObject.transform.position = new Vector3(clampedX, clampedY, playerObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        ClampPlayerMovement();
    }
}
