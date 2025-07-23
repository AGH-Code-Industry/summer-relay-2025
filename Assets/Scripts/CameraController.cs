using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;
    void Update()
    {
        transform.position = new Vector3(
            playerObject.transform.position.x,
            0,
            -10
            );
    }
}
