using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;
    [SerializeField] bool loop = false;
    [SerializeField] GameObject nextSpawner;
    [SerializeField] GameObject upgrades;
    [SerializeField] int enemyNumber;

    bool ok = false;
    bool upgradesSpawned = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        enemyNumber = 0;
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
            ok = true;
            //EnhanceArmament();
        } while (loop);
    }

    private void Update()
    {
        if(ok == true && upgradesSpawned == false)
            if(enemyNumber <= 0)
            {
                StartCoroutine(SpawnUpgradesCanvas());
                upgradesSpawned = true;
            }
    }

    private IEnumerator SpawnAllWaves()
    {
        int i;
        for (i = 0; i < waveConfigs.Count; i++)
        {
            WaveConfig currentWave = waveConfigs[i];
            enemyNumber += currentWave.GetNumberOfEnemies();
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for (int i = 0; i < currentWave.GetNumberOfEnemies(); i++)
        {
            GameObject newEnemy = Instantiate(currentWave.GetEnemyPrefab(), currentWave.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(currentWave);
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
        }
    }

    private void EnhanceArmament()
    {
        foreach (WaveConfig auxWaveConfig in waveConfigs)
            auxWaveConfig.EnhanceGame();
    }

    private IEnumerator SpawnUpgradesCanvas()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(upgrades, transform.position, Quaternion.identity);
    }

    public void InstantiateNextSpawner()
    {
        if (nextSpawner != null)
        {
            Instantiate(nextSpawner, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void EliminateEnemies()
    {
        enemyNumber--;
    }
}
