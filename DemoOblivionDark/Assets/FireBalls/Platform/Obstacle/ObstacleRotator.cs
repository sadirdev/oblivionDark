using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class ObstacleRotator : MonoBehaviour
{
    TweenerCore<Quaternion, Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> _rotate;
    [SerializeField] private float _animationDuration;
    public LvlData.PatternObst Pattern;
    private Transform _pattern;
    private void Start()
    {
        _pattern =  Instantiate(Pattern.PatternObj, transform);
        _pattern.localPosition = new Vector3(0, 0, 0);
        _pattern.DORotate(new Vector3(270, 180 - Pattern.Delay, 0), Time.deltaTime, RotateMode.FastBeyond360);

        _pattern.DOMoveY(1, 1f).SetEase(Ease.Linear);
        if (Pattern.MoveMulty != null && Pattern.MoveMulty.Length > 0) Sequence();
        else NotSequence();


    }
    public void StopRotate()
    {
        _rotate.Kill();

        _pattern.DOMoveY(0, 1).SetEase(Ease.Linear);    
        

    }
    private void NotSequence()
    {
        //Debug.Log($"transform.localRotation.x = {transform.localRotation.eulerAngles.x}");
        //Debug.Log($"transform.rotation.x = {transform.rotation.eulerAngles.x}");
        //Debug.Log($"transform.localRotation.y = {transform.localRotation.eulerAngles.y}");
        //Debug.Log($"transform.rotation.y = {transform.rotation.eulerAngles.y}");
        //_rotate = transform.DORotate(new Vector3(270, -Pattern.Degrees + 180, 0), Pattern.Duration, RotateMode.FastBeyond360).SetLoops(-1, Pattern.LoopType).SetEase(Pattern.Ease);
        _rotate = transform.DORotate(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - Pattern.Degrees, 0), Pattern.Duration, RotateMode.FastBeyond360).SetLoops(-1, Pattern.LoopType).SetEase(Pattern.Ease);
    }
    private void Sequence()
    {
        int indexAnim = -1;

        _rotate = transform.DORotate(new Vector3(270, transform.rotation.eulerAngles.y - Pattern.Degrees, 0), Pattern.Duration, RotateMode.FastBeyond360).SetEase(Pattern.Ease).OnComplete(Next);


        void Next()
        {
            indexAnim++;
            if(Pattern.MoveMulty.Length == indexAnim)
            {
                Sequence();
                return;
            }
           _rotate = transform.DORotate(new Vector3(270, transform.rotation.eulerAngles.y - Pattern.MoveMulty[indexAnim].Degrees, 0), Pattern.MoveMulty[indexAnim].Duration, RotateMode.FastBeyond360).SetEase(Pattern.MoveMulty[indexAnim].Ease).OnComplete(Next);
        }

       
       
    }
    
}
