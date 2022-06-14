using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocLaboratory : MonoBehaviour
{
    [SerializeField] private GameObject _prefabT360;
    [SerializeField] private GameObject _shadowT360;
    [SerializeField] private GameObject _buttonT360;
    [SerializeField] private GameObject _bttnKuseno;

    private GameObject _T360;

    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());

        if(MissionManager.IsComplite(Mission.LostIdentity) || MissionManager.CrntStep(Mission.LostIdentity,2))
        {
            _T360 =  Instantiate(_prefabT360, GameObject.Find("Main Camera").transform);
            _shadowT360.SetActive(true);
        }
        if (SS.sv.Player.Name == Player.Genos) _bttnKuseno.SetActive(true);
        if (!SS.sv.ActiveInterface) _bttnKuseno.SetActive(false);// когда игра завершилась 



    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
        if(_T360 != null) Destroy(_T360);

    }


    private void Jump()
    {
        

        if (Dialog.IsComplite("ПрезинтацияТ360") && !MissionManager.IsComplite(Mission.LostIdentity))
        {

            SS.sv.Player.StatSaitama -= 0.35f;
            
            MissionManager.Complite(Mission.LostIdentity);
            Show.Build.Reward(LaungeSystem.Word("NewBlock"), null);
            Show.Build.Reward(LaungeSystem.RewardExp(80), () => { LvlManager.Static.GetExp(80); });
            MissionManager.Add(Mission.Kepler_2lvl);
        }

        if (MissionManager.IsComplite(Mission.LostIdentity))
        {
            _bttnKuseno.SetActive(false); 
            _buttonT360.SetActive(true); 
            if (SS.sv.Player.Name == Player.Genos && SS.sv.ActiveInterface) _bttnKuseno.SetActive(true);
           
        }
        else _bttnKuseno.SetActive(true);
        

        if (Dialog.IsComplite("kusenoVirusSaitama1") && MissionManager.CrntStep(Mission.LostIdentity,0))
        {
            
            SS.sv.Player.StatSaitama -= 0.3f;//Virus
            SS.sv.Player.StatResident -= 0.1f;
            MissionManager.NextStep(Mission.LostIdentity);
            return;
        }

        

    }

    
   


    public void Click()
    {
        if(SS.sv.Player.Name== Player.Virus)
        {
            if (!Dialog.IsComplite("kusenoVirusSaitama1"))
            {
                Dialog.Build("kusenoVirusSaitama1");
                return;
            }
            if (MissionManager.CrntStep(Mission.LostIdentity, 1)) Dialog.Build("ЕщёНеГотовКусено");
            if (MissionManager.CrntStep(Mission.LostIdentity, 2)) Dialog.Build("ПрезинтацияТ360");
        }
        else
        {
            if (MissionManager.CrntStep(Mission.КомпасПирата, 0))
            {
                Dialog.BuildWithDic("ПоказатьКомпас", () =>
                {
                    MissionManager.NextStep(Mission.КомпасПирата);
                });
            }
            else
            {
                Dialog.Build("НекогдаГенус");
            }
        }
       

    }
   
   
}
