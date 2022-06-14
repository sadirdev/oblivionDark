using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValuePass : MonoBehaviour {

	[SerializeField] Text progress;



	public  void UpdateProgress (float content)
	{
		progress.text = Mathf.Round( content*100) +"%";
        if (content == 1)
        {

            FindObjectOfType<ProgressLvlT360>().KillAnim();
        }
    }


}
