using UnityEngine;

public class ApplicationStart : MonoBehaviour
{
    void Start()
    {
        bool isAppLaunchedSomeTimeAgo = PlayerPrefsManager.GetAppLaunchedState();
        if (!isAppLaunchedSomeTimeAgo) PlayerPrefsManager.EnableAppLaunchedState();
    }
}