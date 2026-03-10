/*
 * Description
 * ------------------------------------------------------------
 * This script stores and updates required game, player states.
 */

using UnityEngine;

public class PlayerDataServer : MonoBehaviour
{
    [HideInInspector] public string gameState;
    [HideInInspector] public int playerScore;
    [HideInInspector] public float gameSessionTime;

    void Start()
    {
        gameState = "PLAYING";
    }

    void Update()
    {
        gameSessionTime += Time.deltaTime;
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
    public void UpdateGameEndedState()
    {
        gameState = "ENDED";
    }

    /// <summary>
    /// Updates 'is player lost' state.
    /// </summary>
    public void UpdatePlayerLostState()
    {
        gameState = "PLAYER_LOST";
    }
}
