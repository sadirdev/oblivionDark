using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    [SerializeField] private LoopType _loopType;

    [SerializeField] private float _x;
    [SerializeField] private float _y;
    [SerializeField] private float _z;

    void Start()
    {
        transform.DORotate(new Vector3(transform.rotation.x + _x, transform.rotation.y + _y, transform.rotation.z + _z), _duration, RotateMode.FastBeyond360).SetEase(_ease).SetLoops(-1, _loopType);
    }

   
}
