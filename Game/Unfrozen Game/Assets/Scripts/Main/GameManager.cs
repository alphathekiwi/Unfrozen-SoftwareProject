using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentLevel = 0;
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
        GameObject menue = instance.CreateGO("Prefabs/Menue");
        Destroy(menue.GetComponent<menu>());
    }
    public static void nextLevel() => instance.NextLevel();
    public void NextLevel()
    {
        print("Clicked next level");
        ClearCanvas();
        if (currentLevel == 1)
            CreateGO("Prefabs/Level2");
        else
        {
            CreateGO("Prefabs/Level").name = "[L] " + Levels[currentLevel].level;
            SceneMechanics.json = Levels[currentLevel];
        }
    }
    internal void RestartLevel(int level)
    {
        ClearCanvas();
        instance.Attration = 0;
        instance.Uniqueness = 5;
        CreateGO("Prefabs/Level").name = "[L] " + Levels[level].level;
        currentLevel = level;
        SceneMechanics.json = Levels[level];
    }
    public static void LostLevel(string explanation = "")
    {
        ClearCanvas(); //CLEAR CANVAS THEN DRAW GAME OVER
        GameObject go = instance.CreateGO("Prefabs/GameOver");
        go.GetComponentInChildren<Text>().text = explanation;
        go.GetComponentInChildren<Button>().onClick.AddListener(GameManager.ShowMenu); ;
    }
    public static void WonLevel()
    {
        ClearCanvas(); //CLEAR CANVAS THEN DRAW WIN GAME
        GameObject go = instance.CreateGO("Prefabs/WinGame");
        currentLevel++;
    }
    public void ChangeScene(dialogJson dialog)
    {
        Attration += dialog.attarction;
        Uniqueness += dialog.uniqueness;
        if (Attration < 0 || Uniqueness > 9 || Uniqueness < 1)
            LostLevel(SceneMechanics.json.responses[dialog.response]);
        else if (dialog.scene >= SceneMechanics.json.scenes.Length)
            WonLevel();
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
