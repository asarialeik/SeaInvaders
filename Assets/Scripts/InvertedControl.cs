using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedControl : MonoBehaviour
{
    public Player player;
    private float powerupCooldown = 5f;
    private float originalPlayerSpeed;
    public GameObject image;

    private void Awake()
    {
        player.playerSpeed = -player.playerSpeed;
        StartCoroutine(ResetPowerup());
        LeanTween.scale(image.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f).setOnComplete(() =>
        {
            image.SetActive(true);
            LeanTween.scale(image.GetComponent<RectTransform>(), new Vector3(10, 10, 10), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
            {
                image.SetActive(false);
            });
        });
    }

    public IEnumerator ResetPowerup()
    {
        yield return new WaitForSeconds(powerupCooldown);
        player.playerSpeed = - player.playerSpeed;
        this.gameObject.SetActive(false);
    }
}
