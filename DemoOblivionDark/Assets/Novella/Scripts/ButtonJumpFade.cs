using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ButtonJumpFade : MonoBehaviour
{
    void Start()
    {
        Image img = GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
        img.DOFade(1, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

   
}
