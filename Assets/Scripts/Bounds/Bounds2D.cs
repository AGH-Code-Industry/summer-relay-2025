using UnityEngine;
public abstract class Bounds2D : MonoBehaviour
{
    [Header("Bounds Settings")]
    [Tooltip("Offset relative to this GameObject's position.")]
    public Vector2 offset = Vector2.zero;

    public abstract Bounds Bounds { get; }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0f, 0f, 0f);
    }
}

