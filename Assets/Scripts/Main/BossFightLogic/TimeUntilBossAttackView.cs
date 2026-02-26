using TMPro;
using UnityEngine;

public class TimeUntilBossAttackView : MonoBehaviour
{
    [SerializeField] private BossFightDataServer bossFightDataServer;

    private TMP_Text timeView;

    void Start()
    {
        timeView = gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        timeView.text = TimeToString(bossFightDataServer.autoBossAttackTimer);
    }

    /// <summary>
    /// Converts time as float to string(seconds).
    /// </summary>
    /// <param name="total">Total seconds.</param>
    /// <returns>Time as string.</returns>
    private string TimeToString(float total)
    {
        int seconds = (int)total % 60; // seconds in total
        return seconds.ToString(); // format string
    }
}
