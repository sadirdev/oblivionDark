using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class PanelMenuBall : MonoBehaviour
{
    [HideInInspector] public MenuBall mb;
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnResume;
    [SerializeField] private Button _btnRestart;
    [SerializeField] private Button _btnExit;
    [SerializeField] private Button _btnNext;
    [SerializeField] private GameObject _addExp;
    [SerializeField] private Text _expCound;
    

    [SerializeField] private Transform _contentLvl;
    [SerializeField] private Transform _contentBttn;
    [SerializeField] private Image _namber;


  


    void Start()
    {
        EdMob.HideBanner();
        _expCound.text = ST.sv.ExpT360.ToString();
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        LvlGenerate();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);

        if (mb.PanelType == MenuBall.BuildPanelType.Play)
        {
            Instantiate(_btnPlay, _contentBttn).onClick.AddListener(() => PlayClick());
        }
            
        else if(mb.PanelType == MenuBall.BuildPanelType.Restart)
        {
            if(ResumeButton.ShowAds)Instantiate(_btnResume, _contentBttn).onClick.AddListener(()=> ResumeClick());
            Instantiate(_btnRestart, _contentBttn).onClick.AddListener(()=> RestartClick());
        }
        else if(mb.PanelType == MenuBall.BuildPanelType.Next)
        {
            CompleteteLvl();
            Instantiate(_btnNext, _contentBttn).onClick.AddListener(() => PlayClick());
            _addExp.SetActive(true);
        }
        Instantiate(_btnExit, _contentBttn);
    }

    void CompleteteLvl()
    {
        mb.PanelType = MenuBall.BuildPanelType.Next;
        ST.sv.LvlTowerView++;

        //ST.sv.NumberLvl++;
    }
    
    private void RestartClick()
    {
        Destroy(FindObjectOfType<PlatformGame>().gameObject);
        Destroy(FindObjectOfType<Tank>().gameObject);
        PlayClick();
    }

    private void PlayClick()
    {
        mb.Play();
    }
    private void ResumeClick()
    {
        if (!EdMob.ShowAdsFull()) return;

        if (ST.sv.LvlKepler > 1) EdMob.ShowBanner();
        ResumeButton.ShowAds = false;
        mb.Resume();
    }
    public void LvlGenerate()
    {
        foreach (Transform item in _contentLvl)
        {
            Destroy(item.gameObject);
        }
        string lvlString = ST.sv.LvlTowerView.ToString();

        if(lvlString.Length == 1)
        {
            Image namber = Instantiate(_namber, _contentLvl);
            namber.sprite = mb.Nambers[0];
            namber.SetNativeSize();
        }

        foreach (var item in lvlString)
        {
            Image namber = Instantiate(_namber, _contentLvl);
            namber.sprite = namberSprite(item);
            namber.SetNativeSize();
        }

        Sprite namberSprite(char namber)
        {
            switch (namber)
            {
                case '1': return mb.Nambers[1];
                case '2': return mb.Nambers[2];
                case '3': return mb.Nambers[3];
                case '4': return mb.Nambers[4];
                case '5': return mb.Nambers[5];
                case '6': return mb.Nambers[6];
                case '7': return mb.Nambers[7];
                case '8': return mb.Nambers[8];
                case '9': return mb.Nambers[9];
                default: return mb.Nambers[0];
            }
        }

    }
}
