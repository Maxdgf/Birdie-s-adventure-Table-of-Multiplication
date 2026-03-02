using UnityEngine;

public class BossFightProgressReceiver : MonoBehaviour
{
    [SerializeField] [Tooltip("Boss fight data server")] private BossFightDataServer bossFightDataServer;

    private SceneFadeLoader loader;
    private bool isActionCompleted;

    private void Start()
    {
        loader = gameObject.AddComponent<SceneFadeLoader>(); // get scene fade loader
    }

    void Update()
    {
        /*
         * --------------------------------------------------------------------
         * | `isActionCompleted` state prevents multiple executions of        |
         * | game progress recording or other actions in Update while a scene |
         * | change coroutine is running.                                     |
         * --------------------------------------------------------------------
         */
        if (!isActionCompleted)
            switch (bossFightDataServer.gameState)
            {
                case "PLAYER_WIN": // launch win scene
                    loader.FadeLoadAfterCustomDelay("BossFightWin", 3f);
                    isActionCompleted = true;
                    break;

                case "BOSS_WIN": // launch game over scene
                    loader.FadeLoadAfterCustomDelay("BossFightOver", 3f);
                    isActionCompleted = true;
                    break;

                default: break; // nothing
            }
    }
}
