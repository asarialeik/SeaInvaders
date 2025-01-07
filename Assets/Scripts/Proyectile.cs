using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 10f;

    private void Update()
    {
        this.transform.position += this.direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Limit"))
        {
            Destroy(this.gameObject);
        }
    }
}
