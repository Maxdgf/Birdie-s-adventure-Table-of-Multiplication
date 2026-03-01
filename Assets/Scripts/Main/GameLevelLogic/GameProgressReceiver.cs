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

    void Start()
    {
        loader = gameObject.AddComponent<SceneFadeLoader>();
    }

    void Update()
    {
        CheckPlayerProgress();
    }

    /// <summary>
    /// Cheks player progress.
    /// </summary>
    private void CheckPlayerProgress()
    {
        switch (dataServer.gameState)
        {
            // launch win scene
            case "ENDED":
                player.FreezePlayer(); // stop player

                gameUiManager.HidePauseButton();
                gameUiManager.MoveAnswersPanel(false);

                GameResult result = new GameResult
                {
                    score = dataServer.playerScore,
                    time = dataServer.gameSessionTime
                };

                string json = JsonUtility.ToJson(result);
                PlayerPrefsManager.WriteToStringPref("GAME_RESULT", json); // save game result

                loader.FadeLoadAfterCustomDelay(win_scene, 4f);
                break;

            // launch game over scene
            case "PLAYER_LOST":
                loader.FadeLoadAfterCustomDelay(game_over_scene, 4f);

                gameUiManager.HidePauseButton();
                gameUiManager.MoveAnswersPanel(false);
                break;

            default: break; // nothing
        }
    }
}