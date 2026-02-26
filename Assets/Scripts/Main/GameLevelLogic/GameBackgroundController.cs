using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class BgNode
{
    public string type;
    public Sprite sprite;
    public bool enableSun, enableClouds;
}

public class GameBackgroundController : MonoBehaviour
{
    [SerializeField] private GameObject background, parallaksObject, sun, clouds;
    [SerializeField] private BgNode[] backgrounds;

    void Start()
    {
        string data = PlayerPrefsManager.ExtractValueFromStringPref("SELECTED_LEVEL");
        LevelData levelData = JsonUtility.FromJson<LevelData>(data);

        bool enableParallaks = levelData.backgroundType switch
        {
            "sunny" => false,
            "cloudy" => true,
            "forest" => true,
            "desert" => true,
            "mountains" => true,
            _ => false
        };

        // select current bg dataset by current level bg type
        BgNode current = Array.Find(backgrounds, node => levelData.backgroundType == node.type);

        if (!enableParallaks)
        {
            SpriteRenderer renderer = background.GetComponent<SpriteRenderer>();
            renderer.sprite = current.sprite;
            background.SetActive(true); // enable static background object
        }
        else
        {
            parallaksObject.SetActive(true); // enable parallaks background object

            // find bg frames
            SpriteRenderer root = GameObject.Find($"{parallaksObject.name}/background").GetComponent<SpriteRenderer>();
            SpriteRenderer bg1 = GameObject.Find($"{parallaksObject.name}/background/bg1").GetComponent<SpriteRenderer>();
            SpriteRenderer bg2 = GameObject.Find($"{parallaksObject.name}/background/bg2").GetComponent<SpriteRenderer>();

            // set required bg sprite
            Sprite sprite = current.sprite;

            // set frames width by bg sprite width
            float spriteX = sprite.bounds.size.x;
            root.size = new Vector2(spriteX, 1f);
            bg1.size = new Vector2(spriteX, 1f);
            bg2.size = new Vector2(spriteX, 1f);

            // adapt side bg frames positions by bg sprite width(root remains same)
            bg1.transform.localPosition = new Vector2(spriteX, 0f);
            bg2.transform.localPosition = new Vector2(-spriteX, 0f);

            // set sprite to bg frames
            root.sprite = sprite;
            bg1.sprite = sprite;
            bg2.sprite = sprite;
        }

        if (current.enableSun) sun.SetActive(true); // enable sun decoration object
        if (current.enableClouds) clouds.SetActive(true); // enable clouds spawner
    }
}