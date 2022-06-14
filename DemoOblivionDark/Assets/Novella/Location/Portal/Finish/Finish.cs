using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
        if (Dialog.IsComplite("Finish"))
        {
            Debug.Log("»√–≈  ŒÕ≈÷");
        }
    }
}
