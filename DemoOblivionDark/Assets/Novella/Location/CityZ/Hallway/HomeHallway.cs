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
        if(MissionManager.CrntStep(Mission.Kep_6lvl_ВирусПриставка,0) || MissionManager.CrntStep(Mission.Kep_6lvl_ВирусПриставка, 1) || MissionManager.CrntStep(Mission.Kep_6lvl_ВирусПриставка, 2))
        {

            if(Inventory.Contains("Кремний") && Inventory.Contains("Текстолит") && Inventory.ContainsCound("Конденсатор",3))
            {
                Show.Build.SliderGame("Текстолит", 3);
                //если есть монитор, то запускается слайдер по финалу запускается диалог{МониторПочинил}
            }
            else
            {
                if (MissionManager.CrntStep(Mission.Kep_6lvl_ВирусПриставка, 0))
                {
                    Dialog.Notification(LaungeSystem.Word("ScreenNoPower"));
                }
                else
                {
                    Dialog.NotificationWithDic(LaungeSystem.Word("RepairMonitor"), () =>
                    {
                        MissionManager.SetStep(Mission.Kep_6lvl_ВирусПриставка, 2);
                    });
                }

                
            }

            return;
            
        }


        if (SS.sv.Player.Name == Player.Genos)
        {
            if(!Dialog.IsComplite("КомпьютерГенос1"))
            {
                Dialog.Build("КомпьютерГенос1"); return;
            }
        }
        else
        {
            if (!MissionManager.Cointains(Mission.КулычныеБои))
            {
                Dialog.BuildWithDic("КомпьютерВирус1", () =>
                 {
                     MissionManager.Add(Mission.КулычныеБои);
                 });
                return;
            }
        }

        Dialog.Build("НовогоНичегоНет");
    }
}
