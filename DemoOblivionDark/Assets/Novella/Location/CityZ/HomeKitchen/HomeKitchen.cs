using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HomeKitchen : MonoBehaviour
{
    [SerializeField] private GameObject _white;
    [SerializeField] private GameObject _black;
    [SerializeField] private GameObject _growBttn;
    [SerializeField] private GameObject _saitama;
    [SerializeField] private GameObject _kitchenBttn;
    private Image _crntSaitama;

    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());

        if (MissionManager.CrntStep(Mission.Kep_5lvl_ГеносСпасиДев, 0)|| MissionManager.CrntStep(Mission.Kep_5lvl_ГеносСпасиДев, 2) || MissionManager.CrntStep(Mission.Kep_5lvl_ГеносСпасиДев, 3)) _saitama.SetActive(false);//Сайтамы нет




        if (SS.sv.Player.Name == Player.Virus)
        {
            _white.SetActive(true);
            _crntSaitama = _white.GetComponent<Image>();
        }
        else
        {
            _black.SetActive(true);
            _crntSaitama = _black.GetComponent<Image>();
        }
            

        //if (MissionManager.CrntStep(Mission.Kep_3lvl_DarkHero, 1)) _growBttn.SetActive(true);
        //else if(MissionManager.CrntStep(Mission.Kep_4lvl_ВирусШтаб,0)) _growBttn.SetActive(true);
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
        _kitchenBttn.SetActive(false);
        _growBttn.SetActive(true);
        _crntSaitama.DOFade(1, DialogFon.DurationFade);
        if (MissionManager.CrntStep(Mission.Kep_3lvl_DarkHero, 1)) _growBttn.SetActive(true);
        else if (Inventory.Contains("ПлащСайтамы")) _growBttn.SetActive(true);
        else if (MissionManager.CrntStep(Mission.Kep_3lvl_DarkHero, 3)) _growBttn.SetActive(true);
        else if (MissionManager.CrntStep(Mission.Kep_4lvl_ВирусШтаб, 0)) _growBttn.SetActive(true);
        else if (MissionManager.CrntStep(Mission.Kep_5lvl_ГеносСпасиДев, 0))
        {
            _kitchenBttn.SetActive(true);
            if (!Dialog.IsComplite("СайтамаГражданский")) DeleteSpagetty();
        }
        else if (MissionManager.CrntStep(Mission.Kep_5lvl_ГеносСпасиДев, 2)) Dialog.BuildWithDic("СайтамыНет", ()=> { MissionManager.NextStep(Mission.Kep_5lvl_ГеносСпасиДев); });
        else if (MissionManager.CrntStep(Mission.Kep_6lvl_ВирусПриставка, 0)) _growBttn.SetActive(true);
        else if (MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен, 0)) _growBttn.SetActive(true);
        else if (MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен, 2)) _growBttn.SetActive(true);
        else _growBttn.SetActive(false);
    }
    public void ClickSaitama()
    {
        _growBttn.SetActive(false);
        if (MissionManager.CrntStep(Mission.Kep_3lvl_DarkHero, 1))
        {
            Dialog.BuildWithDic("Второй Далог Сайтамы и Геноса", () =>
            {
                MissionManager.NextStep(Mission.Kep_3lvl_DarkHero);
                Inventory.Add("ГрязныйПлащ");
                SS.sv.Player.StatAgress += 0.07f;
            });

        }
        else if (Inventory.Contains("ПлащСайтамы"))
        {
            Dialog.BuildWithDic("Вещи Постираны", () =>
            {
                SS.sv.Player.StatSaitama += 0.15f;
                
                Show.Build.Reward(LaungeSystem.Word("NewBlock"), () => 
                {
                    MissionManager.NextStep(Mission.Kep_3lvl_DarkHero);
                    Inventory.Remove("ПлащСайтамы");
                });
                Show.Build.Reward(LaungeSystem.RewardExp(40), () => { LvlManager.Static.GetExp(40); });


            });
        }
        else if(MissionManager.CrntStep(Mission.Kep_3lvl_DarkHero,3))
        {
            if (NecessaryLvl(2)) return;
            else if (ST.sv.LvlKepler == 2) Dialog.Notification(LaungeSystem.Word("KeplerNeed3lvl"));
            return;
        }
        else if(MissionManager.CrntStep(Mission.Kep_4lvl_ВирусШтаб, 0))
        {
            if (NecessaryLvl(2)) return ;
            Dialog.BuildWithDic("БэнгЗоветВШтаб", () => { MissionManager.NextStep(Mission.Kep_4lvl_ВирусШтаб); });
        }
        else if (MissionManager.CrntStep(Mission.Kep_6lvl_ВирусПриставка, 0))
        {
            if (NecessaryLvl(3)) return;
            
            Dialog.BuildWithDic("ВирусВМастерскую", () => { MissionManager.NextStep(Mission.Kep_6lvl_ВирусПриставка); SS.sv.Player.StatAgress -= 0.2f; });
        }
        else if(MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен,0))
        {
            if (NecessaryLvl(3)) return;
            Dialog.BuildWithDic("ЯУберусь", () => { MissionManager.NextStep(Mission.Kep_7lvl_ГеносМумен); SS.sv.Player.StatAgress -= 0.05f; });
        }
        else if (MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен, 2))
        {
            Dialog.BuildWithDic("ИдёмКМумену", () => { MissionManager.NextStep(Mission.Kep_7lvl_ГеносМумен); });
        }


        _crntSaitama.DOFade(0, DialogFon.DurationFade);
        
    }
    public void ClickKitchen()
    {
        if (NecessaryLvl(2)) return;
        if (!Inventory.Contains("Лапша"))
        {
            Dialog.Notification(LaungeSystem.Word("NeedNoodles"));
        }
        else
        {
            Show.Build.SliderGame("Рамен",2);
        }
        
    }

    public void JumpBedroom()
    {
        Show.Loc.JumpLoc(Location.HomeBedroom);
    }
    public void JumpHallway()
    {
        Show.Loc.JumpLoc(Location.HomeHallway);
    }

    private bool NecessaryLvl(int necessaryLvl)
    {
        if (SS.sv.Player.LVL < necessaryLvl)
        {
            Dialog.Notification(LaungeSystem.NeedCharacterLvl(necessaryLvl));
            return true;
        }
        return false;
            
    }
    private void DeleteSpagetty()
    {
        foreach (var item in SS.sv.Player.ItemsInventory)
        {
            if (item.Name == "Лапша") Inventory.Remove("Лапша");
        }
    }
    public void Exit()
    {
        Show.Loc.JumpLoc(Location.CityZ);
    }
}
