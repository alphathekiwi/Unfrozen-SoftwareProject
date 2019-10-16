﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMechanics : MonoBehaviour
{
    public static SceneMechanics instance;
    public static levelJson json;
    public int currentScene;
    public int currentResponse;

    void Start()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
        currentScene = 0;
        currentResponse = -1;
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
        if (currentResponse >= 0)
            SetResponse(json.responses[currentResponse]);
        else
            SetResponse("");

    }
    public void SetResponse(string response)
    {
        foreach (Transform child in transform)
        {
            Text t = child.GetComponent<Text>();
            if (t != null) t.text = response;
        }
    }
}
