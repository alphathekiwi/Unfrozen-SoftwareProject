using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SpaceDriver : MonoBehaviour
{
    public float height = 1;
    public float width = 1;
    void Awake()
    {
        RectTransform pt = transform.parent.GetComponent<RectTransform>();
        RectTransform rt = GetComponent<RectTransform>();
        float calw = width == 0 ? rt.rect.width : pt.rect.width * width;
        float calh = height == 0 ? rt.rect.height : pt.rect.height * height;
        rt.sizeDelta = new Vector2(calw, calh);
    }
}
