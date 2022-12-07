using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float bound = 8;
    public float delay = 3;
    public float interval = 3;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", delay, interval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn()
    {

        Instantiate(enemyPrefab, GenerateRandomPosition(), enemyPrefab.transform.rotation);
    }

    Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-bound, bound);
        float z = Random.Range(-bound, bound);
        return new Vector3(x, 0, z);
    }
}
