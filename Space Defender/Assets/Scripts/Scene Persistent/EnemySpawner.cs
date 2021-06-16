using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SceneSingleton<EnemySpawner>
{
    public const float SpawnAreaWidth = 0.75f;
    public const float SpawnAreaHeightOffset = 0.25f;

    [SerializeField] private List<EnemyWaveConfig> _enemyWaves = null;

    public int TotalEnemiesSpawned { get; private set; } = 0;

    public bool SpawnPaused { get; private set; } = false;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    public void PauseSpawn()
    {
        SpawnPaused = true;
    }

    public void ContinueSpawn()
    {
        SpawnPaused = false;
    }

    private IEnumerator Spawner()
    {
        foreach (var wave in _enemyWaves)
        {
            yield return new WaitForSeconds(wave.SpawnStartupDelay);

            while (wave.EnemiesLeftToSpawn > 0)
            {
                yield return new WaitForSeconds(wave.SpawnDelay);
                yield return !SpawnPaused;

                TotalEnemiesSpawned++;
                Instantiate(wave.GetProbableEnemy(), GetSpawnPos(), Quaternion.identity);
            }
        }
    }

    private Vector3 GetSpawnPos()
    {
        float x = Random.Range(CameraHolder.Instance.LeftBound * SpawnAreaWidth,
                               CameraHolder.Instance.RightBound * SpawnAreaWidth);

        float y = CameraHolder.Instance.UpperBound * (1f + SpawnAreaHeightOffset);

        return new Vector3(x, y, 0f);
    }
}