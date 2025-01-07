using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpeed : MonoBehaviour
{
    public Player player;
    private float powerupCooldown = 5f;
    public GameObject image;

    private void Awake()
    {
        player.shootCooldown = player.shootCooldown / 2f;
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
        player.shootCooldown = player.shootCooldown * 2f;
        this.gameObject.SetActive(false);
    }
}
