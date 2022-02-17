using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPath : MonoBehaviour
{
    WaveConfig waveConfig;

    List<Transform> wayPointsList;
    int wayPointIndex = 0;

    private void Start()
    {
        wayPointsList = waveConfig.GetWayPoints();
        //foreach (Transform ceva in wayPointsList)
        //Debug.Log(ceva.transform.position);
        transform.position = wayPointsList[wayPointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (wayPointIndex <= wayPointsList.Count - 1)
        {
            var targetPosition = wayPointsList[wayPointIndex].transform.position;
            var perFrameMovement = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, perFrameMovement);
            if (transform.position == targetPosition)
                wayPointIndex++;
            //else
            //Debug.Log("Da");
        }
        else
        {
            Destroy(gameObject);
            FindObjectOfType<EnemySpawner>().EliminateEnemies();
            //Debug.Log("NU");
        }
    }
}
