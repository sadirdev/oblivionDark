using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeObject : MonoBehaviour
{
    [SerializeField] private Image _img;
    private static Color _green = new Color(0, 0.7848396f, 8962264f, 1);
    private void Start()
    {




        _img.DOFade(1, DialogFon.DurationFade).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            Destroy(gameObject);
        });

        //img.DOFade(1, DialogFon.DurationFade).OnComplete(() =>
        //{
        //    img.DOFade(0, DialogFon.DurationFade).OnComplete(()=>
        //    {
        //        Destroy(gameObject);
        //    });
            
        //});
    }
    public static void GreenImg(Image img)
    {
        img.DOColor(_green, 0.5f).SetLoops(2, LoopType.Yoyo);
    }
    public static void RedImg(Image img)
    {
        img.DOColor(Color.red, 0.5f).OnComplete(() =>
        {
            img.DOColor(Color.white, 0.5f);
        });
    }
}
    
