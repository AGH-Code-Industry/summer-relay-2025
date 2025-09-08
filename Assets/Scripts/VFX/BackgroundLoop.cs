using UnityEngine;

namespace Relay.VFX
{
    public class BackgroundLoop : MonoBehaviour
    {
        [SerializeField] float length;
        [SerializeField] float speed;

        void Update()
        {
            transform.localPosition += (Vector3)(Vector2.right * Time.deltaTime * speed);
            while (transform.localPosition.x >= length * transform.localScale.x)
                transform.localPosition -= (Vector3)Vector2.right * length * transform.localScale.x;
        }
    }
}