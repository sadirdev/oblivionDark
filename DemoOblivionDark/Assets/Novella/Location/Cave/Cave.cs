using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{

    [SerializeField] private GameObject _caveBttn;
    [SerializeField] private GameObject _spiderBttn;
    private void Start()
    {
        //if (MissionManager.CrntStep(Mission.����������, 0)) _caveBttn.SetActive(false);

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
            if (MissionManager.IsComplite(Mission.����������) || MissionManager.CrntStep(Mission.����������, 2)) _caveBttn.SetActive(false);
            if (MissionManager.CrntStep(Mission.��������������, 0)) _spiderBttn.SetActive(true);
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
            if(!Dialog.IsComplite("�����1"))
            {
                Dialog.BuildWithDic("�����1", () =>
                {
                    MissionManager.Add(Mission.����������);
                });
                return;
            }
            if(MissionManager.CrntStep(Mission.����������,0))
            {
                if (Inventory.Contains("����"))
                {
                    Dialog.BuildWithDic("�����2��������", () =>
                    {
                        MissionManager.NextStep(Mission.����������);
                        Inventory.Remove("����");
                        SS.sv.Player.StatResident += 0.1f;
                    });
                }
                else
                {
                    Dialog.Build("�����2�������");
                }
            }
            else if(MissionManager.CrntStep(Mission.����������, 1))
            {
                if(Inventory.Contains("���"))
                {
                    Dialog.BuildWithDic("�������", () =>
                    {
                        MissionManager.NextStep(Mission.����������);
                        Inventory.Add("�����������");
                        Inventory.Remove("���");
                        SS.sv.Player.StatResident += 0.05f;
                    });
                }
                else
                {
                    Dialog.Build("������");
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
        Show.Build.clicker(Clicker.EnemyEnum.�������, () =>
        {
            Dialog.Notification(LaungeSystem.Word("KillSpider"));
            MissionManager.NextStep(Mission.��������������);
            _spiderBttn.SetActive(false);
        });
    }
}
