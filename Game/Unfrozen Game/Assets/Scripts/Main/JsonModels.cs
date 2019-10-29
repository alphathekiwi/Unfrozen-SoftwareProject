using UnityEngine;
using System;       //Add this line
using System.Collections.Generic;

[Serializable]
public class levelJson
{
    public string level;
    public sceneJson[] scenes;
    public dialogJson[] dialogs;
    public string[] responses;
    public override string ToString() => JsonUtility.ToJson(this);
}
[Serializable]
public class sceneJson
{
    public string name;
    public int[] dialog;
    public override string ToString() => JsonUtility.ToJson(this);
}
[Serializable]
public class dialogJson
{
    public string line;
    public int attarction;
    public int uniqueness;
    public int scene;
    public int response;
    public override string ToString() => JsonUtility.ToJson(this);
}
