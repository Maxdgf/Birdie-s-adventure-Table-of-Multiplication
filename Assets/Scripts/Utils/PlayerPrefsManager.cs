/*
 * Description
 * -------------------------------------------
 * This script updates and reads player prefs.
 */

using UnityEngine;

public static class PlayerPrefsManager
{
    /// <summary>
    /// Writes true to bool 'IS_APP_LAUNCHED_SOME_TIME_AGO' pref.
    /// </summary>
    public static void EnableAppLaunchedState()
    {
        PlayerPrefs.SetString("IS_APP_LAUNCHED_SOME_TIME_AGO", "true");
    }

    /// <summary>
    /// Extracts bool value from pref with name 'IS_APP_LAUNCHED_SOME_TIME_AGO'
    /// </summary>
    /// <returns>Boolean state.</returns>
    public static bool GetAppLaunchedState()
    {
        string value = PlayerPrefs.GetString("IS_APP_LAUNCHED_SOME_TIME_AGO", "false"); // default value - false
        return bool.Parse(value); // convert to bool
    }

    /// <summary>
    /// Unlocks bossfight level state.
    /// </summary>
    public static void UnlockBossfight()
    {
        PlayerPrefs.SetString("IS_BOSSFIGHT_UNLOCKED", "false");
    }

    /// <summary>
    /// Extracts bool value from pref with name 'IS_BOSSFIGHT_UNLOCKED'.
    /// </summary>
    /// <returns>Boolean state.</returns>
    public static bool GetIsBossfightUnlockedState()
    {
        string value = PlayerPrefs.GetString("IS_BOSSFIGHT_UNLOCKED", "false"); // default value - false
        return bool.Parse(value); // convert to bool
    }

    /// <summary>
    /// Writes int value to player pref with specific name.
    /// </summary>
    /// <param name="prefName">Pref name.</param>
    /// <param name="value">Pref int value.</param>
    public static void WriteToIntPref(string prefName, int value)
    {
        PlayerPrefs.SetInt(prefName, value);
    }

    /// <summary>
    /// Extracts int value from player pref with specific name.
    /// </summary>
    /// <param name="prefName">Pref name.</param>
    /// <returns>Player pref int value(0 if player pref is not founded).</returns>
    public static int ExtractValueFromIntPref(string prefName)
    {
        return PlayerPrefs.GetInt(prefName, 0); // 0 default value
    }

    /// <summary>
    /// Writes float value to player pref with specific name.
    /// </summary>
    /// <param name="prefName">Pref name.</param>
    /// <param name="value">Pref float value.</param>
    public static void WriteToFloatPref(string prefName, float value)
    {
        PlayerPrefs.SetFloat(prefName, value);
    }

    /// <summary>
    /// Extracts float value from player pref with specific name.
    /// </summary>
    /// <param name="prefName"></param>
    /// <returns>Player pref float value(0f if player pref is not founded).</returns>
    public static float ExtractValueFromFloatPref(string prefName)
    {
        return PlayerPrefs.GetFloat(prefName, 0f); // 0f default value
    }

    /// <summary>
    /// Writes string value to player pref with specific name.
    /// </summary>
    /// <param name="prefName">Pref name.</param>
    /// <param name="value">Pref string value.</param>
    public static void WriteToStringPref(string prefName, string value)
    {
        PlayerPrefs.SetString(prefName, value);
    }

    /// <summary>
    /// Extracts string value from player pref with specific name.
    /// </summary>
    /// <param name="prefName">Pref name.</param>
    /// <returns>Player pref string value("" if player pref is not founded).</returns>
    public static string ExtractValueFromStringPref(string prefName)
    {
        return PlayerPrefs.GetString(prefName, ""); // "" dafault value
    }

    /// <summary>
    /// Saves all modified prefs now.
    /// </summary>
    public static void SavePrefs()
    {
        PlayerPrefs.Save(); // save modified prefs
    }
}