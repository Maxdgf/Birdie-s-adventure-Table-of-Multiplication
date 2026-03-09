using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComicsRunner : MonoBehaviour
{
    [SerializeField] private Image[] blocks;
    [SerializeField] private float fadeTime, blockDelay;
    [SerializeField] private GameObject descriptionField;

    void Start()
    {
        float delay = blockDelay;
        foreach (Image block in blocks)
        {
            StartCoroutine(HideBlock(delay, block));
            delay += blockDelay;
        }
        StartCoroutine(ShowDescription(delay + 1f));
    }

    private IEnumerator HideBlock(float delay, Image block)
    {
        yield return new WaitForSeconds(delay);
        block.CrossFadeAlpha(0f, fadeTime, true);
    }

    private IEnumerator ShowDescription(float delay)
    {
        yield return new WaitForSeconds(delay);
        descriptionField.SetActive(true);
    }
}
