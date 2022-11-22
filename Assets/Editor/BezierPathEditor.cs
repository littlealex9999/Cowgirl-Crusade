using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(BezierPath))]
public class BezierPathEditor : Editor
{
    SerializedProperty editorList;
    private BezierPath selectedScript;

    float size = 0.1f;

    bool pathDirtied;

    Texture noButtonsImage;

    private void OnEnable()
    {
        SceneView.duringSceneGui += CustomOnSceneGUI;
        editorList = serializedObject.FindProperty("pathPoints");

        noButtonsImage = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Images/NoButtons.jpg", typeof(Texture));
    }

    private void CustomOnSceneGUI(SceneView sceneView)
    {
        selectedScript = target as BezierPath;

        if (selectedScript != null) {
            if (selectedScript.pathPoints.Count <= 2 || selectedScript.controlPoints.Count < selectedScript.pathPoints.Count * 2 - 1) {
                return;
            }

            for (int i = 0; i < selectedScript.pathPoints.Count - 1; ++i) {
                int j = i * 2;

                #region bezier handles
                // START
                Handles.color = Color.green;

                EditorGUI.BeginChangeCheck();

                Vector3 sp = Handles.FreeMoveHandle(selectedScript.pathPoints[i], Quaternion.identity, size, new Vector3(0.1f, 0.1f, 0f), Handles.DotHandleCap);

                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(target, "Changed Bezier Start");

                    Vector3 cpOff = selectedScript.controlPoints[j] - selectedScript.pathPoints[i];

                    selectedScript.pathPoints[i] = sp;

                    selectedScript.controlPoints[j] = cpOff + selectedScript.pathPoints[i];
                    if (j - 1 >= 0) {
                        selectedScript.controlPoints[j - 1] = cpOff * -1 + selectedScript.pathPoints[i];
                    }

                    pathDirtied = true;
                }

                // END
                Handles.color = Color.red;

                EditorGUI.BeginChangeCheck();

                Vector3 ep;
                if (selectedScript.pathPoints.Count <= i + 2) {
                    ep = Handles.FreeMoveHandle(selectedScript.pathPoints[i + 1], Quaternion.identity, size, new Vector3(0.1f, 0.1f, 0f), Handles.DotHandleCap);
                } else {
                    ep = selectedScript.pathPoints[i + 1];
                }

                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(target, "Changed Bezier End");

                    Vector3 cpOff = selectedScript.controlPoints[j + 1] - selectedScript.pathPoints[i + 1];

                    selectedScript.pathPoints[i + 1] = ep;

                    selectedScript.controlPoints[j + 1] = cpOff + selectedScript.pathPoints[i + 1];

                    pathDirtied = true;
                }

                // P2
                Handles.color = Color.magenta;

                EditorGUI.BeginChangeCheck();

                Vector3 p2 = Handles.FreeMoveHandle(selectedScript.controlPoints[j], Quaternion.identity, size, new Vector3(0.1f, 0.1f, 0f), Handles.DotHandleCap);

                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(target, "Changed Bezier Control Point");

                    selectedScript.controlPoints[j] = p2;
                    if (j != 0) {
                        selectedScript.controlPoints[j - 1] = (selectedScript.pathPoints[i] - p2) + selectedScript.pathPoints[i];
                    }

                    pathDirtied = true;
                }

                // P3
                Handles.color = Color.magenta;

                EditorGUI.BeginChangeCheck();

                Vector3 p3 = Handles.FreeMoveHandle(selectedScript.controlPoints[j + 1], Quaternion.identity, size, new Vector3(0.1f, 0.1f, 0f), Handles.DotHandleCap);

                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(target, "Changed Bezier Control Point");

                    selectedScript.controlPoints[j + 1] = p3;
                    if (j != selectedScript.controlPoints.Count) {
                        selectedScript.controlPoints[j + 2] = (selectedScript.pathPoints[i + 1] - p3) + selectedScript.pathPoints[i + 1];
                    }

                    pathDirtied = true;
                }

                Handles.color = Color.blue;

                Handles.DrawLine(sp, p2);
                Handles.DrawLine(ep, p3);
                #endregion

                Handles.DrawBezier(selectedScript.pathPoints[i], selectedScript.pathPoints[i + 1], selectedScript.controlPoints[j], selectedScript.controlPoints[j + 1], Color.yellow, Texture2D.whiteTexture, 1f);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // change bezier control point count on edit points list
        if (selectedScript != null) {
            while (selectedScript.controlPoints.Count != selectedScript.pathPoints.Count * 2 - 1) {
                if (selectedScript.controlPoints.Count < selectedScript.pathPoints.Count * 2 - 1) {
                    if (selectedScript.controlPoints.Count != 0) {
                        selectedScript.controlPoints.Add(selectedScript.controlPoints.Last());
                    } else {
                        selectedScript.controlPoints.Add(Vector3.zero);
                    }
                } else {
                    selectedScript.controlPoints.Remove(selectedScript.controlPoints.Last());
                }
            }


            GUILayout.BeginHorizontal();
            GUILayout.Label("Calculated Bezier Points");
            if (selectedScript.getPath != null) {
                GUILayout.Label(selectedScript.getPath.Count().ToString());
            } else {
                GUILayout.Label("0");
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Generate Path") && selectedScript != null) {
                selectedScript.GeneratePath();
                pathDirtied = false;
            }

            if (pathDirtied) {
                if (noButtonsImage != null) {
                    GUILayout.Box(noButtonsImage);
                }

                GUILayout.Box("GENERATE A NEW PATH \n OH GOD, PLEASE DO IT NOW \n YOU'LL DOOM US ALL IF YOU DON'T \n GODDAMN IT IT'S JUST ONE SINGLE BUTTON HOW ARE YOU NOT HITTING IT");
            }
        }
    }
}
