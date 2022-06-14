using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthControllerExample : MonoBehaviour {


	Material stealthMaterial;

	public float stealth;

	public Button button;
	// Use this for initialization
	public void ClickBtn()
    {
		StopAllCoroutines();
		StartCoroutine("StealthIn");
	}
	void Start () {
		stealthMaterial = GetComponent<Renderer> ().material;
		//StopAllCoroutines();
		//StartCoroutine("StealthIn");
	}
			
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Jump")) {
			StopAllCoroutines ();
			StartCoroutine ("StealthIn");
		}
	}



	IEnumerator StealthIn() {
		stealth = 0;
		while(stealth < 1 ){
			stealth += 0.5f * Time.deltaTime;
			stealthMaterial.SetFloat ("_Stealth",stealth);
			yield return null;
		}
	}
}
