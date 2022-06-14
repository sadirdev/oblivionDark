using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveObject : MonoBehaviour
{
    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        float delta = Delta();
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x * delta, _rectTransform.sizeDelta.y * delta);
    }

    public static void StartStatic(RectTransform rectTransform)
    {
        float delta = Delta();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * delta, rectTransform.sizeDelta.y * delta);
    }

    public static float Delta()
    {
        if (Screen.height > Screen.width) return Screen.width / 720f;
        else return Screen.height / 720f;
    }
}
