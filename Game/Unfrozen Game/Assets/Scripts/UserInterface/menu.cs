using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    private Animator menu_anim;
    // Start is called before the first frame update
    void Start()
    {
        menu_anim = gameObject.GetComponentInChildren<Animator>();
        menu_anim.gameObject.SetActive(false);
        StartCoroutine(ExecuteAfterTime(9));
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        menu_anim.gameObject.SetActive(true);
        menu_anim.Play("menu_animation");
    }
    public void ButtonPlay()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
