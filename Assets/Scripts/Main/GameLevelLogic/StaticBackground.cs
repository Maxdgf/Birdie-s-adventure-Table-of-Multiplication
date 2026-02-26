using UnityEngine;
using UnityEngine.UIElements;

public class StaticBackground : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;

    void Start()
    {
        string data = PlayerPrefsManager.ExtractValueFromStringPref("SELECTED_LEVEL");
        LevelData levelData = JsonUtility.FromJson<LevelData>(data);

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = levelData.backgroundType switch
        {
            "sunny" => backgrounds[0],
            "cloudy" => backgrounds[1],
            _ => backgrounds[0]
        }; // set bg by bg type
    }
}
