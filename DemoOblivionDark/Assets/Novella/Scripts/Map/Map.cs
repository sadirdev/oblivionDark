using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject _prefabFocus;
    [SerializeField] private GameObject _prefabGoal;
    [SerializeField] private Transform _closeBttn;

    public static GameObject PrefabFocus;
    public static GameObject PrefabGoal;

    private void Awake()
    {
        if(PrefabFocus == null)
        {
            PrefabFocus = _prefabFocus;
            PrefabGoal = _prefabGoal;
        }
    }

    private void Start()
    {
        
        AspectRatioFitter canvasGroup = GetComponent<AspectRatioFitter>();
        canvasGroup.enabled = false;
        canvasGroup.enabled = true;
        StartCoroutine(startRotate());
        IEnumerator startRotate()
        {
            yield return new WaitForSeconds(0.5f);
            _closeBttn.DORotate(new Vector3(0, 0, 180), 3, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
        
    }
    public void Close()
    {
        Destroy(gameObject);
    }
}
