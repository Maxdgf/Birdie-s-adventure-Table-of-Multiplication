using UnityEngine;

public class PlayButtonLogic : MonoBehaviour
{
    private bool isStartCutSceneShowed;
    private SceneFadeLoader loader;

    void Start()
    {
        loader = gameObject.GetComponent<SceneFadeLoader>();
        isStartCutSceneShowed = bool.Parse(PlayerPrefsManager.ExtractValueFromStringPref("IS_START_CUTSCENE_SHOWED", "false"));
    }

    public void PlayClick()
    {
        if (isStartCutSceneShowed) loader.FadeLoadAfterLittleDelay("ProgressMap");
        else
        {
            loader.FadeLoadAfterLittleDelay("StartCutScene");
            PlayerPrefsManager.WriteToStringPref("IS_START_CUTSCENE_SHOWED", "true");
        }
    }
}
