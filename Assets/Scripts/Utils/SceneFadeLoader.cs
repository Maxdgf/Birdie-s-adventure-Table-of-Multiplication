/*
 * Description
 * ------------------------------------------------------------
 * This script load scene by name with colored fade transition.
 * 
 * + Additional
 * + ----------------------------------------------------------
 * + This script based on `Simple Scene Fade Load System` asset
 * + from Unity Assets store. 
 * + -> https://assetstore.unity.com/packages/tools/particles-effects/simple-fade-scene-transition-system-81753
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFadeLoader : MonoBehaviour
{
    private const float MULTIPLIER = 2f; // multiplier
    private const float DELAY = 0.3f; // delay
    private const string FADE_COLOR = "#07FF00"; // fade color

    private Color color;

    void Start()
    {
        color = ParseColor(FADE_COLOR); // parse hex color
    }

    /// <summary>
    /// Sets scene with specific name, with colored fade transition(color - #07FF00).
    /// </summary>
    /// <param name="scene_name">Scene name.</param>
    public void FadeLoad(string scene_name)
    {
        Initiate.Fade(scene_name, color, MULTIPLIER);
    }

    /// <summary>
    /// Sets scene with specific name, with inverted or not inverted colored transition(white or black color).
    /// </summary>
    /// <param name="sceneName">Scene name.</param>
    /// <param name="invert">Invert transition color.</param>
    public void DefaultFadeLoad(string sceneName, bool invert)
    {
        Color defaultColor;
        if (invert) defaultColor = Color.white;
        else defaultColor = Color.black;

        Initiate.Fade(sceneName, defaultColor, MULTIPLIER);
    }

    /// <summary>
    /// Sets scene with specific name after delay, with colored fade transition(color - #07FF00).
    /// </summary>
    /// <param name="sceneName">Scene name.</param>
    public void FadeLoadAfterLittleDelay(string sceneName)
    {
        StartCoroutine(LoadSceneAfterDelay(sceneName, DELAY));
    }

    /// <summary>
    /// Sets scene with specific name after delay, with colored fade transition(color - #07FF00).
    /// </summary>
    /// <param name="sceneName">Scene name.</param>
    /// <param name="delay">Scene load delay.</param>
    public void FadeLoadAfterCustomDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneAfterDelay(sceneName, delay));
    }

    public void LoadWithoutFade(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Sets scene after delay
    /// </summary>
    /// <param name="sceneName">Scene name.</param>
    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        Initiate.Fade(sceneName, color, MULTIPLIER);
    }

    /// <summary>
    /// Parses hex string color.
    /// </summary>
    /// <param name="colorHex"></param>
    /// <returns>Parsed color.</returns>
    private Color ParseColor(string colorHex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(FADE_COLOR, out color)) return color; // parsed color
        else return Color.green; // default color
    }
}