using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSphere : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _moveDirection;
    private bool _bonus = false;

    private void Start()
    {
        _moveDirection = Vector3.forward;
        StartCoroutine(DestroyBall());
    }
    private void Update()
    {
       

       
            if (!Tank.BulletGun) Destroy(gameObject);
            transform.Translate(_moveDirection * _speed * Time.deltaTime);
        

    }
    //private void OnDestroy()
    //{
    //    StopAllCoroutines();
    //}

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out Block block))
        {
            if(_bonus) LvlProgress.Bonus();
            Destroy(gameObject);
        }
        else if (other.TryGetComponent(out Obstacle obstacle))
        {
            _bonus = true;
        }
        
    }
   
    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
