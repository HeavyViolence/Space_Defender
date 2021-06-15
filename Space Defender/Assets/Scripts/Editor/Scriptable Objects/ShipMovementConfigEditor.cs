using UnityEditor;

[CustomEditor(typeof(ShipMovementConfig))]
public class ShipMovementConfigEditor : Editor
{
    private SerializedProperty _xSpeed;
    private SerializedProperty _xSpeedRandom;

    private SerializedProperty _xSpeedDuration;
    private SerializedProperty _xSpeedDurationRandom;

    private SerializedProperty _xSpeedTransitionDuration;
    private SerializedProperty _xSpeedTransitionDurationRandom;

    private SerializedProperty _horizontalBoundsOffset;

    private SerializedProperty _ySpeed;
    private SerializedProperty _ySpeedRandom;

    private SerializedProperty _ySpeedDuration;
    private SerializedProperty _ySpeedDurationRandom;

    private SerializedProperty _ySpeedTransitionDuration;
    private SerializedProperty _ySpeedTransitionDurationRandom;

    private SerializedProperty _upperBoundOffset;
    private SerializedProperty _lowerBoundOffset;

    private void OnEnable()
    {
        _xSpeed = serializedObject.FindProperty("_xSpeed");
        _xSpeedRandom = serializedObject.FindProperty("_xSpeedRandom");

        _xSpeedDuration = serializedObject.FindProperty("_xSpeedDuration");
        _xSpeedDurationRandom = serializedObject.FindProperty("_xSpeedDurationRandom");

        _xSpeedTransitionDuration = serializedObject.FindProperty("_xSpeedTransitionDuration");
        _xSpeedTransitionDurationRandom = serializedObject.FindProperty("_xSpeedTransitionDurationRandom");

        _horizontalBoundsOffset = serializedObject.FindProperty("_horizontalBoundsOffset");

        _ySpeed = serializedObject.FindProperty("_ySpeed");
        _ySpeedRandom = serializedObject.FindProperty("_ySpeedRandom");

        _ySpeedDuration = serializedObject.FindProperty("_ySpeedDuration");
        _ySpeedDurationRandom = serializedObject.FindProperty("_ySpeedDurationRandom");

        _ySpeedTransitionDuration = serializedObject.FindProperty("_ySpeedTransitionDuration");
        _ySpeedTransitionDurationRandom = serializedObject.FindProperty("_ySpeedTransitionDurationRandom");

        _upperBoundOffset = serializedObject.FindProperty("_upperBoundOffset");
        _lowerBoundOffset = serializedObject.FindProperty("_lowerBoundOffset");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Horizontal Parameters:");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_xSpeed, 0f, ShipMovementConfig.MaxSpeed, "Speed");
        EditorGUILayout.Slider(_xSpeedRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_xSpeedDuration, 0f, ShipMovementConfig.MaxSpeedDuration, "Duration");
        EditorGUILayout.Slider(_xSpeedDurationRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_xSpeedTransitionDuration, 0f, ShipMovementConfig.MaxSpeedTransitionDuration, "Transition Duration");
        EditorGUILayout.Slider(_xSpeedTransitionDurationRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_horizontalBoundsOffset, -0.5f, 0.5f, "Bounds Offset");

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Vertical Parameters:");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_ySpeed, 0f, ShipMovementConfig.MaxSpeed, "Speed");
        EditorGUILayout.Slider(_ySpeedRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_ySpeedDuration, 0f, ShipMovementConfig.MaxSpeedDuration, "Duration");
        EditorGUILayout.Slider(_ySpeedDurationRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_ySpeedTransitionDuration, 0f, ShipMovementConfig.MaxSpeedTransitionDuration, "Transition Duration");
        EditorGUILayout.Slider(_ySpeedTransitionDurationRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_upperBoundOffset, -2f, 0f, "Upper Bound Offset");
        EditorGUILayout.Slider(_lowerBoundOffset, -2f, 1f, "Lower Bound Offset");

        serializedObject.ApplyModifiedProperties();
    }
}
