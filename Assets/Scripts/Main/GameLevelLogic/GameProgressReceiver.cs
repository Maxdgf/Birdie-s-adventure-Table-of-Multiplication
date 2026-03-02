/*
 * Description
 * --------------------------------------------------------------------------
 * This script recieves players progress, writes this and launches end scene.
 */

using System;
using UnityEngine;

[Serializable]
public class GameResult
{
    public int score;
    public float time;
}

public class GameProgressReceiver : MonoBehaviour
{
    [SerializeField] [Tooltip("Player data server.")] private PlayerDataServer dataServer;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameUiManager gameUiManager;
    [SerializeField] [Tooltip("Win, game over scene name.")] private string win_scene, game_over_scene;

    private SceneFadeLoader loader;
    private bool isActionCompleted;

    void Start()
    {
        loader = gameObject.AddComponent<SceneFadeLoader>(); // scene fade loader
    }

    void Update()
    {
        CheckPlayerProgress(); // check player game progress
    }

    /// <summary>
    /// Cheks player progress.
    /// </summary>
    private void CheckPlayerProgress()
    {
        /*
         * --------------------------------------------------------------------
         * | `isActionCompleted` state prevents multiple executions of        |
         * | game progress recording or other actions in Update while a scene |
         * | change coroutine is running.                                     |
         * --------------------------------------------------------------------
         */
        switch (dataServer.gameState)
        {
            // launch win scene
            case "ENDED":
                if (!isActionCompleted)
                {
                    player.FreezePlayer(); // stop player

                    // configure game progress data
                    GameResult result = new GameResult
                    {
                        score = dataServer.playerScore,
                        time = dataServer.gameSessionTime
                    };

                    string json = JsonUtility.ToJson(result);
                    PlayerPrefsManager.WriteToStringPref("GAME_RESULT", json); // save game result

                    loader.FadeLoadAfterCustomDelay(win_scene, 3f);
                    isActionCompleted = true;
                }

                // hide ui
                gameUiManager.HidePauseButton();
                gameUiManager.MoveAnswersPanel(false);

                break;

            // launch game over scene
            case "PLAYER_LOST":
                if (!isActionCompleted)
                {
                    loader.FadeLoadAfterCustomDelay(game_over_scene, 4f);
                    isActionCompleted = true;
                }

                // hide ui
                gameUiManager.HidePauseButton();
                gameUiManager.MoveAnswersPanel(false);

                break;

            default: break; // nothing
        }

    }
}