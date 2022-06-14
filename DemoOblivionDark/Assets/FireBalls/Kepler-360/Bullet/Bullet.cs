using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _bounceForse;
    [SerializeField] private float _bounceDistance;
    private Transform _camera;
    private Vector3 _moveDirection;
    private bool _bounce = false;
    [SerializeField] private AudioSource _soundBreak;
    [SerializeField] private AudioSource _soundBounce;

    private void Start()
    {
        _moveDirection = Vector3.forward;
        StartCoroutine(DestroyBall());
        _soundBreak.pitch = Tank.Pitch;
    }
    private void Update()
    {
        if(_bounce)
        {
            transform.position = Vector3.MoveTowards(transform.position, _camera.position, _speed * 1.2f * Time.deltaTime);
        }
            
        else
        {
            if (!Tank.BulletGun) Destroy(gameObject);
            transform.Translate(_moveDirection * _speed * Time.deltaTime);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
       

        if (other.TryGetComponent(out Block block))
        {
            
            block.Break();
            Instantiate(_soundBreak);
            Destroy(gameObject);
        }
        else if(other.TryGetComponent(out Obstacle obstacle))
        {
            Tank.BulletGun = false;
            _soundBounce.Play();
            Bounce();
        }
        else if(other.TryGetComponent(out CameraBall camera))
        {
            //Когда шарик прилетит в камеру
            Destroy(gameObject);
            FindObjectOfType<LvlProgress>().UI.SetActive(false);
            GameObject.FindObjectOfType<MenuBall>().MenuBuild(MenuBall.BuildPanelType.Restart);
            

        }
    }
    private void Bounce()
    {
        _camera = FindObjectOfType<CameraBall>().transform;
        _bounce = true;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        //rigidbody.AddExplosionForce(_bounceForse, transform.position + new Vector3(0, -1, 1), _bounceDistance);
    }
    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
