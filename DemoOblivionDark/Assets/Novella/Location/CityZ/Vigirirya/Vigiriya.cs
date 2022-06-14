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
        if(MissionManager.CrntStep(Mission.Kep_5lvl_√енос—пасиƒев,0) && !Dialog.IsComplite("—айтама√ражданский"))
        {
            Dialog.Build("—айтама√ражданский");
        }
    }
    public void ClickLaundry()
    {
        if (Inventory.Contains("√р€зныйѕлащ") && Inventory.Contains("ѕорошок"))
        {
            Dialog.Notification("√р€зные вещи отстираны. ѕолучен предмет: чистый плащ —айтамы.");
            Inventory.Remove("√р€зныйѕлащ");
            Inventory.Add("ѕлащ—айтамы");
            Inventory.Remove("ѕорошок");
        }
        else if(Inventory.Contains("√р€зныйѕлащ") && !Inventory.Contains("ѕорошок"))
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
