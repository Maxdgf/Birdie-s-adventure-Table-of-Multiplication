using UnityEngine;
using UnityEngine.UI;

public class ProgressBarColorState : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private string whoIs;
    [SerializeField] private BossFightDataServer bossFightDataServer;

    private Slider bar;
    private float max;

    void Start()
    {
        bar = gameObject.GetComponent<Slider>();
        max = bar.maxValue;
    }

    void Update()
    {
        bar.value = whoIs switch {
           "player" => bossFightDataServer.playerHealth,
           "boss" => bossFightDataServer.bossHealth,
           _ => bossFightDataServer.playerHealth
        };

        float value = bar.value;
        if (value <= max && value >= max / 2) barImage.color = Color.green; // full health level
        else if (value < max / 2 && value >= max / 4) barImage.color = Color.yellow; // low health level
        else barImage.color = Color.red; // critical health level
    }
}
