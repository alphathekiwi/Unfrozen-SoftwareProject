using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Horizontal
{
    None, Left, Right
}
public enum Vertical
{
    None, Top, Bottom
}
[RequireComponent(typeof(RectTransform))]
public class RectCenter : MonoBehaviour
{
    public Horizontal horizontal;
    public Vertical vertical;
    public int padding;

    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        RectTransform pt = transform.parent.GetComponent<RectTransform>();
        Vector2 pos = new Vector2(rt.localPosition.x, rt.localPosition.y);
        if (horizontal != Horizontal.None)
            pos.x = horizontal == Horizontal.Left ? rt.rect.width / 2 + padding - pt.rect.width / 2 : pt.rect.width / 2 - rt.rect.width / 2 - padding;
        if (vertical != Vertical.None)
            pos.y = vertical == Vertical.Bottom ? rt.rect.height / 2 + padding - pt.rect.height / 2 : pt.rect.height / 2 - rt.rect.height / 2 - padding;
        rt.localPosition = pos;
    }
}
