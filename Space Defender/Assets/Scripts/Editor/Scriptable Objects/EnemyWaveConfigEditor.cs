using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyWaveConfig))]
public class EnemyWaveConfigEditor : Editor
{
    private EnemyWaveConfig _target;

    private SerializedProperty _enemyPrefabs;

    private SerializedProperty _infiniteEnemies;

    private SerializedProperty _enemiesAmount;
    private SerializedProperty _enemiesAmountRandom;

    private SerializedProperty _spawnDelay;
    private SerializedProperty _spawnDelayRandom;

    private SerializedProperty _spawnStartupDelay;
    private SerializedProperty _spawnStartupDelayRandom;

    private SerializedProperty _spawnProbabilityCurve;

    private void OnEnable()
    {
        _target = (EnemyWaveConfig)serializedObject.targetObject;

        _enemyPrefabs = serializedObject.FindProperty("_enemyPrefabs");

        _infiniteEnemies = serializedObject.FindProperty("_infiniteEnemies");

        _enemiesAmount = serializedObject.FindProperty("_enemiesAmount");
        _enemiesAmountRandom = serializedObject.FindProperty("_enemiesAmountRandom");

        _spawnDelay = serializedObject.FindProperty("_spawnDelay");
        _spawnDelayRandom = serializedObject.FindProperty("_spawnDelayRandom");

        _spawnStartupDelay = serializedObject.FindProperty("_spawnStartupDelay");
        _spawnStartupDelayRandom = serializedObject.FindProperty("_spawnStartupDelayRandom");

        _spawnProbabilityCurve = serializedObject.FindProperty("_spawnProbabilityCurve");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_enemyPrefabs, new GUIContent("Enemies"));

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_infiniteEnemies, new GUIContent("Infinite Enemies"));

        if (!_target.InfiniteEnemies)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.IntSlider(_enemiesAmount, 0, EnemyWaveConfig.MaxEnemiesAmount, "Enemies Amount");
            EditorGUILayout.Slider(_enemiesAmountRandom, 0f, 1f, "Random Factor");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_spawnDelay, 0f, EnemyWaveConfig.MaxSpawnDelay, "Spawn Delay");
        EditorGUILayout.Slider(_spawnDelayRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_spawnStartupDelay, 0f, EnemyWaveConfig.MaxSpawnStartupDelay, "Spawn Startup Delay");
        EditorGUILayout.Slider(_spawnStartupDelayRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_spawnProbabilityCurve, new GUIContent("Spawn Probability Curve"));

        serializedObject.ApplyModifiedProperties();
    }
}
