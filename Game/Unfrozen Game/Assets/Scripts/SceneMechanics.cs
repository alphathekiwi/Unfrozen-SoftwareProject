using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMechanics : MonoBehaviour
{
    public levelJson json;
    public static int currentScene;
    void Start()
    {
        print("Launching " + json.level);
    }

    // Update is called once per frame
    void Update()
    {
        DialogOption[] children = GetComponentsInChildren<DialogOption>();
        for (int i = 0; i < children.Length; i++)
        {
            bool active = i < json.scenes[currentScene].dialog.Length;
            children[i].gameObject.SetActive(active);
            if (!active) continue;
            int d = json.scenes[currentScene].dialog[i];
            children[i].SetDialog(json.dialogs[d]);
        }
    }
}
