using UnityEngine;

public class BossFightProgressReceiver : MonoBehaviour
{
    [SerializeField] private BossFightDataServer bossFightDataServer;

    private SceneFadeLoader loader;

    private void Start()
    {
        loader = gameObject.AddComponent<SceneFadeLoader>();
    }

    void Update()
    {
        switch (bossFightDataServer.gameState)
        {
            case "PLAYER_WIN": // launch win scene
                loader.FadeLoadAfterCustomDelay("BossFightWin", 3f);
                break;

            case "BOSS_WIN": // launch game over scene
                loader.FadeLoadAfterCustomDelay("BossFightOver", 3f);
                break;

            default: break; // nothing
        }
    }
}
