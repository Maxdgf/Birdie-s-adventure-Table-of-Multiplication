using UnityEngine;
using UnityEngine.UI;

public class BossfightButtonClickLogic : MonoBehaviour
{
    private SceneFadeLoader loader;

    void Start()
    {
        loader = gameObject.AddComponent<SceneFadeLoader>(); // add scene fade loader
        Button button = gameObject.GetComponent<Button>(); // get button component
        Image buttonImage = button.GetComponent<Image>(); // get image
        Color imageColor = buttonImage.color; // get button image color

        bool isBossfightUnlocked = PlayerPrefsManager.GetIsBossfightUnlockedState();
        if (!isBossfightUnlocked)
        {
            // activate lock icon on level button
            Transform lockObject = transform.Find("lock"); // find child lock icon
            lockObject.gameObject.SetActive(true);

            button.interactable = false; // not clickable
            imageColor.a = 0.5f; // set 50% alpha to button image color
            buttonImage.color = imageColor; // set edited color
        } 
    }

    public void LaunchBossFight()
    {
        loader.FadeLoad("BossFight");
    }
}