using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBounds : MonoBehaviour
{

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void OutOfBounds()
    {
        Debug.Log("Position of the object : " + rectTransform.anchoredPosition);
        if (rectTransform.anchoredPosition.x < minX)
        {
            rectTransform.anchoredPosition = new Vector2(minX, rectTransform.anchoredPosition.y);
        }
        if (rectTransform.anchoredPosition.x > maxX)
        {
            rectTransform.anchoredPosition = new Vector2(maxX, rectTransform.anchoredPosition.y);
        }
        if (rectTransform.anchoredPosition.y < minY)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, minY);
        }
        if (rectTransform.anchoredPosition.y > maxY)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, maxY);
        }
    }

    // Update is called once per frame
    void Update()
    {
        OutOfBounds();
    }
}
