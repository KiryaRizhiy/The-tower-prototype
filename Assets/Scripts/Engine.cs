using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Engine
{
    private static List<LevelData> GameProgressData;

    public static List<string> LevelNames
    {
        get
        {
            List<string> result = new List<string>();
            foreach (LevelData ld in GameProgressData)
            {
                result.Add(ld.name);
            }
            return result;
        }
    }
    public static void Initialize()
    {
        for (int i = 1; i <= Settings.levelsCount; i++)
        {
            GameProgressData.Add(new LevelData("Level" + i.ToString()));
        }
    }

    private class LevelData
    {
        public LevelData(string lvlName)
        {
            name = lvlName;
            passed = false;
        }
        public string name { get; private set; }
        public bool passed;
    }
}
