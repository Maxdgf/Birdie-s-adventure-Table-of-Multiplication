using UnityEngine;

public class MakePause : MonoBehaviour
{
    [SerializeField] [Tooltip("Game pause panel.")] private GameObject pausePanel;

    /// <summary>
    /// Pauses game time and shows pause panel.
    /// </summary>
    public void MakeGamePause()
    {
        TimeManager.PauseGameTime(); // pause game time
        pausePanel.SetActive(true); // set active pause panel
    }
}
