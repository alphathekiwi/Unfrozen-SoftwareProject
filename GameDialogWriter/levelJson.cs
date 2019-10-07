[Serializable]
public class levelJson
{
    public string level;
    public sceneJson[] scenes;
    public dialogJson[] dialogs;
    public string[] responses;
}
[Serializable]
public class sceneJson
{
    public string name;
    public int[] dialog;
}
[Serializable]
public class dialogJson
{
    public string line;
    public int attarction;
    public int uniqueness;
    public int scene;
    public int response;
}
