using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RailsEnemyMovement))]
public class RailsEnemyMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //base.OnInspectorGUI();

        GUILayout.Label("If a leader is assigned, Rail To Follow will not be used.");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("thingToFollow"), new GUIContent("Rails Movement Leader"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("pathToFollow"), new GUIContent("Rail To Follow"));

        EditorGUILayout.Space();

        if (serializedObject.FindProperty("thingToFollow").objectReferenceValue != null) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("targetSpeed"), new GUIContent("Target Speed"));
        } else {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"), new GUIContent("Speed"));
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationSpeed"), new GUIContent("Rotation Speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("loopAtEnd"), new GUIContent("Loop At End"));

        if (serializedObject.FindProperty("thingToFollow").objectReferenceValue != null) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("numPointsAhead"), new GUIContent("Number Of Points Ahead"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ignoreDeviation"), new GUIContent("Ignore Deviation?"));
            if (!serializedObject.FindProperty("ignoreDeviation").boolValue) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maxDeviation"), new GUIContent("Max Deviation"));
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("slowDownMult"), new GUIContent("Slow Down Multiplier"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
