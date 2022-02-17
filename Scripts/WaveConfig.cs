using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy wave config")]
public class WaveConfig : ScriptableObject
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemyPath;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    //[SerializeField] float spawnDelayInSeconds = 0f;

    public GameObject GetEnemyPrefab()    { return enemyPrefab; }

    public List<Transform> GetWayPoints()
    {
        List<Transform> waveWayPoints = new List<Transform>();
        foreach (Transform child in enemyPath.transform)
        {
            waveWayPoints.Add(child);
           // Debug.Log(child.position);
        }
        return waveWayPoints;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetMoveSpeed() { return moveSpeed; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public void EnhanceGame()
    {
        moveSpeed = moveSpeed ++;
        numberOfEnemies += 2;
        timeBetweenSpawns = timeBetweenSpawns + 0.2f;
    }

    //public float GetSpawnDelayInSeconds() { return spawnDelayInSeconds; }

}
