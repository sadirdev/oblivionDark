using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HighwayA : MonoBehaviour
{
    [SerializeField] private GameObject _mumen;
    [SerializeField] private Image _mumenBttn;
    [SerializeField] private Sprite _mumenDrive;
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        if (SS.sv.Player.Name == Player.Genos) _mumen.SetActive(true);
        else _mumen.SetActive(false);
        
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
        if (_mumenBttn.color.a != 1)
        {
            _mumenBttn.DOFade(1, DialogFon.DurationFade);
        }
    }

    public void ClickMumen()
    {
        _mumenBttn.DOFade(0, DialogFon.DurationFade);
        if(SS.sv.Player.Name == Player.Genos)
        {
            if (MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен, 3))
            {
                Dialog.BuildWithDic("ПоехалиУже", () => { MumenDrive(); SS.sv.Player.StatResident += 0.1f; });
                return;
            }
            else if (MissionManager.IsComplite(Mission.Kep_7lvl_ГеносМумен) || MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен, 4) || MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен, 5) || MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен, 6))
            {
                Dialog.BuildWithDic("ПереездГенос", () =>
                {
                    if(SS.sv.Player.Coins <100)
                    {
                        Dialog.Build("УВасНедостаточноДенег");
                    }
                    else
                    {
                        MumenDrive();
                        Coins.MoveCoins(-100);
                    }
                        
                });
            }
            else
            {
                Dialog.Build("ВалиДикарь");
            }
        }
        else
        {
            Dialog.BuildWithDic("ПереездВирус", () =>
            {
                if (SS.sv.Player.Coins < 100)
                {
                    Dialog.Build("УВасНедостаточноДенег");
                }
                else
                {
                    MumenDrive();
                    Coins.MoveCoins(-100);
                }
            });
        }
        
        
    }

    private void MumenDrive()
    {
        LocMngr.FadeJump(_mumenDrive);
        StartCoroutine(delay());
        IEnumerator delay()
        {
            Interface.AllHide();
            yield return new WaitForSeconds(3);
            
            Show.Loc.JumpLoc(Location.HighwayB);
            yield return new WaitForSeconds(DialogFon.DurationFade);
            Interface.AllShow();

        }
    }
    public void JumpEtale()
    {
        Show.Loc.JumpLoc(Location.Etale);
    }
    
}
