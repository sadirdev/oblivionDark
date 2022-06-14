using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float _durationLive;
    void Start()
    {
        StartCoroutine(enumerator());
    }
    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(_durationLive);
        Destroy(gameObject);
    }
   
}
