using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 10f;

    //Special shoot
    private GameObject area;
    private GameObject areaVisual;
    public bool specialProyectile = false;
    private bool proyectileShouldMove = true;
    private bool destroyProyectile = false;
    private float cooldown = 0.35f;

    private void Update()
    {
        if (proyectileShouldMove == true)
        {
            this.transform.position += this.direction * speed * Time.deltaTime;
        }

        if (destroyProyectile == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Limit"))
        {
            if (specialProyectile == true && !other.CompareTag("Bound"))
            {
                area = this.gameObject.transform.GetChild(0).gameObject;
                area.SetActive(true);
                proyectileShouldMove = false;
                StartCoroutine(ShootVisualCooldown());
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public IEnumerator ShootVisualCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        destroyProyectile = true;
    }

}
