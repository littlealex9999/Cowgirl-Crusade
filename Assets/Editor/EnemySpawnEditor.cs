using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawn))]
public class EnemySpawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //base.OnInspectorGUI();

        GUILayout.Label("New Path will not be used if a new leader is set");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("newThingToFollow"), new GUIContent("New Rails Leader"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("newPath"), new GUIContent("New Path"));

        if (serializedObject.FindProperty("newPath").objectReferenceValue != null) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("newMovePoint"), new GUIContent("Set Next Path Point"));
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("destroyOnPlayerEnter"), new GUIContent("Destroy On Player Enter"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("forceOntoPath"), new GUIContent("Force Onto Path"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("setRotation"), new GUIContent("Set Rotation To Leader"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyMoveScripts"), new GUIContent("Enemy Move Scripts"));

        serializedObject.ApplyModifiedProperties();
    }
}
