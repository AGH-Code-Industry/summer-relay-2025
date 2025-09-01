using System;
using System.Collections;
using UnityEngine;

namespace Relay.VFX
{
    public class RailEffect : MonoBehaviour
    {
        public static Action OnHit;

        [SerializeField] float delay1;
        [SerializeField] float delay2;

        void Start()
        {
            StartCoroutine(RailEffectLoop());
        }

        IEnumerator RailEffectLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(delay1);
                OnHit?.Invoke();
                yield return new WaitForSeconds(delay2);
                OnHit?.Invoke();
            }
        }
    }
}