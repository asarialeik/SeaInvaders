using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ships : MonoBehaviour
{
    public Player player;

    //Ship specifics
    [SerializeField]
    public float playerSpeed;
    [SerializeField]
    public float shootCooldown;

    private void OnEnable()
    {
        player.canShoot = true;

        player.playerSpeed = playerSpeed;
        player.shootCooldown = shootCooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Poop"))
        {
            GameManager.Instance.GameOver();
            player.gameObject.SetActive(false);
        }
    }
}
