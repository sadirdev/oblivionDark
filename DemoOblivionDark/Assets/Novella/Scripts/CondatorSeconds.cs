using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondatorSeconds : MonoBehaviour
{
   
     public void Start()
    {
        
        IEnumerator contador = enumerator();

        StopCoroutine(contador);
        StartCoroutine(contador);

        IEnumerator enumerator()
        {
            while (SS.sv.SecondsReward > 0)
            {
                SS.sv.SecondsReward--;
                yield return new WaitForSeconds(1);
            }
            SS.sv.SecondsReward = 0;
        }
    }

    
}
