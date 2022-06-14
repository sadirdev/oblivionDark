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
        if (SS.sv.Player.Name == Player.Genos && !MissionManager.IsComplite(Mission.ПроверкаНовобранца)) _bang.SetActive(true);
        else _bang.SetActive(false);
        if (MissionManager.CrntStep(Mission.ПроверкаНовобранца, 2)) _bang.SetActive(false);
    }
    private void Jump()
    {
        ActiveBang();
    }

    public void ClickHome()
    {
        if(!Dialog.IsComplite("ПроверкаНовобранца"))
        { 
            Dialog.BuildWithDic("ПроверкаНовобранца", () => {  MissionManager.Add(Mission.ПроверкаНовобранца); });
        }
        else if(MissionManager.CrntStep(Mission.ПроверкаНовобранца,0))
        {
            Dialog.Build("ВопросГлорибас");
        }
        else if(MissionManager.CrntStep(Mission.ПроверкаНовобранца, 1))
        {
            Dialog.BuildWithDic("ГлорибасУбит", () => { MissionManager.NextStep(Mission.ПроверкаНовобранца); Inventory.Remove("ГлорибасГолова"); });
        }
        else if(MissionManager.CrntStep(Mission.ПроверкаНовобранца,3))
        {
            Dialog.BuildYesNo("УзналОГароу", () =>
            {
                
                Show.Build.Reward(LaungeSystem.RewardCoins(600), () =>
                {
                    SS.sv.DonActive = false;
                    Coins.MoveCoins(600);
                    SS.sv.Player.StatResident += 0.15f;
                    MissionManager.Complite(Mission.ПроверкаНовобранца);
                });

                
                Show.Build.Reward(LaungeSystem.RewardExp(40), () => { LvlManager.Static.GetExp(40); });
            }, () =>
            {
                Show.Build.Reward(LaungeSystem.RewardCoins(400), () =>
                {
                    Coins.MoveCoins(400);
                    SS.sv.DonActive = true;
                    SS.sv.Player.StatResident += 0.08f;
                    MissionManager.Complite(Mission.ПроверкаНовобранца);
                });
                
                Show.Build.Reward(LaungeSystem.RewardExp(25), () => { LvlManager.Static.GetExp(25); });

            });
        }
        
    }
}
