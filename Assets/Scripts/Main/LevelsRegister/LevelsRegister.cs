/*
 * Description
 * ------------------------------------------------------------------------------------------
 * This script creates game levels json register(if he not exists) and extracts data from it.
 */

using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Level
{
    public int target; // target multiplication num
    public bool isUnlocked; // unlocked state
    public int stars; // earned starts count
    public string id; // uniquie level id (uuid4)
}

[Serializable]
public class Levels
{
    public Level[] levelsList;
}

public class LevelsRegister : MonoBehaviour
{
    [SerializeField] private GameObject levelButtonsPanel, allStarsCountView;
    [HideInInspector] public Levels levelsRegister;

    void Start()
    {
        if (!CheckIsLevelRegisterExists())
        {
            string jsonLevelsRegister = CreateLevelsRegister(); // create levels register
            LevelsRegisterUtil.WriteDataToLevelRegister(jsonLevelsRegister);
        }

        levelsRegister = LevelsRegisterUtil.GetLevelRegister();
        levelButtonsPanel.SetActive(true); // set active buttons panel to true, after register loading
        allStarsCountView.SetActive(true); // set active all stars count view to true
    }

    /// <summary>
    /// Creates levels register.
    /// </summary>
    /// <returns>Levels register json string.</returns>
    private string CreateLevelsRegister()
    {
        Level[] levelsList = new Level[Constants.LEVELS_COUNT];

        for (int i = 0; i < Constants.LEVELS_COUNT; i++)
        {
            Guid uuid = Guid.NewGuid(); // generate uuid 4

            levelsList[i] = new Level
            {
                target = i + 1, // target number
                isUnlocked = i == 0 ? true : false, // set isUnlocked state = true, to first level, other levels locked
                stars = 0, // stars count(max 3)
                id = uuid.ToString() // uniqiue level id
            };
        }

        Levels levels = new Levels
        {
           levelsList = levelsList
        };
        string json = JsonUtility.ToJson(levels); // serialize to json data

        return json;
    }

    /// <summary>
    /// Checks is level register json file exists.
    /// </summary>
    /// <returns>Boolean state.</returns>
    private bool CheckIsLevelRegisterExists()
    {
        string path = Path.Combine(Application.persistentDataPath, Constants.LEVELS_REGISTER_NAME);
        if (File.Exists(path)) return true;
        else return false;
    }
}
