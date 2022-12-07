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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isPowerUp)
        {
            Vector3 dir = (collision.gameObject.transform.position - gameObject.transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);
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
        }
    }

    IEnumerator StopPowerUp()
    {
        yield return new WaitForSeconds(5);
        isPowerUp = false;
        powerUpIndicator.SetActive(false);
    }
}
