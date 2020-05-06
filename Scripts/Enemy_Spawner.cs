using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    private float spawnSpeed;
    public GameObject enemies; // enemy entity 
    public Transform[] spawnPoints;
    private int enemiesWaveCount;
    private int level;
    [HideInInspector] public int killCount;
    private float intervalBetweenWaves;
    private bool SpawnOnce; 
    // Start is called before the first frame update
    void Start()
    {
        SpawnOnce = true;
        level = 1;
        enemiesWaveCount = level * 5;
        killCount = 0;
        spawnSpeed = 2.5f;
        intervalBetweenWaves = 25f; // change in line 67 as well
    }

    // Update is called once per frame
    void Update()
    {
        LevelManager();
    }

    #region LevelManger
    private void LevelManager()
    {
        if(enemiesWaveCount != 0 && killCount == 0)//New Wave
        {
            if (SpawnOnce)
            {
                StarSpawning(spawnSpeed);
                SpawnOnce = false;
            }
        }

        else if(enemiesWaveCount <= 0)//stop spawning
        {
            CancelInvoke();
        }

        if(killCount >= level * 5)//if player killed all enemies, CountDown start
        {
           GameManager.instance.GetComponent<GameManager>().CountDownStart(intervalBetweenWaves);
           intervalBetweenWaves -= Time.deltaTime; 
        }

        if (intervalBetweenWaves <= 0)//CountDown stop, Reset Wave
        {
            GameManager.instance.GetComponent<GameManager>().CountDownStop();
            level++;
            enemiesWaveCount = level * 5;
            killCount = 0;
            spawnSpeed -= 0.1f;
            GameManager.instance.GetComponent<GameManager>().WaveCompleteNotification();
            SpawnOnce = true;
            GameManager.instance.wave++;
            intervalBetweenWaves = 25f;
        }
    }

    private void SpawnEnemies()
    {
        int n = UnityEngine.Random.Range(0, spawnPoints.Length);

        Instantiate(enemies, spawnPoints[n].position, spawnPoints[n].rotation);
        enemiesWaveCount--;
    }

    public void StarSpawning(float time)
    {
        if(time < 0.1f)
        {
            time = 0.1f;//fastest spawntime is 0.1f intervals 
        }

        InvokeRepeating("SpawnEnemies", 1, time);
    }
    #endregion
}
