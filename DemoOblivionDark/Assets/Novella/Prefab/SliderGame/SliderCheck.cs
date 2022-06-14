using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SliderCheck : MonoBehaviour
{
    public bool Hit;
    [SerializeField] private Image _img;
    [SerializeField] private Color _colorFalse;
    [SerializeField] private Color _colorTrue;



    void Start()
    {
        Color color = _colorTrue;
        if(!Hit) color = _colorFalse;
        _img.DOColor(color, 0.2f).SetLoops(7, LoopType.Yoyo).OnComplete(() => { Destroy(gameObject); });
    }

    
}
