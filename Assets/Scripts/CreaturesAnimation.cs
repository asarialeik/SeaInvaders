using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesAnimation : MonoBehaviour
{
    [SerializeField]
    private float animationDuration = 0.5f;
    private MeshRenderer[] spriteRenderers;
    private int activeIndex = 0;

    void OnEnable()
    {
        spriteRenderers = GetComponentsInChildren<MeshRenderer>();

        if (spriteRenderers.Length < 2)
        {
            print("The creature needs at least 2 sprites.");
        }

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].enabled = (i == activeIndex);
        }

        StartCoroutine(SwitchSprites());
    }

    private IEnumerator SwitchSprites()
    {
        while (true)
        {
            yield return new WaitForSeconds(animationDuration);
            spriteRenderers[activeIndex].enabled = false;
            activeIndex = (activeIndex + 1) % spriteRenderers.Length;
            spriteRenderers[activeIndex].enabled = true;
        }
    }
}