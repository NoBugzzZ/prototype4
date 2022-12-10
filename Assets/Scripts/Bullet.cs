using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    public float forceToEnemy = 5;
    public float forceToSelf = 10;
    private Rigidbody bulletRb;
    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            bulletRb.AddForce(dir * forceToSelf, ForceMode.Force);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * forceToEnemy, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }

    public void setTarget(GameObject _target)
    {
        target = _target;
    }
}
