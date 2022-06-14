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
       
        if(MissionManager.CrntStep(Mission.Kep_5lvl_�������������,3))
        {
            LocMngr.NoJump = true;
            Dialog.BuildYesNo("��������������", () =>
            {
                Clicker.NoJump = true;
                Show.Build.clicker(Clicker.EnemyEnum.�������, () =>
                {
                    LocMngr.NoJump = true;
                    Dialog.BuildWithDic("���������", () => 
                    {
                        Show.Loc.JumpLoc(Location.CityZ);
                        Dialog.BuildWithDic("����������������������", () => 
                        {
                            SS.sv.Player.StatResident += 0.2f;
                            SS.sv.Player.StatAgress += 0.1f;
                            SS.sv.Player.StatSaitama += 0.15f;

                            MissionManager.NextStep(Mission.Kep_5lvl_�������������);
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
        if(Inventory.Contains("�����������"))
        {
            Dialog.NotificationWithDic(LaungeSystem.Word("Rafureshidon"), () => 
            {
                Show.Build.clicker(Clicker.EnemyEnum.Flower,()=>
                {
                    Show.Loc.JumpLoc(Location.PradatoryPath);
                    Dialog.NotificationWithDic(LaungeSystem.Word("RafureshidonFinish"), ()=>
                    {
                        
                        Show.Build.Reward(LaungeSystem.RewardItem("�����������"), () =>
                        {
                            Inventory.Add("������������");
                            Inventory.Remove("�����������");
                            MissionManager.Complite(Mission.����������);
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
            if(MissionManager.CrntStep(Mission.������������������,0) && !Inventory.Contains("��������������"))
            {
                Dialog.NotificationWithDic(LaungeSystem.Word("�����������"), () => { Show.Build.clicker(Clicker.EnemyEnum.��������,()=>
                {
                    Inventory.Add("��������������");
                    MissionManager.NextStep(Mission.������������������);
                },()=> 
                {
                    Clicker.NeedApgrateClass();
                }); });
            }
            else Dialog.Notification(LaungeSystem.Word("������������"));

        }
        else Show.Loc.JumpLoc(Location.PredatoryHome);

    }
}
