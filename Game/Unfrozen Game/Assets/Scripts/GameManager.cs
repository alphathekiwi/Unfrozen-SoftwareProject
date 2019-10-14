using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public List<levelJson> Levels;
    void Start()
    {
        if (instance != null) Destroy(this);
        instance = this;
        Levels = new List<levelJson>();

        print(Application.streamingAssetsPath);
        foreach (string f in Directory.GetFiles(Application.streamingAssetsPath))
        {
            if (f.Contains("level_") && f.EndsWith(".json"))
            {
                string l = File.ReadAllText(f);
                Levels.Add(JsonUtility.FromJson<levelJson>(l));
                print(l);
            }
        }
    }
}
