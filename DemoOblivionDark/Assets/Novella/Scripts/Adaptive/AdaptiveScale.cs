using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveScale : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    void Start()
    {
        
        _rectTransform = GetComponent<RectTransform>();
        float delta = AdaptiveObject.Delta();

        _rectTransform.localScale = new Vector2(_rectTransform.localScale.x * delta, _rectTransform.localScale.y * delta);
    }

    public static void StartStatic(RectTransform rectTransform)
    {
        float delta = AdaptiveObject.Delta();
        rectTransform.localScale = new Vector2(rectTransform.localScale.x * delta, rectTransform.localScale.y * delta);
    }

}

