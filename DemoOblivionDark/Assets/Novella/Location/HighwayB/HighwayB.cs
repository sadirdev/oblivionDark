using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HighwayB : MonoBehaviour
{
    [SerializeField] private Sprite _mumenDrive;
    [SerializeField] private Image _mumenBttn;
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        if(SS.sv.Player.Name == Player.Genos && !Dialog.IsComplite("�����������������������"))
        {
            _mumenBttn.color = new Color(1, 1, 1, 0);
        }
        else
        {
            _mumenBttn.color = Color.white;
        }
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
        if(SS.sv.Player.Name == Player.Genos && !Dialog.IsComplite("�����������������������"))
        {
            Dialog.BuildWithDic("�����������������������", () =>
            {
                _mumenBttn.DOFade(1, DialogFon.DurationFade);
            });
            MissionManager.NextStep(Mission.Kep_7lvl_����������);
        }
    }
    public void ClickMumen()
    {
        MumenDrive();
    }
    public void ClickCroaville()
    {
        Show.Loc.JumpLoc(Location.Croaville);
    }

    private void MumenDrive()
    {
        LocMngr.FadeJump(_mumenDrive);
        StartCoroutine(delay());
        IEnumerator delay()
        {
            Interface.AllHide();
            yield return new WaitForSeconds(3);
            Show.Loc.JumpLoc(Location.HighwayA);
            yield return new WaitForSeconds(DialogFon.DurationFade);
            Interface.AllShow();
        }
    }
}
