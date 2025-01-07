using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableShip : MonoBehaviour
{
    public GameObject playerShips;
    public GameObject associatedShip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Proyectile"))
        {
            foreach (Transform child in playerShips.transform)
            {
                if (child.gameObject != associatedShip)
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
