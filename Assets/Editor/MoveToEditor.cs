using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MoveTo))]
public class MoveToEditor : Editor
{
    float size = 0.1f;

    private void OnSceneGUI()
    {
        MoveTo selected = target as MoveTo;

        if (selected.localPosition) {
            Vector3 movePoint = Handles.FreeMoveHandle(selected.moveTo + selected.transform.position, Quaternion.identity, size, new Vector3(0.1f, 0.1f, 0f), Handles.DotHandleCap);
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(target, "Changed Move Point");

                selected.moveTo = movePoint - selected.transform.position;
            }
        } else {
            Vector3 movePoint = Handles.FreeMoveHandle(selected.moveTo, Quaternion.identity, size, new Vector3(0.1f, 0.1f, 0f), Handles.DotHandleCap);
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(target, "Changed Move Point");

                selected.moveTo = movePoint;
            }
        }


        Handles.color = Color.blue;

        if (selected.localPosition) {
            Handles.DrawLine(selected.transform.position, selected.moveTo + selected.transform.position);
        } else {
            Handles.DrawLine(selected.transform.position, selected.moveTo);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
