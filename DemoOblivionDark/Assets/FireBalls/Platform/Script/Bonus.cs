using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public Text Cound;
    [SerializeField] private Text _text;
    [SerializeField] private CanvasGroup _fade;
    void Start()
    {
        if (SS.sv.Lang == lng.rus) _text.text = "Рандом";
        else if (SS.sv.Lang == lng.eng) _text.text = "Random";
        else if (SS.sv.Lang == lng.por) _text.text = "Aleatorio";

        transform.DOMoveY(transform.position.y + 200 * AdaptiveObject.Delta(), 2);
        _fade.DOFade(0, 2).OnComplete(()=> { Destroy(gameObject); });
        
    }

    
}
