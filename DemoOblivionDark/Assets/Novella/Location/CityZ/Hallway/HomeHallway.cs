using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeHallway : MonoBehaviour
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

    }


    public void JumpBedroom()
    {
        Show.Loc.JumpLoc(Location.HomeBedroom);
    }
    public void JumpKitchen()
    {
        Show.Loc.JumpLoc(Location.HomeKitchen);
    }
    public void Exit()
    {
        Show.Loc.JumpLoc(Location.CityZ);
    }
    public void ClickComputer()
    {
        if(MissionManager.CrntStep(Mission.Kep_6lvl_��������������,0) || MissionManager.CrntStep(Mission.Kep_6lvl_��������������, 1) || MissionManager.CrntStep(Mission.Kep_6lvl_��������������, 2))
        {

            if(Inventory.Contains("�������") && Inventory.Contains("���������") && Inventory.ContainsCound("�����������",3))
            {
                Show.Build.SliderGame("���������", 3);
                //���� ���� �������, �� ����������� ������� �� ������ ����������� ������{��������������}
            }
            else
            {
                if (MissionManager.CrntStep(Mission.Kep_6lvl_��������������, 0))
                {
                    Dialog.Notification(LaungeSystem.Word("ScreenNoPower"));
                }
                else
                {
                    Dialog.NotificationWithDic(LaungeSystem.Word("RepairMonitor"), () =>
                    {
                        MissionManager.SetStep(Mission.Kep_6lvl_��������������, 2);
                    });
                }

                
            }

            return;
            
        }


        if (SS.sv.Player.Name == Player.Genos)
        {
            if(!Dialog.IsComplite("��������������1"))
            {
                Dialog.Build("��������������1"); return;
            }
        }
        else
        {
            if (!MissionManager.Cointains(Mission.�����������))
            {
                Dialog.BuildWithDic("��������������1", () =>
                 {
                     MissionManager.Add(Mission.�����������);
                 });
                return;
            }
        }

        Dialog.Build("���������������");
    }
}
