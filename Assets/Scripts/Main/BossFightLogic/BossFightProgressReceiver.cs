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
        if (bossFightDataServer.bossHealth == 0)
            loader.FadeLoadAfterCustomDelay("BossFightWin", 3f);

        if (bossFightDataServer.playerHealth == 0)
            loader.FadeLoadAfterCustomDelay("BossfightOver", 3f);
    }
}
