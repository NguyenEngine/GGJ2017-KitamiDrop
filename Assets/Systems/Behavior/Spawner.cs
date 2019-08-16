using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner> {

    public GameObject enemySceneParent;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    public LifeScript playerLife;

    void Awake()
    {
        spawnPoints.Remove(null);
        enemyPrefabs.Remove(null);
    }

    private bool CanSpawnEnemy()
    {
        return !playerLife || playerLife.IsAlive();
    }

    public void SpawnRandomEnemies(int number)
    {
        if (!CanSpawnEnemy())
            return;

        GameObject enemy = (enemyPrefabs.Count > 0) ? enemyPrefabs[Random.Range(0, enemyPrefabs.Count)] : null;
        if (!enemy)
            return;
        
        for (int i = 0; i < number; ++i)
        {
            List<GameObject> unblockedSpawns = new List<GameObject>();
            List<GameObject> blockedSpawns = new List<GameObject>();
            foreach (GameObject candidateSpawn in spawnPoints)
            {
                if (Physics2D.OverlapCircle(candidateSpawn.transform.position, 0.1f))
                    blockedSpawns.Add(candidateSpawn);
                else
                    unblockedSpawns.Add(candidateSpawn);
            }

            List<GameObject> selectedList = unblockedSpawns.Count > 0 ? unblockedSpawns : blockedSpawns;
            GameObject spawn = (selectedList.Count > 0) ? selectedList[Random.Range(0, selectedList.Count - 1)] : null;
            if (spawn)
            {
                GameObject newEnemy = Instantiate(enemy);
                newEnemy.transform.position = spawn.transform.position;

                if (enemySceneParent)
                    newEnemy.transform.parent = enemySceneParent.transform;
            }
        }
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.W))
            SpawnRandomEnemies(5);
         */
    }


}
