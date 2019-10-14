using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    static GameObject canvas;
    public List<levelJson> Levels;
    void Start()
    {
        if (instance != null) Destroy(this);
        instance = this;
        canvas = GameObject.Find("Canvas");
        LoadLevels();
        LaunchLevel(Levels[0]);
    }
    void LaunchLevel(levelJson level)
    {
        foreach (Transform child in canvas.transform)
            child.gameObject.SetActive(false);
        RectTransform ct = canvas.GetComponent<RectTransform>();
        Vector3 pos = new Vector3(ct.rect.width / 2, ct.rect.height / 2, 0);
        GameObject l = Instantiate(Resources.Load<GameObject>("Prefabs/Level"), pos, Quaternion.identity, canvas.transform);
        l.GetComponent<SceneMechanics>().json = level;
    }

    void LoadLevels()
    {
        Levels = new List<levelJson>();
        foreach (string f in Directory.GetFiles(Application.streamingAssetsPath))
        {
            if (f.Contains("level_") && f.EndsWith(".json"))
            {
                string l = File.ReadAllText(f);
                Levels.Add(JsonUtility.FromJson<levelJson>(l));
            }
        }
    }
}
