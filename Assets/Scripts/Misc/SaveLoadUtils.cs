using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadUtils
{
    // Start is called before the first frame update
    public static bool hasSave
    {
        get { return HasCurrentSpawnPointIndex() && HasCurrentWinRoomIndex() && HasCurrentLevel(); }
    }
    public static bool HasCurrentSpawnPointIndex() => PlayerPrefs.HasKey("currentSpawnPointIndex");
    public static bool HasCurrentWinRoomIndex() => PlayerPrefs.HasKey("randomWinVar");
    public static bool HasCurrentLevel() => PlayerPrefs.HasKey("currentLevel");
    public static bool HasEasyMode() => PlayerPrefs.HasKey("easyMode");
    public static int GetCurrentSpawnPointIndex() => PlayerPrefs.GetInt("currentSpawnPointIndex");
    public static int GetCurrentWinRoomIndex() => PlayerPrefs.GetInt("randomWinVar");
    public static int GetCurrentLevel() => PlayerPrefs.GetInt("currentLevel");
    public static int GetEasyMode() => PlayerPrefs.GetInt("easyMode");

    public static void SetCurrentSpawnPointIndex(int value) => PlayerPrefs.SetInt("currentSpawnPointIndex", value);
    public static void SetCurrentWinRoomIndex(int value) => PlayerPrefs.SetInt("randomWinVar", value);
    public static void SetCurrentLevel(int value) => PlayerPrefs.SetInt("currentLevel", value);
    public static void SetEasyMode(int value) => PlayerPrefs.SetInt("easyMode", value);

    public static void ResetSave()
    {
        PlayerPrefs.DeleteAll();

    }


    public static void GenerateNewSave(int easyMode)
    {
        ResetSave();
        PlayerPrefs.SetInt("randomWinVar", Random.Range(0, 2));
        PlayerPrefs.SetInt("currentSpawnPointIndex", 0);
        PlayerPrefs.SetInt("currentLevel", 0);
        PlayerPrefs.SetInt("easyMode", easyMode);

    }
}
