using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;

    public float damping = 0.03f;
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerObject.transform.position.x, damping), 0, -10);
    }
}
