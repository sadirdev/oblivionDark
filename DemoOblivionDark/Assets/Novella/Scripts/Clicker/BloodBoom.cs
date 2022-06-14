using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBoom : MonoBehaviour
{
    [SerializeField] private GameObject _prefabBlood;

    private static GameObject _staticPrefabBlood;
    private static Transform _staticTransform;

    private void Awake()
    {
        _staticTransform = transform;
        _staticPrefabBlood = _prefabBlood;
    }
    public static void Build()
    {
        Instantiate(_staticPrefabBlood, _staticTransform);
    }
}
