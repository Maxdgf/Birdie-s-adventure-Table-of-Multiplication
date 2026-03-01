using UnityEngine;

public class ResetPause : MonoBehaviour
{
    /// <summary>
    /// Plays game time and hides pause panel.
    /// </summary>
    public void ResetGamePause()
    {
        TimeManager.PlayGameTime(); // play game time
    }
}
