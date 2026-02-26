/*
 * Description
 * ------------------------------------------------------------
 * This script stores and updates required game, player states.
 */

using UnityEngine;

public class PlayerDataServer : MonoBehaviour
{
    [HideInInspector] public bool isEnded, isPlayerLost;
    [HideInInspector] public int playerScore;
    [HideInInspector] public float gameSessionTime;

    void Start()
    {
        isEnded = false;
        isPlayerLost = false;
        gameSessionTime = Time.time;
    }

    /// <summary>
    /// Updates player score state.
    /// </summary>
    /// <param name="value"></param>
    public void UpdateScore(int value)
    {
        playerScore = value;
    }

    /// <summary>
    /// Updates 'is game ended' state. Sets to true.
    /// </summary>
    public void UpdateIsGameEndedState()
    {
        gameSessionTime += (Time.time - gameSessionTime) + gameSessionTime;
        isEnded = true;
    }

    /// <summary>
    /// Updates 'is player lost' state.
    /// </summary>
    public void UpdateIsPlayerLostState()
    {
        isPlayerLost = true;
    }
}
