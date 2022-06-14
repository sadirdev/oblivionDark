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

        if (MissionManager.CrntStep(Mission.Kep_5lvl_�������������, 0)|| MissionManager.CrntStep(Mission.Kep_5lvl_�������������, 2) || MissionManager.CrntStep(Mission.Kep_5lvl_�������������, 3)) _saitama.SetActive(false);//������� ���




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
        //else if(MissionManager.CrntStep(Mission.Kep_4lvl_���������,0)) _growBttn.SetActive(true);
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
        else if (Inventory.Contains("�����������")) _growBttn.SetActive(true);
        else if (MissionManager.CrntStep(Mission.Kep_3lvl_DarkHero, 3)) _growBttn.SetActive(true);
        else if (MissionManager.CrntStep(Mission.Kep_4lvl_���������, 0)) _growBttn.SetActive(true);
        else if (MissionManager.CrntStep(Mission.Kep_5lvl_�������������, 0))
        {
            _kitchenBttn.SetActive(true);
            if (!Dialog.IsComplite("������������������")) DeleteSpagetty();
        }
        else if (MissionManager.CrntStep(Mission.Kep_5lvl_�������������, 2)) Dialog.BuildWithDic("����������", ()=> { MissionManager.NextStep(Mission.Kep_5lvl_�������������); });
        else if (MissionManager.CrntStep(Mission.Kep_6lvl_��������������, 0)) _growBttn.SetActive(true);
        else if (MissionManager.CrntStep(Mission.Kep_7lvl_����������, 0)) _growBttn.SetActive(true);
        else if (MissionManager.CrntStep(Mission.Kep_7lvl_����������, 2)) _growBttn.SetActive(true);
        else _growBttn.SetActive(false);
    }
    public void ClickSaitama()
    {
        _growBttn.SetActive(false);
        if (MissionManager.CrntStep(Mission.Kep_3lvl_DarkHero, 1))
        {
            Dialog.BuildWithDic("������ ����� ������� � ������", () =>
            {
                MissionManager.NextStep(Mission.Kep_3lvl_DarkHero);
                Inventory.Add("�����������");
                SS.sv.Player.StatAgress += 0.07f;
            });

        }
        else if (Inventory.Contains("�����������"))
        {
            Dialog.BuildWithDic("���� ���������", () =>
            {
                SS.sv.Player.StatSaitama += 0.15f;
                
                Show.Build.Reward(LaungeSystem.Word("NewBlock"), () => 
                {
                    MissionManager.NextStep(Mission.Kep_3lvl_DarkHero);
                    Inventory.Remove("�����������");
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
        else if(MissionManager.CrntStep(Mission.Kep_4lvl_���������, 0))
        {
            if (NecessaryLvl(2)) return ;
            Dialog.BuildWithDic("��������������", () => { MissionManager.NextStep(Mission.Kep_4lvl_���������); });
        }
        else if (MissionManager.CrntStep(Mission.Kep_6lvl_��������������, 0))
        {
            if (NecessaryLvl(3)) return;
            
            Dialog.BuildWithDic("����������������", () => { MissionManager.NextStep(Mission.Kep_6lvl_��������������); SS.sv.Player.StatAgress -= 0.2f; });
        }
        else if(MissionManager.CrntStep(Mission.Kep_7lvl_����������,0))
        {
            if (NecessaryLvl(3)) return;
            Dialog.BuildWithDic("��������", () => { MissionManager.NextStep(Mission.Kep_7lvl_����������); SS.sv.Player.StatAgress -= 0.05f; });
        }
        else if (MissionManager.CrntStep(Mission.Kep_7lvl_����������, 2))
        {
            Dialog.BuildWithDic("����������", () => { MissionManager.NextStep(Mission.Kep_7lvl_����������); });
        }


        _crntSaitama.DOFade(0, DialogFon.DurationFade);
        
    }
    public void ClickKitchen()
    {
        if (NecessaryLvl(2)) return;
        if (!Inventory.Contains("�����"))
        {
            Dialog.Notification(LaungeSystem.Word("NeedNoodles"));
        }
        else
        {
            Show.Build.SliderGame("�����",2);
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
            if (item.Name == "�����") Inventory.Remove("�����");
        }
    }
    public void Exit()
    {
        Show.Loc.JumpLoc(Location.CityZ);
    }
}
