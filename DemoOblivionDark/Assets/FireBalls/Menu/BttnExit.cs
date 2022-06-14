using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BttnExit : MonoBehaviour
{
    [SerializeField] private Color _yellow;
    public void Start()
    {
        if(ST.sv.NewCharacter)
        {
            GetComponent<Image>().DOColor(_yellow, 0.5f);
        }
    }


    public void Click()
    {
        
        FindObjectOfType<MenuBall>().BuilChoice();
    }
}
