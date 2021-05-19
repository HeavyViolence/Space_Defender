using UnityEditor;

[CustomEditor(typeof(ProjectileMovementConfig))]
public class ProjectileMovementConfigEditor : Editor
{
    private SerializedProperty _speed;
    private SerializedProperty _speedRandom;

    private void OnEnable()
    {
        _speed = serializedObject.FindProperty("_speed");
        _speedRandom = serializedObject.FindProperty("_speedRandom");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Slider(_speed, 0f, ProjectileMovementConfig.MaxSpeed, "Speed");
        EditorGUILayout.Slider(_speedRandom, 0f, 1f, "Random Factor");

        serializedObject.ApplyModifiedProperties();
    }
}
