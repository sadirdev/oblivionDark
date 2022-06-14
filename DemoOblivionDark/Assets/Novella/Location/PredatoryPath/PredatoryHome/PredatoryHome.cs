using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatoryHome : MonoBehaviour
{
    [SerializeField] private GameObject _infoPazzle;
    [SerializeField] private GameObject _chest;
    int _trueChoice;
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        if (MissionManager.IsComplite(Mission.Ќепокорный—ундук)) _chest.SetActive(false);
        else _chest.SetActive(true);
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
        if (MissionManager.IsComplite(Mission.Ќепокорный—ундук)) _chest.SetActive(false);
        else _chest.SetActive(true);
        if (!Dialog.IsComplite("—амурай∆алуетс€"))
        {
            LocMngr.NoJump = true;
            Dialog.BuildYesNo("—амурай∆алуетс€", () =>
            {
                MissionManager.Add(Mission.Ќепокорный—ундук);
            },()=>
            {
                Show.Loc.JumpLoc(Location.PradatoryPath);
            });


        }
    }

    public void ClickBox()
    {
        Instantiate(_infoPazzle, FindObjectOfType<Build>().transform);
    }
    public void ClickChest()
    {
        _trueChoice = 0;
        Chest();
    }
    private void Chest()
    {
        Dialog.Build_5("—ундук", () => // 1
        {
            Debug.Log("1");
            if (_trueChoice == 0 || _trueChoice == 1 || _trueChoice == 2) _trueChoice++;
            else _trueChoice = 100;
            Chest();
        }, () =>// 2
        {
            Debug.Log("2");
            if (_trueChoice == 4 || _trueChoice == 5 || _trueChoice == 6 || _trueChoice == 7) _trueChoice++;
            else _trueChoice = 100;
            Chest();

        }, () =>// 3
        {
            Debug.Log("3");
            if (_trueChoice == 3) _trueChoice++;
            else _trueChoice = 100;
            Chest();
        }, () =>// 4
        {
            Debug.Log("4");
            _trueChoice = 0;
            Chest();
        }, () =>// 5
        {
            Debug.Log("5");
            if (_trueChoice == 8)
            {
                Dialog.BuildWithDic("—ундукќткрыт", () =>
                {
                    MissionManager.Complite(Mission.Ќепокорный—ундук);
                    SS.sv.BitaVirus = true;
                    Show.Build.Reward(LaungeSystem.Word("–епутаци€—амура€"), null);
                    Show.Build.Reward(LaungeSystem.RewardCoins(1000), () => { Coins.MoveCoins(1000); });
                });
            }
            else Dialog.Notification(LaungeSystem.Word("—ундук«акрыт"));
        });
    }
    public void JumpExit()
    {
        Show.Loc.JumpLoc(Location.PradatoryPath);
    }
}
