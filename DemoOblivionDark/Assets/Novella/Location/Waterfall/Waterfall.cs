using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
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
        if(MissionManager.CrntStep(Mission.Kep_3lvl_DarkHero,0))
        {
            MissionManager.NextStep(Mission.Kep_3lvl_DarkHero);
        }
    }

    public void Fishing()
    {
        //Inventory.Add("аћср");
        Show.Build.SliderGame("аћср",1);


    }


}
