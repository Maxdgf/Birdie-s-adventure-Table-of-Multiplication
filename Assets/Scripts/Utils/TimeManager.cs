/*
 * Description
 * ------------------------------
 * This script manages game time.
 */

using UnityEngine;

public static class TimeManager
{
    /// <summary>
    /// Puases game time.
    /// </summary>
    public static void PauseGameTime()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Plays game time.
    /// </summary>
    public static void PlayGameTime()
    {
        Time.timeScale = 1f;
    }
}