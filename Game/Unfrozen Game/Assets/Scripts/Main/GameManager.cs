using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentLevel = 0;
    public static List<levelJson> Levels;
    static GameObject canvas;
    public int Attration;
    public int Uniqueness;
    void Start()
    {
        if (instance != null) Destroy(this);
        instance = this;
        DontDestroyOnLoad(gameObject);
        canvas = GameObject.Find("Canvas");
        LoadLevels();
#if UNITY_EDITOR
        currentLevel = 1;
        NextLevel();
#endif
    }
    void LoadLevels()
    {
        Levels = new List<levelJson>();
        foreach (string f in Directory.GetFiles(Application.streamingAssetsPath))
            if (f.Contains("level_") && f.EndsWith(".json"))
                Levels.Add(JsonUtility.FromJson<levelJson>(File.ReadAllText(f)));
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
        print($"Loading Level {currentLevel}");
        if (currentLevel > Levels.Count)
            LoadLevels();

        ClearCanvas();
        if (currentLevel == 1)
            CreateGO("Prefabs/Level2");
        else
        {
            CreateGO("Prefabs/Level").name = "[L] " + Levels[currentLevel].level;
            SceneMechanics.json = Levels[currentLevel];
            print(SceneMechanics.json);
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
        currentLevel++;
        GameObject go = instance.CreateGO("Prefabs/WinGame");
    }
    public static void WonLevel2()
    {
        ClearCanvas(); //CLEAR CANVAS THEN DRAW WIN GAME
        currentLevel = 2;
        GameObject go = instance.CreateGO("Prefabs/WinGame");
    }
    public void ChangeScene(dialogJson dialog)
    {
        Attration += dialog.attarction;
        Uniqueness += dialog.uniqueness;
        if (dialog.scene >= SceneMechanics.json.scenes.Length)
            WonLevel();
        else if (Attration < 0 || Uniqueness > 9 || Uniqueness < 1)
            LostLevel(SceneMechanics.json.responses[dialog.response]);
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
