using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kagiyar : MonoBehaviour
{
    [SerializeField] private GameObject _home;
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        ActiveHome();
    }

    private void ActiveHome()
    {
        if (SS.sv.Player.Name == Player.Genos && !MissionManager.IsComplite(Mission.��������������) && !MissionManager.CrntStep(Mission.��������������, 0)) _home.SetActive(true);
        else _home.SetActive(false); 
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }
    private void Jump()
    {
        ActiveHome();
    }

    public void ClickHome()
    {
        if (!Dialog.IsComplite("��������������������"))
        {
            Dialog.BuildWithDic("��������������������", () =>
            {
                MissionManager.Add(Mission.��������������);
            });
        }
        else if (MissionManager.CrntStep(Mission.��������������, 1))
        {
            Dialog.BuildWithDic("�������������������", () =>
            {
                Show.Build.Reward(LaungeSystem.RewardCoins(1000), () => 
                { 
                    Coins.MoveCoins(1000);
                    SS.sv.Player.StatResident += 0.2f;
                    MissionManager.Complite(Mission.��������������);
                });
                
                Show.Build.Reward(LaungeSystem.RewardExp(30), () => { LvlManager.Static.GetExp(30); });
            });
        }
    }
    public void JumpExit()
    {
        Show.Loc.JumpLoc(Location.Cave);
    }
}
