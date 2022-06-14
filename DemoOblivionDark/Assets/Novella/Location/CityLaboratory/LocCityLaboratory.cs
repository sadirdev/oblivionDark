using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LocCityLaboratory : MonoBehaviour
{

    [SerializeField] private Image _watchDog;
    [SerializeField] private Image _watchDogBase;
    private void Start()
    {
        _watchDog.color = new Color(1, 1, 1, 0);
        _watchDogBase.color = new Color(1, 1, 1, 0);
        if (SS.sv.Player.Name == Player.Genos)
        {
            Show.FadeImg(_watchDog);
            Show.FadeImg(_watchDogBase);
        }
        
        
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

    public void Click()
    {
        if(SS.sv.Player.Name == Player.Genos)
        {
            if(Inventory.Contains("ПропускКарта"))
            {
                MissionManager.Complite(Mission.ПопастьВЛабораторию);
                Show.Loc.JumpLoc(Location.Laboratory);
                return;
            }
            
            Show.FadeImg(_watchDog);
            Dialog.BuildWithDic("СторожевойПёсУходи",()=> { Show.FadeImg(_watchDog); });
            MissionManager.Add(Mission.ПопастьВЛабораторию);
        }
        else
        {
            Show.Loc.JumpLoc(Location.Laboratory);
        }

        
    }


}
