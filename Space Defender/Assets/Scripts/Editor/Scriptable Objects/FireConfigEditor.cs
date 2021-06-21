using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FireConfig))]
public class FireConfigEditor : Editor
{
    private FireConfig _config = null;

    private SerializedProperty _fireType;

    private SerializedProperty _sprayEnabled;
    private SerializedProperty _sprayShells;
    private SerializedProperty _sprayShellsRandom;

    private SerializedProperty _fireDelayEnabled;
    private SerializedProperty _fireDelay;
    private SerializedProperty _fireDelayRandom;

    private SerializedProperty _damage;
    private SerializedProperty _damageRandom;

    private SerializedProperty _speed;
    private SerializedProperty _speedRandom;

    private SerializedProperty _rotationEnabled;
    private SerializedProperty _rpm;
    private SerializedProperty _rpmRandom;

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

    private SerializedProperty _hitEffect;
    private SerializedProperty _hitEffectDuration;

    private SerializedProperty _shotAudio;

    private void OnEnable()
    {
        _config = (FireConfig)serializedObject.targetObject;

        _fireType = serializedObject.FindProperty("_fireType");

        _sprayEnabled = serializedObject.FindProperty("_sprayEnabled");

        _sprayShells = serializedObject.FindProperty("_sprayShells");
        _sprayShellsRandom = serializedObject.FindProperty("_sprayShellsRandom");

        _fireDelayEnabled = serializedObject.FindProperty("_fireDelayEnabled");
        _fireDelay = serializedObject.FindProperty("_fireDelay");
        _fireDelayRandom = serializedObject.FindProperty("_fireDelayRandom");

        _damage = serializedObject.FindProperty("_damage");
        _damageRandom = serializedObject.FindProperty("_damageRandom");

        _speed = serializedObject.FindProperty("_speed");
        _speedRandom = serializedObject.FindProperty("_speedRandom");

        _rotationEnabled = serializedObject.FindProperty("_rotationEnabled");
        _rpm = serializedObject.FindProperty("_rpm");
        _rpmRandom = serializedObject.FindProperty("_rpmRandom");

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

        _hitEffect = serializedObject.FindProperty("_hitEffect");
        _hitEffectDuration = serializedObject.FindProperty("_hitEffectDuration");

        _shotAudio = serializedObject.FindProperty("_shotAudio");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("General Fire Properties:");

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_fireType, new GUIContent("Fire Type"));

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_sprayEnabled, new GUIContent("Enable Spray"));

        if (_config.SprayEnabled)
        {
            EditorGUILayout.IntSlider(_sprayShells, FireConfig.MinSprayShells, FireConfig.MaxSprayShells, "Spray Shells Amount");
            EditorGUILayout.Slider(_sprayShellsRandom, 0f, 1f, "Random Factor");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_fireDelayEnabled, new GUIContent("Enable Fire Delay"));

        if (_config.FireDelayEnabled)
        {
            EditorGUILayout.Slider(_fireDelay, FireConfig.MinFireDelay, FireConfig.MaxFireDelay, "Fire Delay");
            EditorGUILayout.Slider(_fireDelayRandom, 0f, 1f, "Random Factor");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_damage, FireConfig.MinDamage, FireConfig.MaxDamage, "Damage");
        EditorGUILayout.Slider(_damageRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_speed, FireConfig.MinSpeed, FireConfig.MaxSpeed, "Projectile Speed");
        EditorGUILayout.Slider(_speedRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_rotationEnabled, new GUIContent("Enable Rotation"));

        if (_config.RotationEnabled)
        {
            EditorGUILayout.Slider(_rpm, FireConfig.MinRPM, FireConfig.MaxRPM, "RPM");
            EditorGUILayout.Slider(_rpmRandom, 0f, 1f, "Random Factor");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_dispersion, FireConfig.MinDispersion, FireConfig.MaxDispersion, "Dispersion");
        EditorGUILayout.Slider(_dispersionRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_fireDuration, FireConfig.MinFireDuration, FireConfig.MaxFireDuration, "Fire Duration");
        EditorGUILayout.Slider(_fireDurationRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_cooldown, FireConfig.MinCoolDown, FireConfig.MaxCooldown, "Cooldown");
        EditorGUILayout.Slider(_cooldownRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.Slider(_fireRate, FireConfig.MinFireRate, FireConfig.MaxFireRate, "Fire Rate");
        EditorGUILayout.Slider(_fireRateRandom, 0f, 1f, "Random Factor");

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_infiniteAmmo, new GUIContent("Infinite Ammo"));

        if (!_config.InfiniteAmmo) EditorGUILayout.IntSlider(_initialAmmo, 0, FireConfig.MaxAmmo, "Initial Ammo");

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Fire Prefabs:");

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_projectile, new GUIContent("Projectile Prefab"));

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_hitEffect, new GUIContent("Projectile Hit Prefab"));

        if (_config.HitEffect != null)
        {
            EditorGUILayout.Slider(_hitEffectDuration,
                                   FireConfig.MinHitEffectDuration,
                                   FireConfig.MaxHitEffectDuration,
                                   "Hit Effect Duration");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(_shotAudio, new GUIContent("Shot Audio"));

        serializedObject.ApplyModifiedProperties();
    }
}
