using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tank : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private BulletSphere _bulletSphere;
    [SerializeField] private float _delayBetweemShoots;
    [SerializeField] private float _reoilDistance;
    
    public static bool BulletGun = false;
    public static float Pitch;
    public static float Bonus;

    private float _timeAfterShoot;

   
   

    private void Update()
    {
        _timeAfterShoot += Time.deltaTime;
        
        if (Input.GetMouseButton(0) && BulletGun)
        {
            if(_timeAfterShoot > _delayBetweemShoots)
            {
                Shoot();
                transform.DOMoveZ(transform.position.z - _reoilDistance, _delayBetweemShoots / 2).SetLoops(2, LoopType.Yoyo);
                _timeAfterShoot = 0;
            }
        }
        else
        {
            Pitch = 0.9f;
            Bonus = 2;
        }
    }
    private void Shoot()
    {
        Instantiate(_bulletTemplate, _shootPoint.position, Quaternion.identity);
        Instantiate(_bulletSphere, _shootPoint.position, Quaternion.identity);
        Pitch += 0.07f;
        Bonus += 0.2f;
    }
}
