using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{

    [SerializeField] private GameObject _caveBttn;
    [SerializeField] private GameObject _spiderBttn;
    private void Start()
    {
        //if (MissionManager.CrntStep(Mission.ЛютыйГолод, 0)) _caveBttn.SetActive(false);

        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        ActiveBttn();
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }

    private void ActiveBttn()
    {
        _caveBttn.SetActive(true);
        if (SS.sv.Player.Name == Player.Genos)
        {
            if (MissionManager.IsComplite(Mission.ЛютыйГолод) || MissionManager.CrntStep(Mission.ЛютыйГолод, 2)) _caveBttn.SetActive(false);
            if (MissionManager.CrntStep(Mission.КоролеваПауков, 0)) _spiderBttn.SetActive(true);
        }
    }
    private void Jump()
    {
        ActiveBttn();


    }

    public void ClickCave()
    {
        _caveBttn.SetActive(false);
        if (SS.sv.Player.Name == Player.Genos)
        {
            if(!Dialog.IsComplite("Голод1"))
            {
                Dialog.BuildWithDic("Голод1", () =>
                {
                    MissionManager.Add(Mission.ЛютыйГолод);
                });
                return;
            }
            if(MissionManager.CrntStep(Mission.ЛютыйГолод,0))
            {
                if (Inventory.Contains("Рыба"))
                {
                    Dialog.BuildWithDic("Голод2ЕстьРыба", () =>
                    {
                        MissionManager.NextStep(Mission.ЛютыйГолод);
                        Inventory.Remove("Рыба");
                        SS.sv.Player.StatResident += 0.1f;
                    });
                }
                else
                {
                    Dialog.Build("Голод2НетРыбы");
                }
            }
            else if(MissionManager.CrntStep(Mission.ЛютыйГолод, 1))
            {
                if(Inventory.Contains("Рак"))
                {
                    Dialog.BuildWithDic("ЕстьРак", () =>
                    {
                        MissionManager.NextStep(Mission.ЛютыйГолод);
                        Inventory.Add("КлючЗункоти");
                        Inventory.Remove("Рак");
                        SS.sv.Player.StatResident += 0.05f;
                    });
                }
                else
                {
                    Dialog.Build("НетРак");
                }
            }
           
           



        }
        else
        {
            Dialog.Notification(LaungeSystem.Word("NowWay"));
        }
        
    }
    public void ClickAngiyar()
    {
        Show.Loc.JumpLoc(Location.Kagiyar);
    }
    public void ClickSpider()
    {
        Show.Build.clicker(Clicker.EnemyEnum.Паучиха, () =>
        {
            Dialog.Notification(LaungeSystem.Word("KillSpider"));
            MissionManager.NextStep(Mission.КоролеваПауков);
            _spiderBttn.SetActive(false);
        });
    }
}
