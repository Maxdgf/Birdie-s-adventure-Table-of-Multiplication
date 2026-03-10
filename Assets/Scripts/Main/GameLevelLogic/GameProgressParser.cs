/*
 * Description
 * ---------------------------------------------------------------------
 * This script parses players game result data and manages win scene ui.
 */

using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameProgressParser : MonoBehaviour
{
    [SerializeField] [Tooltip("Game time view.")] private TMP_Text timeView;
    [SerializeField] [Tooltip("Level stars view ui panel.")] private GameObject starsSetterPanel;

    void Start()
    {
        ManageGameResultData();
    }

    /// <summary>
    /// Calculates star count by player score.
    /// </summary>
    /// <param name="score">Player int score.</param>
    /// <returns>Stars count.</returns>
    private int CalculateStarsCount(int score)
    {
        if (score == Constants.EXAMPLES_COUNT) return 3; // full
        else if (score < Constants.EXAMPLES_COUNT && score >= Constants.EXAMPLES_COUNT / 2) return 2; // between full and half
        else if (score < Constants.EXAMPLES_COUNT / 2 && score > 1) return 1; // less half
        else return 0; // nothing
    }

    /// <summary>
    /// Converts time as float to string(minutes.seconds).
    /// </summary>
    /// <param name="total">Total seconds.</param>
    /// <returns>Time as string.</returns>
    private string TimeToString(float total)
    {
        int minutes = (int)total / 60; // minutes in total
        int seconds = (int)total % 60; // seconds in total

        return string.Format("{0:00} : {1:00}", minutes, seconds); // format string
    }

    private void ManageGameResultData()
    {
        StarsSetter starsSetter = starsSetterPanel.GetComponent<StarsSetter>();

        string data = PlayerPrefsManager.ExtractValueFromStringPref("SELECTED_LEVEL"); // get selected level data ---|
        LevelData levelData = JsonUtility.FromJson<LevelData>(data); // deserialization to class --------------------|

        string gameResultData = PlayerPrefsManager.ExtractValueFromStringPref("GAME_RESULT"); // get game result json data ---|
        GameResult result = JsonUtility.FromJson<GameResult>(gameResultData); // deserialization to class --------------------|

        int starsCount = CalculateStarsCount(result.score); // calculate earned stars count
        timeView.text = TimeToString(result.time); // set game elapsed time
        starsSetter.SetStars(starsCount); // set stars

        Levels levels = LevelsRegisterUtil.GetLevelRegister(); // deserialization to class
        int levelNum = Array.FindIndex(levels.levelsList, levels => levels.id == levelData.levelId); // get level index by current level id
        if (levelNum < levels.levelsList.Length - 1) levels.levelsList[levelNum + 1].isUnlocked = true; // unlock next normal level
        if (starsCount > levels.levelsList[levelNum].stars) levels.levelsList[levelNum].stars = starsCount; // set stars count to current level

        int allStars = levels.levelsList.Select(level => level.stars).Sum(); // all earned stars
        if (allStars == Constants.STARS_REQUIRED) PlayerPrefsManager.UnlockBossfight(); // unlock bossfight if 30 stars collected

        string json = JsonUtility.ToJson(levels); // serialization levels register class to json ---|
        LevelsRegisterUtil.WriteDataToLevelRegister(json); // write new data to json file ----------|
    }
}