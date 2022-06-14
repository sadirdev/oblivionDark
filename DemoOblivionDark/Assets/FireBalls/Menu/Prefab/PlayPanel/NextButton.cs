using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _button;
 
    

    public void Disable()
    {
        _button.enabled = false;
        _canvasGroup.alpha = 0.5f;
    }

    public void Enable()
    {
        _button.enabled = true;
        _canvasGroup.DOFade(1, 0.3f);
    }
}
