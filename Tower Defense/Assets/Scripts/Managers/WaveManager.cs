using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int indexEnemyPool;
    [SerializeField] private float timeToSpawn;
    private float refTime;
    [SerializeField] private int indexWave = 0;
    [SerializeField] private float waveBase;
    [SerializeField] private float expo;
    private IEnumerator spawnWave;
    private int ammountEnemys;
    private int enemyDone;


    private void Start()
    {
        refTime = timeToSpawn;
        ActionsController.OnLaunchGame += OnLaunchWave;
        ActionsController.OnEnemyDone += OnEnemyDone;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnEnemy(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnEnemy(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnEnemy(2);
        }
    }

    private void OnDestroy()
    {
        ActionsController.OnLaunchGame -= OnLaunchWave;
        ActionsController.OnEnemyDone -= OnEnemyDone;
    }

    void OnLaunchWave()
    {
        indexWave++;
        ammountEnemys = (int) ((waveBase * indexWave) * expo);
        enemyDone = 0;
        OnStartSpawnWave(ammountEnemys);
    }

    void OnStartSpawnWave(int enemyCont)
    {
        if (spawnWave != null)
        {
            StopCoroutine(spawnWave);
        }

        spawnWave = SpawnWaves(enemyCont);
        StartCoroutine(spawnWave);
    }

    IEnumerator SpawnWaves(int enemyCont)
    {
        refTime = timeToSpawn;
        int count = 0;

        while (count < enemyCont)
        {
            yield return null;

            refTime += Time.deltaTime;
            if (refTime >= timeToSpawn)
            {
                if (indexWave < 3)
                {
                    SpawnEnemy(Random.Range(0, 2));
                }
                else
                {
                    SpawnEnemy(Random.Range(0, 3));
                }
                refTime = 0;
                count++;
            }
        }
    }

    void SpawnEnemy(int indexPool)
    {
        GameObject go = PoolManager.Instance.GetObject(indexPool);
        go.GetComponent<Enemy>().Initialize();
    }

    void OnEnemyDone()
    {
        enemyDone++;
        if (enemyDone == ammountEnemys)
        {
            Invoke("OnLaunchWave", 5f);
        }
    }
}