using UnityEngine;

public static class PlayerDataManager
{
    public const string PlayerUsername = "ad75baab";
    public const string PlayerColor = "5576b43a";

    public static void SaveData(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static string LoadData(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public static bool ValueExists(string key)
    {
        return PlayerPrefs.GetString(key).Length != 0;
    }

    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
