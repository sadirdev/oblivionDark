using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestColor : MonoBehaviour
{
    [SerializeField] private Image _color;
    public LvlData[] AllLvl;
    void Start()
    {
        for (int i  = 0; i< AllLvl.Length; i ++)
        {
            var colorBlock = Instantiate(_color, transform);
            colorBlock.color = AllLvl[i].Colors[0];
            colorBlock.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
        }
    }

    
}
