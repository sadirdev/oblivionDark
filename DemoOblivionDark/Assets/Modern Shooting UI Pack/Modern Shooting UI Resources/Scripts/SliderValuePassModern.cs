﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValuePassModern : MonoBehaviour {

	Text progress;

	// Use this for initialization
	void Start () {
		progress = GetComponent<Text>();

	}
	
	public  void UpdateProgress (float content) {
		progress.text = Mathf.Round( content*100) +"%";
	}


}
