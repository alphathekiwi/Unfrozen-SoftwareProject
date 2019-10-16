using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogOption : MonoBehaviour
{
    public dialogJson dialog;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { GameManager.instance.ChangeScene(dialog); }); ;
    }
    public void SetDialog(dialogJson dialog)
    {
        this.dialog = dialog;
        GetComponentInChildren<Text>().text = dialog.line;
    }
}
