using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentLevel;
    public List<levelJson> Levels;
    static GameObject canvas;
    public int Attration;
    public int Uniqueness;
    void Start()
    {
        if (instance != null) Destroy(instance);
        instance = this;
        DontDestroyOnLoad(gameObject);
        canvas = GameObject.Find("Canvas");
        LoadLevels();
        //ShowMenu();
        LaunchLevel(0);
    }
    void LoadLevels()
    {
        Levels = new List<levelJson>();
        foreach (string f in Directory.GetFiles(Application.streamingAssetsPath))
            if (f.Contains("level_") && f.EndsWith(".json"))
            {
                string l = File.ReadAllText(f);
                Levels.Add(JsonUtility.FromJson<levelJson>(l));
            }
    }
    public static void ShowMenu()
    {
        ClearCanvas();
        instance.Attration = 0;
        instance.Uniqueness = 5;
        instance.CreateGO("Prefabs/Level");
        currentLevel = 0;
        SceneMechanics.json = instance.Levels[currentLevel];
    }
    public void NextLevel()
    {
        print("Clicked ext level");
        ClearCanvas();
        CreateGO("Prefabs/Level");
        SceneMechanics.json = Levels[currentLevel];
    }
    internal void LaunchLevel(int level)
    {
        ClearCanvas();
        instance.Attration = 0;
        instance.Uniqueness = 5;
        CreateGO("Prefabs/Level");
        currentLevel = level;
        SceneMechanics.json = Levels[level];
    }
    public void ChangeScene(dialogJson dialog)
    {
        Attration += dialog.attarction;
        Uniqueness += dialog.uniqueness;
        if (Attration < 0 || Uniqueness > 9 || Uniqueness < 1)
        {
            ClearCanvas(); //CLEAR CANVAS THEN DRAW GAME OVER
            GameObject go = CreateGO("Prefabs/GameOver");
            go.GetComponentInChildren<Text>().text = SceneMechanics.json.responses[dialog.response];
            go.GetComponentInChildren<Button>().onClick.AddListener(GameManager.ShowMenu); ;
        }
        else if (dialog.scene >= SceneMechanics.json.scenes.Length)
        {
            ClearCanvas(); //CLEAR CANVAS THEN DRAW WIN GAME
            GameObject go = CreateGO("Prefabs/WinGame");
            currentLevel = 2;
        }
        else
        {
            SceneMechanics.instance.currentScene = dialog.scene;
            SceneMechanics.instance.SetResponse(dialog.response);
        }
    }
    // HELPER METHODS
    GameObject CreateGO(string name)
    {
        Rect ct = canvas.GetComponent<RectTransform>().rect;
        return Instantiate(Resources.Load<GameObject>(name), new Vector3(ct.width / 2, ct.height / 2, 0), Quaternion.identity, canvas.transform);
    }
    static void ClearCanvas()
    {
        foreach (Transform child in canvas.transform)
            Destroy(child.gameObject);
    }
}
