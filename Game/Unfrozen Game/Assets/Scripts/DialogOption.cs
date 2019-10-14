using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogOption : MonoBehaviour
{
    public dialogJson dialog;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { Clicked(); }); ;
    }
    public void SetDialog(dialogJson dialog)
    {
        this.dialog = dialog;
        GetComponentInChildren<Text>().text = dialog.line;
    }
    public void Clicked()
    {
        print("Choose: " + dialog.line);
        SceneMechanics.currentScene = dialog.scene;
    }
}
