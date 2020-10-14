using UnityEngine;

public static class PlayerDataManager
{
    // Strings to use as keys when saving data
    public const string PlayerUsername = "ad75baab";
    public const string PlayerColor = "5576b43a";

    /// <summary>
    /// Saves the provided data into PlayerPrefs.
    /// </summary>
    /// <param name="key">Key of the data to save</param>
    /// <param name="value">Value of the data to save</param>
    public static void SaveData(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }
    
    /// <summary>
    /// Load data by a key provided.
    /// </summary>
    /// <param name="key">Key which to look up</param>
    /// <returns>Data matching the key</returns>
    public static string LoadData(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    /// <summary>
    /// Checks if a value with a provided key exists.
    /// </summary>
    /// <param name="key">Key which to check</param>
    /// <returns>True if value exists and false if it does not</returns>
    public static bool ValueExists(string key)
    {
        return PlayerPrefs.GetString(key).Length != 0;
    }

    /// <summary>
    /// Clears all data from the PlayerPrefs.
    /// </summary>
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
