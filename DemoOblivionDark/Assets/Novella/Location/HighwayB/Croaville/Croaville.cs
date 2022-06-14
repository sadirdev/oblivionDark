using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croaville : MonoBehaviour
{
    [SerializeField] private GameObject _home;
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
        _home.SetActive(false);
        if (MissionManager.CrntStep(Mission.Kep_7lvl_√еносћумен, 4))
        {
            Dialog.BuildWithDic(" инь амушком¬ќкно", () => {MissionManager.NextStep(Mission.Kep_7lvl_√еносћумен); SS.sv.Genos.StatSaitama += 0.1f; });
        }
        else if(MissionManager.CrntStep(Mission.Kep_7lvl_√еносћумен, 5))
        {
            _home.SetActive(true);
        }
    }

    public void ClickHighway()
    {
        Show.Loc.JumpLoc(Location.HighwayB);
    }
    public void ClickHome()
    {
        Show.Build.SliderGame(" амушек",4);
    }
}
