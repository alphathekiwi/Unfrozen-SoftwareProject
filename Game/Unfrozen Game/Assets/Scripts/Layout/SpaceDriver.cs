using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CopySizeFrom
{
    None, WidthToHeight, HeightToWidth
}

[RequireComponent(typeof(RectTransform))]
public class SpaceDriver : MonoBehaviour
{
    public CopySizeFrom CopySizeFrom;
    public float height = 1;
    public float width = 1;
    private Vector2 calcSize;
    public Vector2 size
    {
        get
        {
            if (calcSize == null || calcSize == Vector2.zero)
                calcSize = getSize();
            return calcSize;
        }
    }
    void Awake()
    {
        GetComponent<RectTransform>().sizeDelta = size;
    }
    Vector2 getSize()
    {
        RectTransform rt = GetComponent<RectTransform>();
        SpaceDriver ps = transform.parent.GetComponent<SpaceDriver>();
        if (ps != null) return UseAspect(calcPix(rt.rect.width, ps.size.x, width), calcPix(rt.rect.height, ps.size.y, height));
        Rect pt = transform.parent.GetComponent<RectTransform>().rect;
        return UseAspect(calcPix(rt.rect.width, pt.width, width), calcPix(rt.rect.height, pt.height, height));
    }
    Vector2 UseAspect(float x, float y)
    {
        if (CopySizeFrom == CopySizeFrom.HeightToWidth) x = y;
        if (CopySizeFrom == CopySizeFrom.WidthToHeight) y = x;
        return new Vector2(x, y);
    }
    float calcPix(float orgl, float cont, float mod)
    {
        if (mod == 0)
            return orgl;
        if (mod > 1)
            return mod;
        return cont * mod;
    }
}
