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
        // launch win scene
        if (dataServer.isEnded)
        {
            GameResult result = new GameResult
            {
                score = dataServer.playerScore,
                time = dataServer.gameSessionTime
            };

            string json = JsonUtility.ToJson(result);
            PlayerPrefsManager.WriteToStringPref("GAME_RESULT", json); // save game result

            loader.FadeLoad(win_scene);
            return;
        }

        // launch game over scene
        if (dataServer.isPlayerLost)
        {
            loader.FadeLoadAfterCustomDelay(game_over_scene, 4f);
            gameUiManager.MoveAnswersPanel(false);
        }
    }
}