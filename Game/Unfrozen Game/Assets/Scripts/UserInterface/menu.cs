using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    private GameObject menu_buttons;
    // Start is called before the first frame update
    void Start()
    {
        menu_buttons = gameObject.GetComponentInChildren<RectCenter>().gameObject;
        menu_buttons.SetActive(false);
        StartCoroutine(ExecuteAfterTime(9));
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        menu_buttons.SetActive(true);
        menu_buttons.GetComponent<RectCenter>().UpdatePosition();
    }
    public void ButtonPlay()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
