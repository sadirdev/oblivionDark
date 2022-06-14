using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveSquare : MonoBehaviour
{
    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(Time.deltaTime);
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.x);
        }
        
    }

   
}
