using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private static GameData instance;

    public static GameData LoadFromJSONResource()
    {
        if (instance == null)
        {
            TextAsset json = Resources.Load("LevelData", typeof(TextAsset)) as TextAsset;
            instance = JsonUtility.FromJson<GameData>(json.text);
        }
        return instance;
    }

    public LevelData[] levelData;
}
