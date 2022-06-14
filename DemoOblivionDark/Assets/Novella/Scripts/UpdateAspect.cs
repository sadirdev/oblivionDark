using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateAspect : MonoBehaviour
{
    
    void Start()
    {
        AspectRatioFitter Filter = GetComponent<AspectRatioFitter>();
        Filter.enabled = false;
        Filter.enabled = true;
    }

    
}
