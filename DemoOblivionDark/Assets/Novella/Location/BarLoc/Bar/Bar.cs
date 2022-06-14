using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private GameObject _barman;
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        if (MissionManager.CrntStep(Mission.������������������, 2)) _barman.SetActive(true);
        else
        {
            if(SS.sv.Player.Name== Player.Genos) _barman.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
        if (MissionManager.CrntStep(Mission.������������������, 2)) _barman.SetActive(true);
        else
        {
            if (SS.sv.Player.Name == Player.Genos) _barman.SetActive(false);
        }
    }
    public void ClickBarman()
    {
        if(SS.sv.Player.Name == Player.Genos)
        {
            Dialog.BuildWithDic("�����������", () => { MissionManager.NextStep(Mission.������������������); });
        }
        else
        {
            MissionManager.Add(Mission.�����������);
            if (MissionManager.CrntStep(Mission.�����������, 0) || MissionManager.CrntStep(Mission.�����������, 1))
            {
                
                if (!Dialog.IsComplite("�����������������"))
                {
                    Dialog.BuildWithDic("�����������������", () => { MissionManager.SetStep(Mission.�����������, 1); Show.Loc.JumpLoc(Location.BarFight); });
                }
                else Dialog.BuildWithDic("�����������1", () => { Show.Loc.JumpLoc(Location.BarFight); });
            }
            else if (MissionManager.CrntStep(Mission.�����������, 2))
            {
                Dialog.BuildWithDic("�����������2", () => { Show.Loc.JumpLoc(Location.BarFight); });
            }
            else if (MissionManager.CrntStep(Mission.�����������, 3))
            {
                Dialog.BuildWithDic("�����������3", () => { Show.Loc.JumpLoc(Location.BarFight); });
            }
            else if (MissionManager.CrntStep(Mission.�����������, 4))
            {
                Dialog.BuildWithDic("�����������4", () => { Show.Loc.JumpLoc(Location.BarFight); });
            }
        }
        
        
    }
    public void ClickExit()
    {
        Show.Loc.JumpLoc(Location.BarLoc);
    }
}
