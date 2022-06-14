using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dump : MonoBehaviour
{
    [SerializeField] private GameObject _don;
    [SerializeField] private GameObject _dump;
    private void Start()
    {
        
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        ActiveDon();
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }
    private void ActiveDon()
    {
        if (SS.sv.DonActive && SS.sv.Player.Name == Player.Genos) _don.SetActive(true);
        else _don.SetActive(false);
        if (SS.sv.Player.Name == Player.Genos && (MissionManager.IsComplite(Mission.КомпасПирата) || Inventory.Contains("Компас"))) _dump.SetActive(false);
    }

    private void Jump()
    { 
        ActiveDon();
    }
    public void ClickDump()
    {
        Dialog.BuildWithDic("ОбыскатьМусор", () =>
        {
            Show.Build.SliderGame("Бачок",1);
        });
    }
    public void ClickDon()
    {
        Dialog.BuildWithDic("БандитБлагодарит", () =>
        {
            Show.Build.Reward(LaungeSystem.RewardCoins(600), () => { Coins.MoveCoins(600); });
            Show.Build.Reward(LaungeSystem.RewardExp(25), () => 
            {
                SS.sv.DonActive = false;
                LvlManager.Static.GetExp(25);
            }); 
        });
    }
}
