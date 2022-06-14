using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelInfoNovella : MonoBehaviour
{
    private CanvasGroup _panel;
    void Start()
    {
        _panel = GetComponent<CanvasGroup>();
        _panel.alpha = 0;
        _panel.DOFade(1, DialogFon.DurationFade);
    }
    public void ClickClose()
    {
        _panel.DOFade(0, DialogFon.DurationFade).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

   
}
