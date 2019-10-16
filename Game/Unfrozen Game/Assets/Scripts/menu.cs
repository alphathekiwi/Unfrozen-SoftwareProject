using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    private Animator menu_anim;
    // Start is called before the first frame update
    void Start()
    {
        menu_anim = gameObject.GetComponent<Animator>();
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(ExecuteAfterTime(9, gameObject));
    }


    IEnumerator ExecuteAfterTime(float time, GameObject MyObject)
    {
        yield return new WaitForSeconds(time);
        //MyObject.SetActive(true);
        //MyObject.transform.localScale = new Vector3(12, 12, 12);
        menu_anim.Play("menu_animation");


    }
}
