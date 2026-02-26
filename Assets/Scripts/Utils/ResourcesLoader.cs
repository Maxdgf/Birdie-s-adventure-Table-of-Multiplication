using UnityEngine;

public static class ResourcesLoader
{
    /// <summary>
    /// Loads sparite by name.
    /// </summary>
    /// <param name="name">Sprite name.</param>
    /// <returns>Loaded sprite.</returns>
    public static Sprite LoadSprite(string name)
    {
        string path = "Sprites/" + name; // path to sprite
        Debug.Log(path);
        return Resources.Load<Sprite>(path);
    }
}
