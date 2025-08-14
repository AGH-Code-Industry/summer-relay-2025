using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[ExecuteAlways]
public class CircleBounds2D : Bounds2D
{
    [Tooltip("Radius of the circle bounds.")]
    public float radius = 0.5f;

    public override Bounds Bounds
    {
        get
        {
            Vector3 center = transform.position + (Vector3)offset;
            return new Bounds(center, Vector3.one * radius * 2f);
        }
    }

    override protected void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Vector3 center = transform.position + (Vector3)offset;
        Gizmos.DrawSphere(center, radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center, radius);
    }
}