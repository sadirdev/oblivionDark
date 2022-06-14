using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RewardsTablo : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    [SerializeField] private Transform _content;
    [SerializeField] private TMP_Text _reward;
    private List<DelegateVoid> _delegateVoids = new List<DelegateVoid>();

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, DialogFon.DurationFade);
    }

    public void Build(string reward, DelegateVoid delegateVoid)
    {
        Instantiate(_reward, _content).text = reward;
        _delegateVoids.Add(delegateVoid);
    }
   
    public void Close()
    {
        _canvasGroup.DOFade(0, DialogFon.DurationFade).OnComplete(() =>
        {
            foreach (var item in _delegateVoids)
            {
                if(item !=null) item(); 
            }
            
            Destroy(gameObject);
        });
        
    }
}
