using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] float damping = 0.03f;

    Vector3 offset;

    void Awake()
    {
        offset = transform.position;
        transform.position = new Vector3(playerObject.transform.position.x, offset.y, offset.z);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerObject.transform.position.x, damping * Time.deltaTime), offset.y, offset.z);
    }
}
