using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatoryPath : MonoBehaviour
{
    private void Start()
    {

        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
       
        if(MissionManager.CrntStep(Mission.Kep_5lvl_√енос—пасиƒев,3))
        {
            LocMngr.NoJump = true;
            Dialog.BuildYesNo("ƒочь¬опасности", () =>
            {
                Clicker.NoJump = true;
                Show.Build.clicker(Clicker.EnemyEnum.Ѕогомол, () =>
                {
                    LocMngr.NoJump = true;
                    Dialog.BuildWithDic("ќтецЌапал", () => 
                    {
                        Show.Loc.JumpLoc(Location.CityZ);
                        Dialog.BuildWithDic("яЌе—делалЌичегоѕлохого", () => 
                        {
                            SS.sv.Player.StatResident += 0.2f;
                            SS.sv.Player.StatAgress += 0.1f;
                            SS.sv.Player.StatSaitama += 0.15f;

                            MissionManager.NextStep(Mission.Kep_5lvl_√енос—пасиƒев);
                            Show.Build.Reward(LaungeSystem.Word("NewBlock"), null);
                            Show.Build.Reward(LaungeSystem.RewardExp(50), () => { LvlManager.Static.GetExp(50); });
                        });
                    });
                }, () =>
                {
                    Clicker.NeedApgrateClass();
                    Show.Loc.JumpLoc(Location.CityZ);
                });
            },()=>
            {
                if(LocMngr.BuferLoc == Location.PradatoryPath) Show.Loc.JumpLoc(Location.CityZ);
                else Show.Loc.JumpLoc(LocMngr.BuferLoc);

            });
        }
    }
    public void ClickHouse()
    {
        if(Inventory.Contains(" люч«ункоти"))
        {
            Dialog.NotificationWithDic(LaungeSystem.Word("Rafureshidon"), () => 
            {
                Show.Build.clicker(Clicker.EnemyEnum.Flower,()=>
                {
                    Show.Loc.JumpLoc(Location.PradatoryPath);
                    Dialog.NotificationWithDic(LaungeSystem.Word("RafureshidonFinish"), ()=>
                    {
                        
                        Show.Build.Reward(LaungeSystem.RewardItem(" люч«ункоти"), () =>
                        {
                            Inventory.Add("ѕропуск арта");
                            Inventory.Remove(" люч«ункоти");
                            MissionManager.Complite(Mission.Ћютый√олод);
                        });
                        Show.Build.Reward(LaungeSystem.RewardExp(20), () => { LvlManager.Static.GetExp(20); });
                        Show.Build.Reward(LaungeSystem.RewardCoins(350), () => { Coins.MoveCoins(350); });
                    });
                    
                }, ()=> 
                {
                    Clicker.NeedApgrateClass();
                    Show.Loc.JumpLoc(Location.PradatoryPath); 
                });
            });
        }
        else Dialog.Notification(LaungeSystem.Word("DoorClose"));

        
    }
    public void ClickHome()
    {
        if(SS.sv.Player.Name == Player.Genos)
        {
            if(MissionManager.CrntStep(Mission.ѕроверкаЌовобранца,0) && !Inventory.Contains("√лорибас√олова"))
            {
                Dialog.NotificationWithDic(LaungeSystem.Word("—лышим¬опль"), () => { Show.Build.clicker(Clicker.EnemyEnum.√лорибас,()=>
                {
                    Inventory.Add("√лорибас√олова");
                    MissionManager.NextStep(Mission.ѕроверкаЌовобранца);
                },()=> 
                {
                    Clicker.NeedApgrateClass();
                }); });
            }
            else Dialog.Notification(LaungeSystem.Word("«апертаƒверь"));

        }
        else Show.Loc.JumpLoc(Location.PredatoryHome);

    }
}
