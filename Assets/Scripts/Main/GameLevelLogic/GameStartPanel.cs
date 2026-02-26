/*
 * Description
 * -------------------------------------
 * This script manages start game panel.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartPanel : MonoBehaviour
{
    [SerializeField] private float fadeTime;

    void Start()
    {
        TimeManager.PauseGameTime(); // pause game time

        Image image = gameObject.GetComponent<Image>();
        Button button = gameObject.GetComponent<Button>();

        // get start panel child object - description
        Transform descriptionTransform = transform.Find("Description");
        GameObject description = descriptionTransform.gameObject;

        button.onClick.AddListener(delegate {
            description.SetActive(false); // hide description
            image.CrossFadeAlpha(0f, fadeTime, true);
            StartCoroutine(DestroyPanel(fadeTime));
            TimeManager.PlayGameTime();
        });
    }

    /// <summary>
    /// Destroys game start panel after delay.
    /// </summary>
    /// <param name="delay">Destroy delay.</param>
    private IEnumerator DestroyPanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}