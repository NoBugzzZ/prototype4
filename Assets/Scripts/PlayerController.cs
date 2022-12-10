using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;
    public float speed=5;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool isPowerUp = false;
    public float force = 20;
    private GameObject powerUpIndicator;
    private bool isFired = false;
    public GameObject bulletPrefab;

    public float attackRandius=5;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        powerUpIndicator = GameObject.Find("PowerUp Indicator");
        powerUpIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
        powerUpIndicator.transform.position = gameObject.transform.position + new Vector3(0, -0.5f, 0);
        powerUpIndicator.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.y, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * 20, ForceMode.Impulse);
        }
        if (transform.position.y > 5)
        {
            playerRb.AddForce(Vector3.down * 20, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isPowerUp)
        {
            Vector3 dir = (collision.gameObject.transform.position - gameObject.transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);
        }else if (collision.gameObject.CompareTag("Island")&&isPowerUp)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0, len = enemies.Length; i < len; i++)
            {
                Vector3 vec = enemies[i].transform.position - transform.position;
                if (vec.magnitude <= attackRandius)
                {
                    enemies[i].gameObject.GetComponent<Rigidbody>().AddForce(vec.normalized * force, ForceMode.Impulse);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            isPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(StopPowerUp());
            powerUpIndicator.SetActive(true);
            StartCoroutine(Fire());
        }
    }

    IEnumerator StopPowerUp()
    {
        yield return new WaitForSeconds(5);
        isPowerUp = false;
        powerUpIndicator.SetActive(false);
    }

    IEnumerator Fire()
    {
        while (isPowerUp && isFired)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0, len = enemies.Length; i < len; i++)
            {
                Vector3 dir = (enemies[i].transform.position - transform.position).normalized;
                bulletPrefab.GetComponent<Bullet>().setTarget(enemies[i]);
                Instantiate(bulletPrefab, transform.position + dir * 2, bulletPrefab.transform.rotation);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

}
