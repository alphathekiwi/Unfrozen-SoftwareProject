using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class introvideo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
        GetComponent<VideoPlayer>().Play();
    }
}
