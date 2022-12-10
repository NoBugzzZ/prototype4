using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarderEnemy : MonoBehaviour
{
    public float force = 5;
    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 dir = (playerRb.gameObject.transform.position - gameObject.transform.position).normalized;
            playerRb.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}
