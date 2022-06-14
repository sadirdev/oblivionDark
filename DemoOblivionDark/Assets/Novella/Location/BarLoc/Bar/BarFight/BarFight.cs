using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarFight : MonoBehaviour
{
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        //Interface.NameLoc("Бар «Большая глотка»");
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
       

        if (MissionManager.CrntStep(Mission.КулычныеБои,1) || MissionManager.CrntStep(Mission.КулычныеБои, 0))
        {
            Show.Build.clicker(Clicker.EnemyEnum.КислаяРожа, () => 
            {
                Show.Build.Reward(LaungeSystem.Word("NewEnemy"), () =>
                {
                    SS.sv.Player.StatResident -= 0.05f;
                    SS.sv.Player.StatAgress -= 0.085f;
                    MissionManager.SetStep(Mission.КулычныеБои, 2);
                    Show.Loc.JumpLoc(Location.Bar);
                });
                Show.Build.Reward(LaungeSystem.RewardCoins(300), () => { Coins.MoveCoins(300); });
                Show.Build.Reward(LaungeSystem.RewardExp(38), () => { LvlManager.Static.GetExp(38); });




            }, () =>
            {
                Clicker.NeedApgrateClass();
                Show.Loc.JumpLoc(Location.Bar);
                
            });
        }
        else if (MissionManager.CrntStep(Mission.КулычныеБои, 2))
        {
            Show.Build.clicker(Clicker.EnemyEnum.МастерВМайке, () =>
            {

                Show.Build.Reward(LaungeSystem.Word("NewEnemy"), () =>
                {
                    SS.sv.Player.StatResident -= 0.05f;
                    SS.sv.Player.StatAgress -= 0.085f;
                    MissionManager.NextStep(Mission.КулычныеБои);
                    Show.Loc.JumpLoc(Location.Bar);
                });
                Show.Build.Reward(LaungeSystem.RewardCoins(1000), () => { Coins.MoveCoins(1000); });
                Show.Build.Reward(LaungeSystem.RewardExp(52), () => { LvlManager.Static.GetExp(52); });


            }, () =>
            {
                Clicker.NeedApgrateClass();
                Show.Loc.JumpLoc(Location.Bar);
            });
        }
        else if (MissionManager.CrntStep(Mission.КулычныеБои, 3))
        {
            Show.Build.clicker(Clicker.EnemyEnum.БлестящийСуперсплав, () =>
            {
                Show.Build.Reward(LaungeSystem.Word("NewEnemy"), () =>
                {
                    SS.sv.Player.StatResident -= 0.05f;
                    SS.sv.Player.StatAgress -= 0.085f;
                    MissionManager.NextStep(Mission.КулычныеБои);
                    Show.Loc.JumpLoc(Location.Bar);
                });
                Show.Build.Reward(LaungeSystem.RewardCoins(1500), () => { Coins.MoveCoins(1500); });
                Show.Build.Reward(LaungeSystem.RewardExp(68), () => { LvlManager.Static.GetExp(68); });
            }, () =>
            {
                Clicker.NeedApgrateClass();
                Show.Loc.JumpLoc(Location.Bar);
            });
        }
        else if (MissionManager.CrntStep(Mission.КулычныеБои, 4))
        {
            Show.Build.clicker(Clicker.EnemyEnum.ГомоГомоЗек, () =>
            {
                
                Show.Build.Reward(LaungeSystem.RewardCoins(2500), () => 
                {
                    Coins.MoveCoins(2500);
                    SS.sv.Player.StatResident -= 0.05f;
                    SS.sv.Player.StatAgress -= 0.085f;
                    MissionManager.NextStep(Mission.КулычныеБои);
                    Show.Loc.JumpLoc(Location.Bar);
                });
                Show.Build.Reward(LaungeSystem.RewardExp(108), () => { LvlManager.Static.GetExp(108); });
            }, () =>
            {
                Clicker.NeedApgrateClass();
                Show.Loc.JumpLoc(Location.Bar);
            });
        }
    }
}
