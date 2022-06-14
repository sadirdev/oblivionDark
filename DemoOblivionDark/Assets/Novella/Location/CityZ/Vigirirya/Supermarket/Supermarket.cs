using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supermarket : MonoBehaviour
{
    [SerializeField] private GameObject _bttnJuice;
    [SerializeField] private GameObject _bttnGrocery;
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        if (MissionManager.CrntStep(Mission.Kep_4lvl_ВирусШтаб, 3))
        {
            _bttnJuice.SetActive(true);
            _bttnGrocery.SetActive(false);
        }
            
        else
        {
            _bttnGrocery.SetActive(true);
            _bttnJuice.SetActive(false);
        }
        
        Debug.Log($"MissionManager.CrntStep(Mission.Kep_4lvl_ВирусШтаб,3 = {MissionManager.CrntStep(Mission.Kep_4lvl_ВирусШтаб,3)}");
        if (MissionManager.CrntStep(Mission.Kep_4lvl_ВирусШтаб, 2) && !Dialog.IsComplite("ДостатьСок"))
        {
            
            Dialog.BuildYesNo("ДостатьСок", () => { Show.Build.SliderGame("Сок", 1); MissionManager.SetStep(Mission.Kep_4lvl_ВирусШтаб, 3); },
                () =>
                {
                    MissionManager.SetStep(Mission.Kep_4lvl_ВирусШтаб, 3);
                    Jump();
                });
        }
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
        if (MissionManager.CrntStep(Mission.Kep_4lvl_ВирусШтаб, 3))
        {
            _bttnJuice.SetActive(true);
            _bttnGrocery.SetActive(false);
        }
            
        else
        {
            _bttnJuice.SetActive(false);
            _bttnGrocery.SetActive(true);
        }
           

        
    }
    public void ClickJuice()
    {
        Show.Build.SliderGame("Сок",1);
    }
    public void ClickGrocery()
    {
        Show.Loc.JumpLoc(Location.SuperMarket);
        Show.Build.Shop(false);
    }
    public void Exit()
    {
        Show.Loc.JumpLoc(Location.Vigiriya);
    }
}
