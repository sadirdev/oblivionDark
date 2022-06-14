using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shinava : MonoBehaviour
    
{
    [SerializeField] private GameObject _bang;
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        ActiveBang();
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }

    private void ActiveBang()
    {
        if (SS.sv.Player.Name == Player.Genos && !MissionManager.IsComplite(Mission.������������������)) _bang.SetActive(true);
        else _bang.SetActive(false);
        if (MissionManager.CrntStep(Mission.������������������, 2)) _bang.SetActive(false);
    }
    private void Jump()
    {
        ActiveBang();
    }

    public void ClickHome()
    {
        if(!Dialog.IsComplite("������������������"))
        { 
            Dialog.BuildWithDic("������������������", () => {  MissionManager.Add(Mission.������������������); });
        }
        else if(MissionManager.CrntStep(Mission.������������������,0))
        {
            Dialog.Build("��������������");
        }
        else if(MissionManager.CrntStep(Mission.������������������, 1))
        {
            Dialog.BuildWithDic("������������", () => { MissionManager.NextStep(Mission.������������������); Inventory.Remove("��������������"); });
        }
        else if(MissionManager.CrntStep(Mission.������������������,3))
        {
            Dialog.BuildYesNo("�����������", () =>
            {
                
                Show.Build.Reward(LaungeSystem.RewardCoins(600), () =>
                {
                    SS.sv.DonActive = false;
                    Coins.MoveCoins(600);
                    SS.sv.Player.StatResident += 0.15f;
                    MissionManager.Complite(Mission.������������������);
                });

                
                Show.Build.Reward(LaungeSystem.RewardExp(40), () => { LvlManager.Static.GetExp(40); });
            }, () =>
            {
                Show.Build.Reward(LaungeSystem.RewardCoins(400), () =>
                {
                    Coins.MoveCoins(400);
                    SS.sv.DonActive = true;
                    SS.sv.Player.StatResident += 0.08f;
                    MissionManager.Complite(Mission.������������������);
                });
                
                Show.Build.Reward(LaungeSystem.RewardExp(25), () => { LvlManager.Static.GetExp(25); });

            });
        }
        
    }
}
