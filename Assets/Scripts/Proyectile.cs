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
        if (other.CompareTag("Creature") || other.CompareTag("Bound") || other.CompareTag("Proyectile"))
        {
            if (specialProyectile == true && other.CompareTag("Creature"))
            {
                Destroy(this.GetComponent<BoxCollider>());
                Destroy(this.GetComponent<Rigidbody>());
                proyectileShouldMove = false;
                area = this.gameObject.transform.GetChild(0).gameObject;
                LeanTween.scale(area, new Vector3(0, 0, 0), 0f);
                area.SetActive(true);
                LeanTween.scale(area, new Vector3(17f, 6f, 2f), 0.35f).setEase(LeanTweenType.easeInOutCirc);
                StartCoroutine(ShootVisualCooldown());
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else if (other.CompareTag("ShipModel"))
        {
            Destroy(this.gameObject);
        }
    }

    public IEnumerator ShootVisualCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        destroyProyectile = true;
    }

}
