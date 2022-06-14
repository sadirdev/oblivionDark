using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class North : MonoBehaviour
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

    public void ClickRock()
    {
        Dialog.BuildWithDic("ДобытьКремний", () => { Show.Build.SliderGame("Кремний",3); });

    }
    public void ClickCastle()
    {
        if (SS.sv.Player.Name == Player.Genos && SS.sv.BitaGenos == false)
        {
            Dialog.BuildWithDic("КинутьКлинок", () =>
            {
                if (!Inventory.Contains("Катана"))
                {
                    Dialog.Notification(LaungeSystem.Word("NotKatana"));
                }
                else
                {
                    SS.sv.BitaGenos = true;
                    Inventory.Remove("Катана");
                    Dialog.Build("КинулКлинок");
                    SS.sv.Player.StatResident += 0.2f;
                    SS.sv.Player.StatSaitama += 0.2f;
                }

            });
        }
        else
        {
            Dialog.Notification(LaungeSystem.Word("GateClosed"));
        }
    }
    
}
