using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum orientation
{
    HorizontalLeft,
    HorizontalRight,
    VerticalTop,
    VerticalBot
}
[RequireComponent(typeof(RectTransform))]
public class Layout_Position : MonoBehaviour
{
    public orientation orientation;
    public int padding;
    void Update()
    {
        RectTransform rt = GetComponent<RectTransform>();
        RectTransform pt = transform.parent.GetComponent<RectTransform>();
        switch (orientation)
        {
            case orientation.HorizontalLeft:
                GetComponent<RectTransform>().localPosition = new Vector2(rt.rect.width / 2 + padding - pt.rect.width / 2, rt.localPosition.y);
                break;
            case orientation.HorizontalRight:
                GetComponent<RectTransform>().localPosition = new Vector2(pt.rect.width / 2 - rt.rect.width / 2 - padding, rt.localPosition.y);
                break;
            case orientation.VerticalTop:
                GetComponent<RectTransform>().localPosition = new Vector2(rt.localPosition.x, pt.rect.height / 2 - rt.rect.height / 2 - padding);
                break;
            case orientation.VerticalBot:
                GetComponent<RectTransform>().localPosition = new Vector2(rt.localPosition.x, rt.rect.height / 2 + padding - pt.rect.height / 2);
                break;
        }
    }
}
