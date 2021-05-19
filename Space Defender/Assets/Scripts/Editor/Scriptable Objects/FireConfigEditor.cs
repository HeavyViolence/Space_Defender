using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FireConfig))]
public class FireConfigEditor : Editor
{
    private FireConfig _target = null;

    private SerializedProperty _fireType;

    private SerializedProperty _damage;
    private SerializedProperty _damageRandom;

    private SerializedProperty _dispersion;
    private SerializedProperty _dispersionRandom;

    private SerializedProperty _fireDuration;
    private SerializedProperty _fireDurationRandom;

    private SerializedProperty _cooldown;
    private SerializedProperty _cooldownRandom;

    private SerializedProperty _fireRate;
    private SerializedProperty _fireRateRandom;

    private SerializedProperty _infiniteAmmo;
    private SerializedProperty _initialAmmo;

    private SerializedProperty _projectile;
    private SerializedProperty _muzzleFlash;
    private SerializedProperty _hitEffect;

    private void OnEnable()
    {
        _target = (FireConfig)serializedObject.targetObject;

        _fireType = serializedObject.FindProperty("_fireType");

        _damage = serializedObject.FindProperty("_damage");
        _damageRandom = serializedObject.FindProperty("_damageRandom");

        _dispersion = serializedObject.FindProperty("_dispersion");
        _dispersionRandom = serializedObject.FindProperty("_dispersionRandom");

        _fireDuration = serializedObject.FindProperty("_fireDuration");
        _fireDurationRandom = serializedObject.FindProperty("_fireDurationRandom");

        _cooldown = serializedObject.FindProperty("_cooldown");
        _cooldownRandom = serializedObject.FindProperty("_cooldownRandom");

        _fireRate = serializedObject.FindProperty("_fireRate");
        _fireRateRandom = serializedObject.FindProperty("_fireRateRandom");

        _infiniteAmmo = serializedObject.FindProperty("_infiniteAmmo");
        _initialAmmo = serializedObject.FindProperty("_initialAmmo");

        _projectile = serializedObject.FindProperty("_projectile");
        _muzzleFlash = serializedObject.FindProperty("_muzzleFlash");
        _hitEffect = serializedObject.FindProperty("_hitEffect");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("General Fire Properties:");

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_fireType, new GUIContent("Fire Type"));

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_damage, 0f, FireConfig.MaxDamage, "Damage");
        EditorGUILayout.Slider(_damageRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_dispersion, 0f, FireConfig.MaxDispersion, "Dispersion");
        EditorGUILayout.Slider(_dispersionRandom, 0f, 1f, "Random Fatcor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_fireDuration, 0f, FireConfig.MaxFireDuration, "Fire Duration");
        EditorGUILayout.Slider(_fireDurationRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_cooldown, 0f, FireConfig.MaxCooldown, "Cooldown");
        EditorGUILayout.Slider(_cooldownRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_fireRate, 0f, FireConfig.MaxFireRate, "Fire Rate");
        EditorGUILayout.Slider(_fireRateRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_infiniteAmmo, new GUIContent("Infinite Ammo"));
        if (!_target.InfiniteAmmo) EditorGUILayout.IntSlider(_initialAmmo, 0, FireConfig.MaxAmmo, "Initial Ammo");

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Fire Prefabs:");

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_projectile, new GUIContent("Projectile Prefab"));
        EditorGUILayout.PropertyField(_muzzleFlash, new GUIContent("Muzzle Flash Prefab"));
        EditorGUILayout.PropertyField(_hitEffect, new GUIContent("Projectile Hit Prefab"));

        serializedObject.ApplyModifiedProperties();
    }
}
