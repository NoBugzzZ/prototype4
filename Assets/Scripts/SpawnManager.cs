using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float bound = 8;
    public float delay = 3;
    public float interval = 3;
    private int countPerWave = 1;
    public int enemyCount;
    public GameObject powerupPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        SpawnPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            SpawnEnemy();
            SpawnPowerUp();
        }
    }

    void SpawnEnemy()
    {
        for(int i = 0; i < countPerWave; i++)
        {
            int index = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[index], GenerateRandomPosition(), enemyPrefabs[index].transform.rotation);
        }
        countPerWave++;
    }

    void SpawnPowerUp()
    {
        Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
    }

    Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-bound, bound);
        float z = Random.Range(-bound, bound);
        return new Vector3(x, 0, z);
    }
}
