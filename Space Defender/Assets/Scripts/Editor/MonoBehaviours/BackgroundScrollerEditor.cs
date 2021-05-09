using UnityEditor;

[CustomEditor(typeof(BackgroundScroller))]
public class BackgroundScrollerEditor : Editor
{
    private SerializedProperty _horizontalScrollSpeed;
    private SerializedProperty _horizontalScrollSpeedRandom;
    private SerializedProperty _horizontalScrollDuration;
    private SerializedProperty _horizontalScrollDurationRandom;

    private SerializedProperty _verticalScrollSpeed;
    private SerializedProperty _verticalScrollSpeedRandom;
    private SerializedProperty _verticalScrollDuration;
    private SerializedProperty _verticalScrollDurationRandom;

    private SerializedProperty _transitionDuration;
    private SerializedProperty _transitionDurationRandom;

    private void OnEnable()
    {
        _horizontalScrollSpeed = serializedObject.FindProperty("_horizontalScrollSpeed");
        _horizontalScrollSpeedRandom = serializedObject.FindProperty("_horizontalScrollSpeedRandom");
        _horizontalScrollDuration = serializedObject.FindProperty("_horizontalScrollDuration");
        _horizontalScrollDurationRandom = serializedObject.FindProperty("_horizontalScrollDurationRandom");

        _verticalScrollSpeed = serializedObject.FindProperty("_verticalScrollSpeed");
        _verticalScrollSpeedRandom = serializedObject.FindProperty("_verticalScrollSpeedRandom");
        _verticalScrollDuration = serializedObject.FindProperty("_verticalScrollDuration");
        _verticalScrollDurationRandom = serializedObject.FindProperty("_verticalScrollDurationRandom");

        _transitionDuration = serializedObject.FindProperty("_transitionDuration");
        _transitionDurationRandom = serializedObject.FindProperty("_transitionDurationRandom");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Slider(_horizontalScrollSpeed, 0f, BackgroundScroller.MaxScrollSpeed, "Horizontal Scroll Speed");
        EditorGUILayout.Slider(_horizontalScrollSpeedRandom, 0f, 1f, "Random Factor");
        EditorGUILayout.Slider(_horizontalScrollDuration,
                               BackgroundScroller.MinScrollDuration,
                               BackgroundScroller.MaxScrollDuration,
                               "Scroll Duration");
        EditorGUILayout.Slider(_horizontalScrollDurationRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_verticalScrollSpeed, 0f, BackgroundScroller.MaxScrollSpeed, "Vertical Scroll Speed");
        EditorGUILayout.Slider(_verticalScrollSpeedRandom, 0f, 1f, "Random Factor");
        EditorGUILayout.Slider(_verticalScrollDuration,
                               BackgroundScroller.MinScrollDuration,
                               BackgroundScroller.MaxScrollDuration,
                               "Scroll Duration");
        EditorGUILayout.Slider(_verticalScrollDurationRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_transitionDuration,
                               BackgroundScroller.MinScrollLerpDuration,
                               BackgroundScroller.MaxScrollLerpDuration,
                               "Transition Duration");
        EditorGUILayout.Slider(_transitionDurationRandom, 0f, 1f, "Random Factor");

        serializedObject.ApplyModifiedProperties();
    }
}
