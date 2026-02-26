using System.IO;
using UnityEngine;

public static class LevelsRegisterUtil
{
    /// <summary>
    /// Writes data to levels register.
    /// </summary>
    /// <param name="json">Levels register json string.</param>
    public static void WriteDataToLevelRegister(string json)
    {
        string path = Path.Combine(Application.persistentDataPath, Constants.LEVELS_REGISTER_NAME);
        File.WriteAllText(path, json); // write data to json file
    }

    /// <summary>
    /// Gets deserialized levels riegister json.
    /// </summary>
    /// <returns>Deserialized levels register.</returns>
    public static Levels GetLevelRegister()
    {
        string path = Path.Combine(Application.persistentDataPath, Constants.LEVELS_REGISTER_NAME);
        string json = File.ReadAllText(path); // data from json file
        Levels levels = JsonUtility.FromJson<Levels>(json); // deserialization to Levels class

        return levels;
    }
}
