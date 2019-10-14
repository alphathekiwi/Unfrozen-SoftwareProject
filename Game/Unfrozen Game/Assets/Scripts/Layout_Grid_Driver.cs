using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Layout_Grid_Driver : MonoBehaviour
{
    public int cols = 1;
    public int rows = 1;

    void Start()
    {
        GridLayoutGroup grd = GetComponent<GridLayoutGroup>();
        RectTransform rt = GetComponent<RectTransform>();
        grd.cellSize = new Vector2((rt.rect.width / cols) - grd.spacing.x, (rt.rect.height / rows) - grd.spacing.y);
        grd.padding.left = (int)grd.spacing.x / 2;
        grd.padding.top = (int)grd.spacing.y / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
