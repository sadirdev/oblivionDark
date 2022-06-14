using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigiriya : MonoBehaviour
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
        if(MissionManager.CrntStep(Mission.Kep_5lvl_�������������,0) && !Dialog.IsComplite("������������������"))
        {
            Dialog.Build("������������������");
        }
    }
    public void ClickLaundry()
    {
        if (Inventory.Contains("�����������") && Inventory.Contains("�������"))
        {
            Dialog.Notification("������� ���� ���������. ������� �������: ������ ���� �������.");
            Inventory.Remove("�����������");
            Inventory.Add("�����������");
            Inventory.Remove("�������");
        }
        else if(Inventory.Contains("�����������") && !Inventory.Contains("�������"))
        {
            Dialog.Notification(LaungeSystem.Word("NoPowder"));
        }
        else Dialog.Notification(LaungeSystem.Word("NoDirtyShmot"));

    }
    public void ClickShop()
    {
        Show.Loc.JumpLoc(Location.SuperMarket);
    }
    public void JumpCityZ()
    {
        Show.Loc.JumpLoc(Location.CityZ);
    }
}
