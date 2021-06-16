using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Enemy Wave Config", menuName = "Configs/Enemy Wave Config")]
public class EnemyWaveConfig : ScriptableObject
{
    public const int MaxEnemiesAmount = 100;
    public const float MaxSpawnDelay = 10f;
    public const float MaxSpawnStartupDelay = 10f;

    private int _selectedEnemyIndex = 0;

    [SerializeField] private List<GameObject> _enemyPrefabs = null;

    [SerializeField] private bool _infiniteEnemies = false;

    [SerializeField] private int _enemiesAmount = 0;
    [SerializeField] private float _enemiesAmountRandom = 0f;

    [SerializeField] private float _spawnDelay = 0f;
    [SerializeField] private float _spawnDelayRandom = 0f;

    [SerializeField] private float _spawnStartupDelay = 0f;
    [SerializeField] private float _spawnStartupDelayRandom = 0f;

    [SerializeField] private AnimationCurve _spawnProbabilityCurve = null;

    public int EnemiesAmount { get; private set; } = 0;

    public float SpawnDelay => AuxMath.Randomize(_spawnDelay, _spawnDelayRandom);

    public float SpawnStartupDelay => AuxMath.Randomize(_spawnStartupDelay, _spawnStartupDelayRandom);

    public int EnemiesSpawned { get; private set; } = 0;

    public int EnemiesLeftToSpawn { get; private set; } = 0;

    public bool InfiniteEnemies => _infiniteEnemies;

    private void OnEnable()
    {
        SetupEnemyWaveConfig();
    }

    private void SetupEnemyWaveConfig()
    {
        if (InfiniteEnemies)
        {
            EnemiesAmount = int.MaxValue;
            EnemiesLeftToSpawn = int.MaxValue;
        }
        else
        {
            EnemiesAmount = Mathf.RoundToInt(AuxMath.Randomize(_enemiesAmount, _enemiesAmountRandom));
            EnemiesLeftToSpawn = EnemiesAmount;
        }

        EnemiesSpawned = 0;
        _selectedEnemyIndex = 0;
    }

    public GameObject GetRandomEnemy()
    {
        if (_enemyPrefabs.Count == 0)
            throw new Exception("Enemy list is empty!");

        if (InfiniteEnemies)
        {
            EnemiesSpawned++;

            return Perform();
        }
        else
        {
            if (EnemiesLeftToSpawn > 0)
            {
                EnemiesSpawned++;
                EnemiesLeftToSpawn--;

                return Perform();
            }
            else return null;
        }

        GameObject Perform() =>
            _enemyPrefabs[UnityEngine.Random.Range(0, _enemyPrefabs.Count)];
    }

    public GameObject GetNextEnemy()
    {
        if (_enemyPrefabs.Count == 0)
            throw new Exception("Enemy list is empty!");

        if (InfiniteEnemies)
        {
            EnemiesSpawned++;

            return Perform();
        }
        else
        {
            if (EnemiesLeftToSpawn > 0)
            {
                EnemiesSpawned++;
                EnemiesLeftToSpawn--;

                return Perform();
            }
            else return null;
        }

        GameObject Perform() =>
            _enemyPrefabs[(int)Mathf.Repeat(_selectedEnemyIndex++, _enemyPrefabs.Count - 1f)];
    }

    public GameObject GetProbableEnemy()
    {
        if (_enemyPrefabs.Count == 0)
            throw new Exception("Enemy list is empty!");

        if (InfiniteEnemies)
        {
            EnemiesSpawned++;

            return Perform();
        }
        else
        {
            if (EnemiesLeftToSpawn > 0)
            {
                EnemiesSpawned++;
                EnemiesLeftToSpawn--;

                return Perform();
            }
            else return null;
        }

        GameObject Perform()
        {
            float valueThreshold = UnityEngine.Random.Range(0f, 1f);

            for (int i = _enemyPrefabs.Count - 1; i >= 0; i--)
            {
                float enemyPos = (float)i / _enemyPrefabs.Count;
                float enemyValue = _spawnProbabilityCurve.Evaluate(enemyPos);

                if (enemyValue > valueThreshold) return _enemyPrefabs[i];
            }

            return _enemyPrefabs[0];
        }
    }
}
