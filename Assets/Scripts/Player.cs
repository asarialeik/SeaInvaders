using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //For fire config
    public GameObject proyectile;
    public float shootCooldown;
    public bool canShoot = true;

    //For movement
    public float playerSpeed;
    private float bounds = 4.3f;
    private bool goingRight = false;
    private bool boolValue = false;

    void Start()
    {
        this.gameObject.SetActive(false);
        boolValue = goingRight;
    }

    void Update()
    {
        float movementInput = Input.GetAxis("Horizontal");
        Vector2 playerPosition = transform.position;
        if (movementInput > 0)
        {
            goingRight = true;
        }
        else if (movementInput < 0)
        {
            goingRight= false;
        }
        playerPosition.x = Mathf.Clamp(playerPosition.x + movementInput * playerSpeed * Time.deltaTime, -bounds, bounds);
        transform.position = playerPosition;

        if (goingRight != boolValue)
        {
            OnBoolChanged();
            boolValue = goingRight;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (canShoot == true)
            {
                canShoot = false;
                Fire();
                StartCoroutine(ResetShootCooldown());
            }
        }
    }

    public void Fire()
    {
        Instantiate(this.proyectile, this.transform.position, Quaternion.identity);
    }

    public IEnumerator ResetShootCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    void OnBoolChanged()
    {
        if (goingRight == true)
        {
            foreach (Transform child in transform)
            {
                child.rotation = Quaternion.Euler(child.rotation.x, 180, child.rotation.z);
            }
        }
        else if (goingRight == false)
        {
            foreach (Transform child in transform)
            {
                child.rotation = Quaternion.Euler(child.rotation.x, 0, child.rotation.z);
            }
        }
    }
}
