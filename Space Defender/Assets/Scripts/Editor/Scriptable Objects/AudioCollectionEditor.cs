using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioCollection))]
public class AudioCollectionEditor : Editor
{
    private SerializedProperty _clips;
    private SerializedProperty _group;

    private SerializedProperty _volume;
    private SerializedProperty _volumeRandom;

    private SerializedProperty _priority;

    private SerializedProperty _spatialBlend;

    private void OnEnable()
    {
        _clips = serializedObject.FindProperty("_clips");
        _group = serializedObject.FindProperty("_group");

        _volume = serializedObject.FindProperty("_volume");
        _volumeRandom = serializedObject.FindProperty("_volumeRandom");

        _priority = serializedObject.FindProperty("_priority");

        _spatialBlend = serializedObject.FindProperty("_spatialBlend");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_clips, new GUIContent("Audio Clips"));

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_group, new GUIContent("Audio Mixer Group"));

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_volume, 0f, 1f, "Volume");
        EditorGUILayout.Slider(_volumeRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.IntSlider(_priority, 0, AudioCollection.MaxPriority, "Priority");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_spatialBlend, 0f, 1f, "Spatial Blend");

        serializedObject.ApplyModifiedProperties();
    }
}
