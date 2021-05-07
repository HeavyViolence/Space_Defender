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

    private SerializedProperty _scrollLerpDuration;
    private SerializedProperty _ScrollLerpDurationRandom;

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

        _scrollLerpDuration = serializedObject.FindProperty("_scrollLerpDuration");
        _ScrollLerpDurationRandom = serializedObject.FindProperty("_ScrollLerpDurationRandom");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Slider(_horizontalScrollSpeed, 0f, BackgroundScroller.ScrollSpeedMax, "Horizontal Scroll Speed");
        EditorGUILayout.Slider(_horizontalScrollSpeedRandom, 0f, 1f, "Random Factor");
        EditorGUILayout.Slider(_horizontalScrollDuration,
                               BackgroundScroller.ScrollDurationMin,
                               BackgroundScroller.ScrollDurationMax,
                               "Scroll Duration");
        EditorGUILayout.Slider(_horizontalScrollDurationRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_verticalScrollSpeed, 0f, BackgroundScroller.ScrollSpeedMax, "Vertical Scroll Speed");
        EditorGUILayout.Slider(_verticalScrollSpeedRandom, 0f, 1f, "Random Factor");
        EditorGUILayout.Slider(_verticalScrollDuration,
                               BackgroundScroller.ScrollDurationMin,
                               BackgroundScroller.ScrollDurationMax,
                               "Scroll Duration");
        EditorGUILayout.Slider(_verticalScrollDurationRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_scrollLerpDuration,
                               BackgroundScroller.ScrollLerpDurationMin,
                               BackgroundScroller.ScrollLerpDurationMax,
                               "Scroll Lerp Duration");
        EditorGUILayout.Slider(_ScrollLerpDurationRandom, 0f, 1f, "Random Factor");

        serializedObject.ApplyModifiedProperties();
    }
}
