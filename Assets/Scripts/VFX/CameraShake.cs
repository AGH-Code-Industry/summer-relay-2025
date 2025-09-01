using UnityEngine;

namespace Relay.VFX
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] AnimationCurve heightOverTime;
        [SerializeField] float amplitude;
        [SerializeField] float duration;

        float timer = 1f;
        float baseHeight;

        void Start()
        {
            baseHeight = transform.localPosition.y;
            RailEffect.OnHit += Shake;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
                timer = 0f;

            if (timer >= 1f)
                return;

            timer += Time.deltaTime * (1f / duration);
            transform.localPosition = new Vector3(transform.localPosition.x, baseHeight + heightOverTime.Evaluate(timer) * amplitude, transform.localPosition.z);
        }

        void Shake() => timer = 0f;

        void OnDestroy()
        {
            RailEffect.OnHit -= Shake;
        }
    }
}