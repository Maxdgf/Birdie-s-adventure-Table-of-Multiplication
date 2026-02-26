/*
 * Description
 * ----------------------------------------------------------------
 * This script handles pressing a button for a specific game level.
 */

using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LevelData
{
    public int targetNum; // target multiplication num
    public string backgroundType; // game background type
    public string levelId; // game level id (uuid4)
}

public class LevelButtonClickLogic : MonoBehaviour
{
    private const float STAR_ON_BUTTON_SIZE = 0.25f; // star on button size

    [SerializeField] [Tooltip("Target multiplication num.")] private int multiplicationNum;
    [SerializeField] [Tooltip("Levels register object.")] private LevelsRegister allLevels;
    [SerializeField] [Tooltip("Field on which stars obtained at this level will be placed.")] private GameObject starsField;
    [SerializeField] [Tooltip("Level background type.")] private string backgroundType;

    private SceneFadeLoader loader;
    private int index;

    void Start()
    {
        index = Array.FindIndex(allLevels.levelsRegister.levelsList, levels => levels.target == multiplicationNum); // get level index by multiplication num
        loader = gameObject.AddComponent<SceneFadeLoader>(); // add scene fade loader

        Button button = gameObject.GetComponent<Button>(); // get button component
        Image buttonImage = button.GetComponent<Image>(); // get image
        Color imageColor = buttonImage.color; // get button image color

        if (!allLevels.levelsRegister.levelsList[index].isUnlocked)
        {
            // activate lock icon on level button
            Transform lockObject = transform.Find("lock"); // find child lock icon
            lockObject.gameObject.SetActive(true);

            button.interactable = false; // not clickable
            imageColor.a = 0.5f; // set 50% alpha to button image color
            buttonImage.color = imageColor; // set edited color
        } 
        else
        {
            int starsCount = allLevels.levelsRegister.levelsList[index].stars;
            for (int i = 0; i < starsCount; i++)
            {
                GameObject star = ConfigureStarObject();
                star.transform.SetParent(starsField.transform);
            }
        }
    }

    /// <summary>
    /// Configures star ui object.
    /// </summary>
    /// <returns>Star object.</returns>
    private GameObject ConfigureStarObject()
    {
        GameObject starObject = new GameObject();
        starObject.transform.localScale = new Vector2(STAR_ON_BUTTON_SIZE, STAR_ON_BUTTON_SIZE);
        Image image = starObject.AddComponent<Image>();
        Sprite star = ResourcesLoader.LoadSprite("star"); // load star sprite
        image.sprite = star;

        return starObject;
    }

    /// <summary>
    /// Selects level or sets Boss fight scene. Writes current level to 'SELECTED_LEVEL' player pref, then sets game scene.
    /// </summary>
    public void SelectLevel()
    {
        if (allLevels.levelsRegister.levelsList[index].isUnlocked) // check is level unlocked
        {
            LevelData levelData = new LevelData
            {
                targetNum = allLevels.levelsRegister.levelsList[index].target,
                backgroundType = backgroundType,
                levelId = allLevels.levelsRegister.levelsList[index].id
            };

            string serializedLevelData = JsonUtility.ToJson(levelData); // serialize to json

            PlayerPrefsManager.WriteToStringPref("SELECTED_LEVEL", serializedLevelData); // set target multiplication num
            PlayerPrefsManager.SavePrefs(); // save

            loader.FadeLoad("Game"); // load game level scene
        }
    }
}
