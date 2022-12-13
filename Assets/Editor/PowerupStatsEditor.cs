using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PowerupStats))]
public class PowerupStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PowerupStats selected = target as PowerupStats;

        //base.OnInspectorGUI();

        EditorGUILayout.LabelField("Health Stats", EditorStyles.boldLabel);
        serializedObject.FindProperty("health").floatValue = EditorGUILayout.FloatField("Health", serializedObject.FindProperty("health").floatValue);
        serializedObject.FindProperty("shield").floatValue = EditorGUILayout.FloatField("Shield", serializedObject.FindProperty("shield").floatValue);

        serializedObject.FindProperty("maxHealth").floatValue = EditorGUILayout.FloatField("Max Health", serializedObject.FindProperty("maxHealth").floatValue);
        serializedObject.FindProperty("maxShield").floatValue = EditorGUILayout.FloatField("Max Shield", serializedObject.FindProperty("maxShield").floatValue);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Bullet Attributes", EditorStyles.boldLabel);
        serializedObject.FindProperty("bulletDamageAddition").floatValue = EditorGUILayout.FloatField("Bullet Damage Addition", serializedObject.FindProperty("bulletDamageAddition").floatValue);
        serializedObject.FindProperty("bulletDamageMultiplier").floatValue = EditorGUILayout.FloatField("Bullet Damage Multiplier", serializedObject.FindProperty("bulletDamageMultiplier").floatValue);
        serializedObject.FindProperty("bulletSpeedAddition").floatValue = EditorGUILayout.FloatField("Bullet Speed Addition", serializedObject.FindProperty("bulletSpeedAddition").floatValue);
        serializedObject.FindProperty("bulletSpeedMultiplier").floatValue = EditorGUILayout.FloatField("Bullet Speed Multiplier", serializedObject.FindProperty("bulletSpeedMultiplier").floatValue);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Special Projectile", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("specialSpawn"), new GUIContent("Special Spawn"));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Time", EditorStyles.boldLabel);
        serializedObject.FindProperty("permanent").boolValue = EditorGUILayout.Toggle("Permanent", serializedObject.FindProperty("permanent").boolValue);
        if (!serializedObject.FindProperty("permanent").boolValue) {
            serializedObject.FindProperty("duration").floatValue = EditorGUILayout.FloatField("Duration", serializedObject.FindProperty("duration").floatValue);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
