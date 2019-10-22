using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Horizontal
{
    None, Left, Right, Offset
}
public enum Vertical
{
    None, Top, Bottom, Offset
}
[RequireComponent(typeof(RectTransform))]
public class RectCenter : MonoBehaviour
{
    public Horizontal horizontal;
    public Vertical vertical;
    public Vector2 padding;
    void Start() => UpdatePosition();
    public void UpdatePosition()
    {
        RectTransform rt = GetComponent<RectTransform>();
        RectTransform pt = transform.parent.GetComponent<RectTransform>();
        Vector2 pos = new Vector2(rt.localPosition.x, rt.localPosition.y);
        float padx = padding.x > 1 ? padding.x : pt.rect.width * padding.x;
        switch (horizontal)
        {
            case Horizontal.Left:
                pos.x = rt.rect.width / 2 + padx - pt.rect.width / 2; break;
            case Horizontal.Right:
                pos.x = pt.rect.width / 2 - rt.rect.width / 2 - padx; break;
            case Horizontal.Offset:
                pos.x = padx; break;
        }
        float pady = padding.y > 1 ? padding.y : pt.rect.height * padding.y;
        switch (vertical)
        {
            case Vertical.Top:
                pos.y = pt.rect.height / 2 - rt.rect.height / 2 - pady; break;
            case Vertical.Bottom:
                pos.y = rt.rect.height / 2 + pady - pt.rect.height / 2; break;
            case Vertical.Offset:
                pos.y = pady; break;
        }
        rt.localPosition = pos;
    }
}
