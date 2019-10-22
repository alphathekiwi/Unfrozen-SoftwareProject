using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharmDisplay : MonoBehaviour
{
    public GameObject CharmOverlay;
    public GameObject UniqueOverlay;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CharmOverlay.GetComponent<Image>().fillAmount = 1 - GameManager.instance.Attration * 0.1f;
        UniqueOverlay.GetComponent<Image>().fillAmount = GameManager.instance.Uniqueness * 0.1f;
    }
}
