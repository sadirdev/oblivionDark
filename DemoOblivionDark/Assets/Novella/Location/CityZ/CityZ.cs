using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityZ : MonoBehaviour
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

    public void ClickHome()
    {
        if (MissionManager.CrntStep(Mission.Kep_3lvl_DarkHero, 0))
        {
            Dialog.Build("�����������������"); return;
        }
        else if (MissionManager.CrntStep(Mission.Kep_4lvl_���������, 2)) //����� ������� � ���������
        {
            Dialog.Notification(LaungeSystem.Word("CloseDoor")); return;
        }



        Show.Loc.JumpLoc(Location.HomeHallway);
    }
    public void ClickShop()
    {
        Show.Loc.JumpLoc(Location.CityZ);
        Show.Build.Shop(false);
    }
    public void ClickVigiriya()
    {
        Show.Loc.JumpLoc(Location.Vigiriya);
    }
}
