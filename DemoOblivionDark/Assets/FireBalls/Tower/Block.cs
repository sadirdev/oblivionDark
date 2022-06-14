using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private ParticleSystemRenderer _destroyEffect;

    private MeshRenderer _meshRenderer;
   
    

    public event UnityAction<Block> BulletHit;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        //_meshFilter = GetComponent<MeshFilter>();
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
    public void SetScale(float scale)
    {
        transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z);
    }

    public void Break()
    {
        var render = Instantiate(_destroyEffect, transform.position, _destroyEffect.transform.rotation);
        BulletHit?.Invoke(this);

        
        render.material.color = _meshRenderer.material.color;
        //render.mesh = _meshFilter.mesh;
        render.transform.localScale = transform.localScale;
        render.transform.rotation = transform.rotation;
        Destroy(gameObject);
    }
}
