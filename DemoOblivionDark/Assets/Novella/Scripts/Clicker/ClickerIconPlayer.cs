using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;

public class ClickerIconPlayer : MonoBehaviour
{
    [SerializeField] private Text _testTestLeft;
    [SerializeField] private Text _testTestRight;
    [SerializeField] private Image _icon;
    public Slider Slider;
    public static Slider StaticHP;
    private static Image _fillImg;
    [HideInInspector] public Clicker Clicker;
    [SerializeField] private Sprite _genosIcon;
    [SerializeField] private Sprite _virusIcon;
    public TMP_Text Lvl;
    [SerializeField] private GameObject _nameGenos;
    [SerializeField] private GameObject _nameVirus;
    //[SerializeField] TMP_Text _name;
    public GameObject Repair;
     private float _duration;
    private TweenerCore<float, float, FloatOptions> _sliderAnim;
    private void Awake()
    {
        _fillImg = Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        StaticHP = Slider;
        Show.IconPlayer = this;
        gameObject.SetActive(SS.sv.ActiveIconPlayer);
        LocMngr.LocUpdate += Start;

    }
    public void LvlUp()
    {
        //MainAudio.Static.PlaySlider();
        Show.Build.LvlUp();
    }

    public void AfterDelete()
    {
        MainAudio.Static.PlaySlider();
    }

    public void TestViewDebag(string debag)
    {
        _testTestRight.text = debag;
    }
    public void TestBttn()
    {

        MissionManager.Complite(Mission.Kep_4lvl_ВирусШтаб);
        //MainAudio.Static.PlayFon();
        //SS.sv.Player = SS.sv.Genos;
        //MissionManager.Add(Mission.Kep_7lvl_ГеносМумен);
        //MissionManager.SetStep(Mission.Kep_7lvl_ГеносМумен, 6);

        //MissionManager.Add(Mission.Kep_5lvl_ГеносСпасиДев);
        //MissionManager.SetStep(Mission.Kep_5lvl_ГеносСпасиДев, 3); 
        //LvlManager.Static.GetExp(120);
        

        //SS.sv.Gem = 0;

        //Inventory.Remove("Удочка");
        //Show.Build.Reward("Получено : 120 единиц опыта.", null);
        //foreach (var item in SS.sv.ActiveMissions)
        //{
        //    Debug.Log($"item.Name = {item.Name}");
        //}
        //foreach (var item in SS.sv.Genos.ItemsInventory)
        //{
        //    Debug.Log($"item.Name = {item.Name}");
        //}
        //if (SS.sv.Genos.ItemsInventory.Count == 0) Debug.Log($"Нихуя нет");

        //LvlManager.Static.GetExp(11);
    }
    public void Start()
    {
        gameObject.SetActive(SS.sv.ActiveIconPlayer);

        if (SS.sv.Player.Name == Player.Genos)
        {
            _icon.sprite = _genosIcon;
            _nameGenos.SetActive(true);
            _nameVirus.SetActive(false);
        }
        else
        {
            _icon.sprite = _virusIcon;
            _nameGenos.SetActive(false);
            _nameVirus.SetActive(true);
        }
        

        Slider.value = SS.sv.Player.HP;
        Lvl.text = Clicker.ClassReturn(SS.sv.Player.LVL);
       
        if (Slider.value != 1) Repair.SetActive(true);
        else Repair.SetActive(false);


        _testTestLeft.text = SS.sv.testAdsCoundGenos.ToString();
        _testTestRight.text = SS.sv.testAdsCoundVirys.ToString();


    }

    public void Build(float duration)
    {
        _duration = duration;
        
    }
    public void Stop()
    {

        SS.sv.Player.HP = Slider.value;
        _sliderAnim.Kill();
    }

    public void SliderAnim()
    {
        _sliderAnim = Slider.DOValue(0, _duration * Slider.value).SetEase(Ease.Linear).OnComplete(()=>
        {
            Clicker.Loose();
        });
    }

    public void ClickRepair()
    {
        LocMngr.NoJump = true;
        


        Dialog.Build_5("ads", () => // 1
        {
            Debug.Log("1");
            SS.sv.Gem--;
            SS.sv.Player.HP = 1;
            Repair.SetActive(false);
            GetHP(1);

        }, () =>// 2
        {
            Debug.Log("2");
            ShowAds();

        }, () =>// 3
        {
            Debug.Log("3");
          
        }, () =>// 4
        {
            Debug.Log("4");
           
        }, () =>// 5
        {
            Debug.Log("5");
        });

    }

    private void ShowAds()
    {
        if (SS.sv.SecondsReward > 0)
        {
            if (!EdMob.ShowAdsBet())
            {
                Dialog.Notification(LaungeSystem.Word("НуженИнтернет"));
                return;
            }
        }
        else
        {
            if (!EdMob.ShowAdsFull())
            {
                Dialog.Notification(LaungeSystem.Word("НуженИнтернет"));
                return;
            }
        }





        if (SS.sv.Player.Name == Player.Genos)
        {
            SS.sv.testAdsCoundGenos++;
        }
        else
        {
            SS.sv.testAdsCoundVirys++;
        }
        SS.sv.Player.HP = 1;

        Repair.SetActive(false);
        GetHP(1);
        Debug.Log("Ads show");
    }
    public static void GetHP(float hp)
    {
        float remains = StaticHP.value + hp;

        SS.sv.Player.HP = remains;

        if (remains > 1) remains = 1;
        else if (remains < 0) remains = 0;
        StaticHP.DOValue(remains, 1.5f);
    }
    
    public static void GetHPColor(float hp)
    {
        
        if(hp>0)
        {
            FadeObject.GreenImg(_fillImg);
        }
        else FadeObject.RedImg(_fillImg);

    }

    [System.Serializable]
    public class Specifications
    {
        public Player Name;
        public float HP = 1;
        public float EXP = 0;
        public int LVL = 1;
        public int Coins = 100;
        public List<Inventory.Item> ItemsInventory = new List<Inventory.Item>();
        public int IndexMusic;
        public float StatHealth;
        public float StatDamage;
        public float StatResident;
        public float StatAgress;
        public float StatSaitama;

        public Specifications(Player Name)
        {
            this.Name = Name;
        }
    }
  
    void OnDestroy()
    {
        LocMngr.LocUpdate -= Start;
    }
}


public enum Player { Genos, Virus}

