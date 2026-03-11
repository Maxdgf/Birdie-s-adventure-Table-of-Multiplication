using UnityEngine;

public class ResetPause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    /// <summary>
    /// Plays game time and hides pause panel.
    /// </summary>
    public void ResetGamePauseAndPanel()
    {
        TimeManager.PlayGameTime(); // play game time
        pausePanel.SetActive(false);
    }

    /// <summary>
    /// Plays game time.
    /// </summary>
    public void ResetGamePause()
    {
        TimeManager.PlayGameTime();
    }
}
