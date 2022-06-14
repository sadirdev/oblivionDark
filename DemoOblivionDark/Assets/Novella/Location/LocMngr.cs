using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LocMngr : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private GameObject _prefabCanvasFade;
    public delegate void DelegateVoidSprite(Sprite loc);

    [SerializeField] private GameObject _locNull;
    [Header("Локации")]
    [SerializeField] private LocPortal _portal;
    [SerializeField] private LocCityLaboratory _cityLaboratory;
    [SerializeField] private LocLaboratory _laboratory;
    [SerializeField] private LocStart _start;
    [SerializeField] private CityZ _homeCity;
    [SerializeField] private HomeBedroom _homeBedroom;
    [SerializeField] private HomeKitchen _homeKitchen;
    [SerializeField] private Waterfall _waterfall;
    [SerializeField] private HomeHallway _homeHallway;
    [SerializeField] private Vigiriya _vigiriya;
    [SerializeField] private Cave _cave;
    [SerializeField] private PredatoryPath _pradatoryPath;
    [SerializeField] private Etale _cityZ;
    [SerializeField] private Supermarket _supermarket;
    [SerializeField] private North _north;
    [SerializeField] private Dump _dump;
    [SerializeField] private HighwayA _highwayA;
    [SerializeField] private HighwayB _highwayB;
    [SerializeField] private Croaville _croaville;
    [SerializeField] private Bar _bar;
    [SerializeField] private BarLoc _barLoc;
    [SerializeField] private BarFight _barFight;
    [SerializeField] private PredatoryHome _predatoryHome;
    [SerializeField] private Shinava _shinava;
    [SerializeField] private Kagiyar _kagiyar;
    //public static bool NotUpdateLoc = true;

    public static UnityAction LocUpdate;
    public static DelegateVoidSprite FadeJump;
    public static DelegateVoid RemoveLoc;
    public static bool NoJump = false;
    public static Location BuferLoc;
    private bool _fade = false;
    private int _childCound;
    

    private List<Button> _buttonsActive = new List<Button>();
    private void Awake()
    {
        Show.Loc = this;
        FadeJump = FadeJumpVoid;
        RemoveLoc = RemoveLocVoid;
    }
    private void Start()
    {
        JumpLoc(SS.sv.CrntLoc);
    }
    private void RemoveLocVoid()
    {
        if(transform.childCount>1)
        {
            for (int i =1; i< transform.childCount;i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
    private void FadeJumpVoid(Sprite sprite)
    {
        StartCoroutine(InstLoc());
        Instantiate(_prefabCanvasFade, _mainCamera);
        IEnumerator InstLoc()
        {
            yield return new WaitForSeconds(DialogFon.DurationFade);
            var loc = Instantiate(_locNull, transform);
            loc.GetComponent<Image>().sprite = sprite;
        }
        
    }
    public void JumpLoc(Location loc)
    {
        if(NameLocPanel.Close!= null) NameLocPanel.Close();
        if (SS.sv.CrntLoc == loc && transform.childCount >0 && !_fade)
        {
            StartCoroutine(locUpdate(DialogFon.DurationFade ));

            _fade = false;
            return;
        }
        if (transform.childCount == 0)
        {
            buildLoc();
        }
        else
        {
            Instantiate(_prefabCanvasFade, _mainCamera); // Там уже есть скрипт с Fade
            _childCound = transform.childCount;
            
            foreach (Transform item in transform)
            {
                
                StartCoroutine(DestroyLoc(item.gameObject));
            }
        }
        
        IEnumerator DestroyLoc(GameObject locObj)
        {
            yield return new WaitForSeconds(DialogFon.DurationFade); 

            Destroy(locObj);
            _childCound--;
            if (_childCound == 0)
            {
                buildLoc();
                Show.IconPlayer.Start();
            }
             
            

        }
        
        void buildLoc()
        {
            //Interface.NameLocOff();
            Image locImg;
            BuferLoc = SS.sv.CrntLoc;
            SS.sv.CrntLoc = loc;
            switch (loc)
            {
                case Location.CityLaboratory: locImg =  Instantiate(_cityLaboratory.GetComponent<Image>(), transform); break;
                case Location.Portal: locImg = Instantiate(_portal.GetComponent<Image>(), transform); break;
                case Location.Laboratory: locImg = Instantiate(_laboratory.GetComponent<Image>(), transform); break;
                case Location.CityZ: locImg = Instantiate(_homeCity.GetComponent<Image>(), transform); break;
                case Location.HomeBedroom: locImg = Instantiate(_homeBedroom.GetComponent<Image>(), transform); break;
                case Location.HomeKitchen: locImg = Instantiate(_homeKitchen.GetComponent<Image>(), transform); break;
                case Location.Waterfall: locImg = Instantiate(_waterfall.GetComponent<Image>(), transform); break;
                case Location.HomeHallway: locImg = Instantiate(_homeHallway.GetComponent<Image>(), transform); break;
                case Location.Vigiriya: locImg = Instantiate(_vigiriya.GetComponent<Image>(), transform); break;
                case Location.Cave: locImg = Instantiate(_cave.GetComponent<Image>(), transform); break;
                case Location.PradatoryPath: locImg = Instantiate(_pradatoryPath.GetComponent<Image>(), transform); break;
                case Location.Etale: locImg = Instantiate(_cityZ.GetComponent<Image>(), transform); break;
                case Location.SuperMarket: locImg = Instantiate(_supermarket.GetComponent<Image>(), transform); break;
                case Location.North: locImg = Instantiate(_north.GetComponent<Image>(), transform); break;
                case Location.Dump: locImg = Instantiate(_dump.GetComponent<Image>(), transform); break;
                case Location.HighwayA: locImg = Instantiate(_highwayA.GetComponent<Image>(), transform); break;
                case Location.HighwayB: locImg = Instantiate(_highwayB.GetComponent<Image>(), transform); break;
                case Location.Croaville: locImg = Instantiate(_croaville.GetComponent<Image>(), transform); break;
                case Location.BarLoc: locImg = Instantiate(_barLoc.GetComponent<Image>(), transform); break;
                case Location.Bar: locImg = Instantiate(_bar.GetComponent<Image>(), transform); break;
                case Location.BarFight: locImg = Instantiate(_barFight.GetComponent<Image>(), transform); break;
                case Location.PredatoryHome: locImg = Instantiate(_predatoryHome.GetComponent<Image>(), transform); break;
                case Location.Shinava: locImg = Instantiate(_shinava.GetComponent<Image>(), transform); break;
                case Location.Kagiyar: locImg = Instantiate(_kagiyar.GetComponent<Image>(), transform); break;
                default: locImg = Instantiate(_start.GetComponent<Image>(), transform); break;
            }
            
            _fade = false;
            //locImg.color = Color.black;
            // EnableButton();
            StartCoroutine(locUpdate(DialogFon.DurationFade));
            //locImg.DOColor(Color.white, DialogFon.DurationFade).OnComplete(() =>
            //{
            //    LocUpdate.Invoke();
            //});
            UpdateAspect( locImg);
        }
        IEnumerator locUpdate(float duration)
        {
            yield return new WaitForSeconds(duration);
            LocUpdate.Invoke();
        }
    }
    public void JumpLoc(Location loc, bool fade)
    {
        _fade = true;
        JumpLoc(loc);
    }
    private void UpdateAspect(Image locImg)
    {
        AspectRatioFitter q = locImg.GetComponent<AspectRatioFitter>();
        q.enabled = false;
        q.enabled = true;
    }
    private void DisableButton()
    {
        _buttonsActive.Clear();
        var allButtons = FindObjectsOfType<Button>();
        foreach (var bttn in allButtons)
        {
            if (bttn.enabled)
            {
                bttn.enabled = false;
                _buttonsActive.Add(bttn);
            }
        }
    }
    private void EnableButton()
    {
        foreach (var bttn in _buttonsActive)
        {
            if(bttn != null) bttn.enabled = true;

        }
    }
 
}

