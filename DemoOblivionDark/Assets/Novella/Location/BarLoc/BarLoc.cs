using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLoc : MonoBehaviour
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

    public void ClickBar()
    {
        Show.Loc.JumpLoc(Location.Bar);
    }
}
