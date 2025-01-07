using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkersPowerup : MonoBehaviour
{
    public GameObject bunkers;
    public GameObject image;

    private void Awake()
    {
        LeanTween.scale(image.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f).setOnComplete(() =>
        {
            image.SetActive(true);
            LeanTween.scale(image.GetComponent<RectTransform>(), new Vector3(10, 10, 10), 0.5f).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
            {
                image.SetActive(false);
            });
        });
        
        foreach (Transform child in bunkers.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }
}
