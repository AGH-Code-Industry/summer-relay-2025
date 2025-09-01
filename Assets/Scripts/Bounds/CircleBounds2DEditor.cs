#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircleBounds2D))]
public class CircleBounds2DEditor : Editor
{
    private void OnSceneGUI()
    {
        CircleBounds2D circle = (CircleBounds2D)target;
        Vector3 center = circle.transform.position + (Vector3)circle.offset;

        EditorGUI.BeginChangeCheck();

        // Draw a radius handle
        float newRadius = Handles.RadiusHandle(
            Quaternion.identity, // rotation
            center,             // handle position
            circle.radius       // current radius
        );

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(circle, "Resize Circle Bounds");
            circle.radius = Mathf.Max(0f, newRadius); // clamp radius to non-negative
        }
    }
}
#endif
