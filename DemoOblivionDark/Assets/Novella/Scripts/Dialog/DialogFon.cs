using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogFon : MonoBehaviour
{

    public static float DurationFade =0.5f;
    [SerializeField]private Image _fon;
    public void Show(Sprite fon)
    {
        if (fon == null || fon == _fon.sprite) return;
        

        if(_fon.sprite == null)
        {
            _fon.color = Color.clear;
            _fon.DOFade(1, DurationFade).OnComplete(() =>
            {
                _fon.sprite = fon;
                _fon.DOColor(Color.white, DurationFade);
            });
        }
        else
        {
            _fon.DOColor(Color.black, DurationFade).OnComplete(() =>
            {
                _fon.sprite = fon;
                _fon.DOColor(Color.white, DurationFade);
            });
        }
    }
    public void Hide()
    {
        if(_fon.sprite != null)
        {
            _fon.DOColor(Color.black, DurationFade);
        }
    }
  
}
