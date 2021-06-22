using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DurabilityConfig))]
public class DurabilityConfigEditor : Editor
{
    private SerializedProperty _maxDurability;
    private SerializedProperty _maxDurabilityRandom;

    private SerializedProperty _reconstructionEnabled;

    private SerializedProperty _reconstructionRate;
    private SerializedProperty _reconstructionRateRandom;

    private SerializedProperty _reconstructionBounded;

    private SerializedProperty _reconstructionLowerBound;
    private SerializedProperty _reconstructionUpperBound;

    private SerializedProperty _explosionEffect;
    private SerializedProperty _explosionDuration;

    private SerializedProperty _explosionAudio;

    private DurabilityConfig _target = null;

    private void OnEnable()
    {
        _maxDurability = serializedObject.FindProperty("_maxDurability");
        _maxDurabilityRandom = serializedObject.FindProperty("_maxDurabilityRandom");

        _reconstructionEnabled = serializedObject.FindProperty("_reconstructionEnabled");

        _reconstructionRate = serializedObject.FindProperty("_reconstructionRate");
        _reconstructionRateRandom = serializedObject.FindProperty("_reconstructionRateRandom");

        _reconstructionBounded = serializedObject.FindProperty("_reconstructionBounded");

        _reconstructionLowerBound = serializedObject.FindProperty("_reconstructionLowerBound");
        _reconstructionUpperBound = serializedObject.FindProperty("_reconstructionUpperBound");

        _explosionEffect = serializedObject.FindProperty("_explosionEffect");
        _explosionDuration = serializedObject.FindProperty("_explosionDuration");

        _explosionAudio = serializedObject.FindProperty("_explosionAudio");

        _target = (DurabilityConfig)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Slider(_maxDurability,
                               DurabilityConfig.DurabilityFloor,
                               DurabilityConfig.DurabilityCeiling,
                               "Max Durability");
        EditorGUILayout.Slider(_maxDurabilityRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_reconstructionEnabled, new GUIContent("Enable Reconstruction"));

        if (_target.ReconstructionEnabled)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.Slider(_reconstructionRate, 0f, DurabilityConfig.MaxReconstructionRate, "Reconstruction Rate");
            EditorGUILayout.Slider(_reconstructionRateRandom, 0f, 1f, "Random Factor");

            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_reconstructionBounded, new GUIContent("Restrict Reconstruction"));

            if (_target.ReconstructionBounded)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.Slider(_reconstructionLowerBound, 0f, 0.5f, "Lower Bound");
                EditorGUILayout.Slider(_reconstructionUpperBound, 0.5f, 1f, "Upper Bound");
            }
        }

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_explosionEffect, new GUIContent("Explosion Effect"));

        if (_target.ExplosionEffect != null)
        {
            EditorGUILayout.Slider(_explosionDuration,
                               DurabilityConfig.MinExplosionDuration,
                               DurabilityConfig.MaxExplosionDuration,
                               "Explosion Duration");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_explosionAudio, new GUIContent("Explosion Audio"));

        serializedObject.ApplyModifiedProperties();
    }
}
