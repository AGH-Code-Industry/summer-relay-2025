using UnityEngine;

[ExecuteAlways]
public class BoxBounds2D : Bounds2D
{
    [Tooltip("Width and height of the bounds.")]
    public Vector2 size = new Vector2(1f, 1f);

    public override Bounds Bounds
    {
        get
        {
            Vector3 center = transform.position + (Vector3)offset;
            return new Bounds(center, size);
        }
    }

    override protected void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Vector3 center = transform.position + (Vector3)offset;
        Gizmos.DrawCube(center, size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
}
