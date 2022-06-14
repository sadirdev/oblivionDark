using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScrollJumpLoc : MonoBehaviour
{
    [SerializeField] Transform _content;
    [SerializeField] GameObject _bttn;
   

    

    public void Start()
    {
        LocBttn locBttn = FindObjectOfType<LocBttn>();
        if (locBttn == null) return;
        var bttnObj = locBttn.BttnObj;
        

        StartCoroutine(BuildBttn());
        IEnumerator BuildBttn()
        {
            yield return new WaitForSeconds(0.35f);
            foreach (var bttnLoc in bttnObj)
            { 
                if (bttnLoc.Obj.activeSelf)
                {
                    GameObject bttnPanel = Instantiate(_bttn, _content);

                    string textBttn = "";
                    if (SS.sv.Lang == lng.rus) textBttn = bttnLoc.NameRus;
                    else if(SS.sv.Lang == lng.eng) textBttn = bttnLoc.NameEng;
                    else if (SS.sv.Lang == lng.por) textBttn = bttnLoc.NamePor;
                    bttnPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = textBttn;

                    Button bttnText = bttnPanel.transform.GetChild(0).GetComponent<Button>();
                    bttnText.onClick = bttnLoc.Obj.transform.GetChild(0).GetComponent<Button>().onClick;
                    bttnText.onClick.AddListener(() =>
                    {
                        NameLocPanel.Close();
                    });
                }
               
            }
        }

        
    }
}
