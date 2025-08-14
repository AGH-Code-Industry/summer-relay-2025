#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxBounds2D))]
public class BoxBounds2DEditor : Editor
{
    void OnSceneGUI()
    {
        BoxBounds2D box = (BoxBounds2D)target;
        Vector3 center = box.transform.position + (Vector3)box.offset;

        EditorGUI.BeginChangeCheck();
        Vector3 newSize = Handles.ScaleHandle(
            box.size,
            center,
            Quaternion.identity,
            HandleUtility.GetHandleSize(center)
        );
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(box, "Resize Box Bounds");
            box.size = newSize;
        }
    }
}
#endif
