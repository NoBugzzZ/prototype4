using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject playerGObj;
    private Rigidbody enemyRb;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerGObj = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (playerGObj.transform.position - transform.position).normalized;
        enemyRb.AddForce(dir * speed);
    }
}
