using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HomeBedroom : MonoBehaviour
{
    [SerializeField] private GameObject _jumpKitchen;
    [SerializeField] private GameObject _sleep;
    [SerializeField] private GameObject _hallway;
    [SerializeField] private Sprite _hightSprite;
    [SerializeField] private GameObject _clear;

    private Sprite _fonBase;

    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        if (MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен, 1))
        {
            _sleep.SetActive(false);
            _clear.SetActive(true);
        }
        else
        {
            _sleep.SetActive(true);
            _clear.SetActive(false);
        }
        
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
        if (MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен, 1))
        {
            _sleep.SetActive(false);
            _clear.SetActive(true);
        }
    }
    public void Sleep()
    {
        Interface.AllHide();
        MainAudio.Static.AudioSource.DOFade(0.25f, 2).SetLoops(2, LoopType.Yoyo);
        _jumpKitchen.SetActive(false);
        _sleep.SetActive(false);
        _hallway.SetActive(false);
        Image fon = GetComponent<Image>();
        _fonBase = fon.sprite;
        fon.DOColor(Color.black, DialogFon.DurationFade*3).OnComplete(() =>
        {
            fon.sprite = _hightSprite;
            fon.DOColor(Color.white, DialogFon.DurationFade*3).OnComplete(() =>
            {
                fon.DOColor(Color.black, DialogFon.DurationFade*3).OnComplete(() =>
                {
                    fon.sprite = _fonBase;
                    if (MissionManager.CrntStep(Mission.LostIdentity, 1))
                    {
                        MissionManager.NextStep(Mission.LostIdentity);
                    }
                    else if (MissionManager.CrntStep(Mission.Kep_5lvl_ГеносСпасиДев,1))
                    {
                        MissionManager.NextStep(Mission.Kep_5lvl_ГеносСпасиДев);
                    }
                    ClickerIconPlayer.GetHP(-0.15f);

                    fon.DOColor(Color.white, DialogFon.DurationFade*3).OnComplete(() =>
                    {
                        Interface.AllShow();
                        _jumpKitchen.SetActive(true);
                        _sleep.SetActive(true);
                        _hallway.SetActive(true);
                    });
                });
            });
        });
        
    }
    public void Clear()
    {
        if(Inventory.Contains("РукиДомашнихЗабот")) 
        {
            Show.Build.SliderGame("Уборка",2);
        }
        else
        {
            Dialog.Notification(LaungeSystem.Word("NeedHands"));
        }
        
        _clear.SetActive(false);
    }
    public void JumpKitchen()
    {
        Show.Loc.JumpLoc(Location.HomeKitchen);
    }
    public void JumpHallway()
    {
        Show.Loc.JumpLoc(Location.HomeHallway);
    }

    public void Exit()
    {
        Show.Loc.JumpLoc(Location.CityZ);
    }


}
