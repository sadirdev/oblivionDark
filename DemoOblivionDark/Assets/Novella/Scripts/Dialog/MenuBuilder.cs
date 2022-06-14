using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuBuilder : MonoBehaviour
{
    [HideInInspector]public DialogTablo dt;
    
    [HideInInspector] public int JumpNext;
    public TMP_Text Text;
    [SerializeField] Button _button;
    
    public void ButtonClick()
    {
        dt.DestroyMenu();
        dt.JumpIndex = JumpNext;
        dt.NextPhrase();
    }
    
}
