using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Level2 : MonoBehaviour
{
    string path;
    string jsonString;
    
    // Start is called before the first frame update
    void Start()
    {
        path = Application.streamingAssetsPath + "/level2.json";
        jsonString = File.ReadAllText(path);
        
    }  
}
