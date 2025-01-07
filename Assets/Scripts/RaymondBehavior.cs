using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaymondBehavior : MonoBehaviour
{
    private float speed = 2.5f;
    private float leftBound;
    private float rightBound;

    public void SetLimits(float left, float right)
    {
        leftBound = left;
        rightBound = right;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftBound - 1f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Proyectile"))
        {
            GameManager.Instance.DestroyRaymond();
            Destroy(this.gameObject);
        }
    }
}
