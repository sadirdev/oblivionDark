using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class T360Click : MonoBehaviour
{
    [SerializeField] private GameObject _loadScene;
    void Start()
    {
        transform.DORotate(new Vector3(0, 148.4f, 0), 5, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        
    }

    public void Click()
    {
        Dialog.BuildWithDic("ÏðûæîêÍàÒ360", () =>
        {
            Instantiate(_loadScene);
        });
    }
}
