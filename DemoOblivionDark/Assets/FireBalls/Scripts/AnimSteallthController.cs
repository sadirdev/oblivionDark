using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSteallthController : MonoBehaviour
{
	[Range(0f,1f)]
	public float Stealth;
	private Material _material;
	void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
	}

	private void Update()
    {

		_material.SetFloat("_Stealth", Stealth);
	}
    
}
