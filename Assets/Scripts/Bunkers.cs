using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunkers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Proyectile") || other.CompareTag("Poop"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
