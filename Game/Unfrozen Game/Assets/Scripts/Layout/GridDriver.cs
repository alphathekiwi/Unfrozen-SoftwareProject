﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class GridDriver : MonoBehaviour
{
    public int cols = 1;
    public int rows = 1;

    void Start()
    {
        GridLayoutGroup grd = GetComponent<GridLayoutGroup>();
        RectTransform rt = GetComponent<RectTransform>();
        grd.cellSize = new Vector2((rt.rect.width / cols) - grd.spacing.x * 0.75f, (rt.rect.height / rows) - grd.spacing.y);
        grd.padding.left = (int)(grd.spacing.x / 4);
        grd.padding.top = (int)(grd.spacing.y / 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
