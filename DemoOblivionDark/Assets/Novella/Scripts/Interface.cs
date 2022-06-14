using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField] private Button _mission;
    [SerializeField] private Button _map;
    [SerializeField] private Button _inventory;
    [SerializeField] private Button _nameLoc;
    [SerializeField] private GameObject _iconPlayer;
    [SerializeField] private GameObject _nofifiMission;
    [SerializeField] private GameObject _repairAds;
    [SerializeField] private Button _buttonLvlUp;


    //private static Button _missionStatic;
    //private static Button _mapStatic;
    private static GameObject _nameLocStatic;
    private static GameObject _iconPlayerStatic;

    private static CanvasGroup _buttonGroup;
    private static CanvasGroup _allGroup;

    public static DelegateVoid AllHide;
    public static DelegateVoid AllShow;
    public static DelegateVoid ButtonHide;
    public static DelegateVoid ButtonShow;


    private void Awake()
    {

        AllHide = allHide;
        AllShow = allShow;
        ButtonHide = buttonHide;
        ButtonShow = buttonShow;

        _nameLocStatic = _nameLoc.gameObject;
        _iconPlayerStatic = _iconPlayer;
        _allGroup = GetComponent<CanvasGroup>();
        _buttonGroup = transform.GetChild(1).GetComponent<CanvasGroup>();
    }

    public void Start()
    {

        _mission.gameObject.SetActive(SS.sv.ActiveInterface);
        
        _map.gameObject.SetActive(SS.sv.ActiveInterface);
        _inventory.gameObject.SetActive(SS.sv.ActiveInterface);
        _iconPlayerStatic.SetActive(SS.sv.ActiveIconPlayer);
        _nofifiMission.SetActive(SS.sv.NofifyMission);
        


    }

    private void allHide()
    {
        _mission.enabled = false;
        _map.enabled = false;
        _inventory.enabled = false;
        _allGroup.DOFade(0, DialogFon.DurationFade).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    private void allShow()
    {

        //if (!SS.sv.ActiveInterface) return;
        gameObject.SetActive(true);
        _iconPlayerStatic.SetActive(SS.sv.ActiveIconPlayer);
        _mission.enabled = true;
        _map.enabled = true;
        _inventory.enabled = true;
        _allGroup.DOFade(1, DialogFon.DurationFade);
    }
    private void buttonHide()
    {
        
        _buttonLvlUp.enabled = false;
        _repairAds.SetActive(false);
        _mission.enabled = false;
        _map.enabled = false;
        _nameLoc.enabled = false;
        _inventory.enabled = false;
        _buttonGroup.DOFade(0, DialogFon.DurationFade).OnComplete(() => { _inventory.gameObject.SetActive(false); });
    }
    private void buttonShow()
    {
        _buttonLvlUp.enabled = true;
        if (!SS.sv.ActiveInterface) return;
        
        _repairAds.SetActive(true);
        _mission.enabled = true;
        _map.enabled = true;
        _nameLoc.enabled = true;
        _inventory.gameObject.SetActive(true);
        _inventory.enabled = true;
        _buttonGroup.DOFade(1, DialogFon.DurationFade);
    }



 

    public static void NameLoc(string name)
    {
        if(!_nameLocStatic.activeSelf)
        {
            _nameLocStatic.SetActive(true);
        }

        _nameLocStatic.transform.GetChild(0).GetComponent<TMP_Text>().text = name;
    }
    public static void NameLocOff()
    {
        _nameLocStatic.SetActive(false);
    }

}
