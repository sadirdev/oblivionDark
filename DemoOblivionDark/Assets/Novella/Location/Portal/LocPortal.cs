using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class LocPortal : MonoBehaviour
{
    [SerializeField] private Image _portalBttn;
    [SerializeField] private Color _endColor;
    [SerializeField] private float _durationBlink;
    private TweenerCore<Color, Color, ColorOptions> _animPortal;

    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        if (MissionManager.CrntStep(Mission.Kep_6lvl_ВирусПриставка, 3) || MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен,6) || MissionManager.IsComplite(Mission.Kep_7lvl_ГеносМумен))
        {
            SS.sv.ActivePortal = true;
        }
        if (!SS.sv.ActivePortal)
        {
            _portalBttn.gameObject.SetActive(false);
        }

    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }

    public void ClickPortal()
    {
        if(MissionManager.CrntStep(Mission.Kep_6lvl_ВирусПриставка,4))
        {
             Dialog.Notification(LaungeSystem.Word("KeplerNeed6lvl"));
            return;
        }
        else if(MissionManager.CrntStep(Mission.Kep_7lvl_ГеносМумен,6) || MissionManager.IsComplite(Mission.Kep_7lvl_ГеносМумен))
        {
            MainAudio.Static.PlayStart();
            if(SS.sv.BitaVirus && SS.sv.BitaGenos)
            {
                Dialog.BuildWithDic("FinishGood", FinishJump);
            }
            else Dialog.BuildWithDic("FinishBad", FinishJump);
            return;
        }

        if(!SS.sv.ComliteEnemy.Contains(Clicker.EnemyEnum.EnemyPortal))
        {
            Show.Build.clicker(Clicker.EnemyEnum.EnemyPortal);
            _animPortal.Kill();
            return;
        }
        if(SS.sv.ActivePortal)
        {
            _portalBttn.transform.GetChild(0).GetComponent<Button>().enabled = false;

            GetComponent<AudioSource>().Play();
            MissionManager.GetPlayer(Player.Virus);

            SS.sv.ActivePortal = false;
            Show.Loc.JumpLoc(SS.sv.CrntLoc, true);
            MainAudio.Static.PlayFon();
            return;
            
        }
        
       
        
    }


    private void FinishJump()
    {
        Interface.AllHide();
        Show.Loc.JumpLoc(Location.Laboratory);
        SS.sv.ActiveIconPlayer = false;
        SS.sv.ActiveInterface = false;
    }
    


    private void AmimPortal()
    {
        if (_animPortal != null && _animPortal.active) return;
        _animPortal = _portalBttn.DOColor(_endColor, _durationBlink).SetLoops(-1, LoopType.Yoyo).OnKill(() =>
        {
            _portalBttn.DOColor(Color.white, 0.5f);
        });
    }
    
    private void Jump()
    {
        if(MissionManager.CrntStep(Mission.Kep_6lvl_ВирусПриставка,3))
        {
            if(!Dialog.IsComplite("ПорталСноваОткрыт"))
            {
                
                Dialog.BuildWithDic("ПорталСноваОткрыт", () => 
                {
                    MissionManager.NextStep(Mission.Kep_6lvl_ВирусПриставка);
                    SS.sv.Player.StatAgress -= 0.1f;
                    Show.Build.Reward(LaungeSystem.Word("NewBlock"), null);
                    Show.Build.Reward(LaungeSystem.RewardExp(150), () => { LvlManager.Static.GetExp(150); });

                });
            }
        }


        if (!Dialog.IsComplite("portalEnemy"))
        {
            
            Dialog.Build("portalEnemy");
            SS.sv.ActiveIconPlayer = true;
            return;
        }
        


        if (SS.sv.ActivePortal)
        {
            AmimPortal();
        }
        else if(_portalBttn.gameObject.activeSelf)
        {
            _portalBttn.enabled = false;
            _portalBttn.DOColor(Color.clear, DialogFon.DurationFade).OnComplete(() =>
            {
                _portalBttn.gameObject.SetActive(false);
            });
        }

        if(SS.sv.Player.Name == Player.Virus)
        {
            

            if(!Dialog.IsComplite("portalVirus"))
            {
                StartSpetipicationsGenos(SS.sv.Player);
                Dialog.BuildWithDic("portalVirus",()=>
                {
                    SS.sv.ActiveInterface = true;
                    FindObjectOfType<Interface>().Start();
                    MissionManager.Add(Mission.LostIdentity);
                    Interface.ButtonShow();
                });
            }
            
        }
        else //Genos
        {

           
            if (!Dialog.IsComplite("portalGenosSaitama") && ST.sv.LvlKepler >= 2)
            {
                StartSpetipicationsGenos(SS.sv.Player);

                

                Dialog.BuildYesNo("portalGenosSaitama", () =>
                 {
                     MissionManager.Complite(Mission.Kepler_2lvl);
                     MissionManager.Add(Mission.Kep_3lvl_DarkHero);
                     Show.Build.clicker(Clicker.EnemyEnum.Sonic);
                 }, ()=>
                 {
                     MissionManager.Complite(Mission.Kepler_2lvl);
                     MissionManager.Add(Mission.Kep_3lvl_DarkHero);
                 });


            }
                
            
        }
    }

    private void StartSpetipicationsGenos(ClickerIconPlayer.Specifications player)
    {
        if(player.Name == Player.Genos)
        {
            player.StatHealth = 0.18f;
            player.StatDamage = 0.13f;
            player.StatAgress = 0.04f;
            player.StatResident = 0.0f;
            player.StatSaitama = 0.03f;

        }
        else
        {
            player.StatHealth = 0.13f;
            player.StatDamage = 0.18f;
            player.StatAgress = 0.98f;
            player.StatResident = 0.95f;
            player.StatSaitama = 0.8f;
        }
        
    }
    
}
