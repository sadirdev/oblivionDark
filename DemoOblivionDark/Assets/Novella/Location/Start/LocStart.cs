using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocStart : MonoBehaviour
{
    [SerializeField] private GameObject _choiseLang;

    
    private void Start()
    {
        LocMngr.LocUpdate += Jump;

        _choiseLang.SetActive(false);
        if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
        {
            MainAudio.Static.PlayStart();
            SS.sv.Lang = lng.rus;
            Dialog.BuildWithDic("start", () => { Show.Loc.JumpLoc(Location.Portal); MainAudio.Static.PlayFon(); });
        }
        else if (Application.systemLanguage == SystemLanguage.Portuguese || Application.systemLanguage == SystemLanguage.Spanish)
        {
            MainAudio.Static.PlayStart();
            SS.sv.Lang = lng.por;
            Dialog.BuildWithDic("start", () => { Show.Loc.JumpLoc(Location.Portal); MainAudio.Static.PlayFon(); });
        }
        else
        {
            SS.sv.Lang = lng.eng;
        _choiseLang.SetActive(true);
        }
        


    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    public void ClichChoise(string lang)
    {
        if (lang == "rus") SS.sv.Lang = lng.rus;
        else if (lang == "eng") SS.sv.Lang = lng.eng;
        else if (lang == "por") SS.sv.Lang = lng.por;
        _choiseLang.SetActive(false);
        MainAudio.Static.PlayStart();
        Dialog.BuildWithDic("start", () => { Show.Loc.JumpLoc(Location.Portal); MainAudio.Static.PlayFon(); });
    }

    private void Jump()
    {
       
    }


}
