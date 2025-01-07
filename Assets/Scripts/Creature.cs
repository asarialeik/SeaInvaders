using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Poop") && !other.CompareTag("Limit") && !other.CompareTag("Bunkers"))
        {
            this.gameObject.SetActive(false);
            CreaturesManager.Instance.KilledCreature();
        }
        else if (other.CompareTag("Limit"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
