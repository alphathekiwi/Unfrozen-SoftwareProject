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
    public Vector2 padding;

    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        RectTransform pt = transform.parent.GetComponent<RectTransform>();
        Vector2 pos = new Vector2(rt.localPosition.x, rt.localPosition.y);
        if (horizontal != Horizontal.None)
        {
            float padx = padding.x > 1 ? padding.x : pt.rect.width * padding.x;
            pos.x = horizontal == Horizontal.Left ? rt.rect.width / 2 + padx - pt.rect.width / 2 : pt.rect.width / 2 - rt.rect.width / 2 - padx;
        }
        if (vertical != Vertical.None)
        {
            float pady = padding.y > 1 ? padding.y : pt.rect.height * padding.y;
            pos.y = vertical == Vertical.Bottom ? rt.rect.height / 2 + pady - pt.rect.height / 2 : pt.rect.height / 2 - rt.rect.height / 2 - pady;
        }
        rt.localPosition = pos;
    }
}
