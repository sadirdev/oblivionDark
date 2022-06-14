using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class lvlUpPanel : MonoBehaviour
{
    public static bool LvlUp = false;

    [SerializeField] private Preferens _preferens;

    [SerializeField] private GameObject _title;
    [SerializeField] private TMP_Text _titleText;

    [SerializeField] private GameObject _badge;
    [SerializeField] private GameObject _stat;
    [SerializeField] private Text _lvl;

    [SerializeField] private Slider _health;
    [SerializeField] private Slider _damage;
    [SerializeField] private Slider _agress;
    [SerializeField] private Slider _resident;
    [SerializeField] private Slider _repSaitama;



    [SerializeField] private GameObject _item1;
    [SerializeField] private GameObject _item2;
    [SerializeField] private GameObject _item3;
    [SerializeField] private GameObject _item4;

    [SerializeField] private GameObject _close1;
    [SerializeField] private GameObject _close2;
    [SerializeField] private GameObject _close3;
    [SerializeField] private GameObject _close4;
    private ClickerIconPlayer.Specifications _player;

    void Start()
    {


        GetComponent<CanvasGroup>().DOFade(1, 0.5f);
         _player = SS.sv.Player;
        

        if (LvlUp)
        {
            _title.SetActive(false);
            _badge.SetActive(true);
            _stat.SetActive(false);
            _player.StatHealth = +0.05f;
            _player.StatDamage = +0.05f;

            LvlUp = false;
            StartCoroutine(lvlUp());
        }
        else
        {
            _title.SetActive(true);
            _badge.SetActive(false);
            _stat.SetActive(true);
            _health.value = _player.StatHealth;
            _damage.value = _player.StatDamage;
            _agress.value = _player.StatAgress;
            _resident.value = _player.StatResident;
            _repSaitama.value = _player.StatSaitama;


            string nameGenos = "Генос";
            string nameVirus = "Вирус";

            if(SS.sv.Lang == lng.eng)
            {
                nameGenos = "Genos";
                nameVirus = "Virus";
            }
            else if(SS.sv.Lang == lng.por)
            {
                nameGenos = "Genos";
                nameVirus = "Virus";
            }

            if (_player.Name == Player.Genos) _titleText.text = nameGenos;
            else _titleText.text = nameVirus;

            Baze(_player.LVL);
        }



        if (SS.sv.ActiveInterface)
        {
            if (Inventory.Contains("РукиДомашнихЗабот")) _item1.SetActive(true);
            if (Inventory.Contains("Сенсор")) _item2.SetActive(true);
            if (Inventory.Contains("Ускорители")) _item3.SetActive(true);
            if (Inventory.Contains("Ядро")) _item4.SetActive(true);
        }
        else
        {
            _health.value = 0.18f;
            _damage.value = 0.13f;
            _agress.value = 0.04f;
            _resident.value = 0.95f;
            _repSaitama.value = 0.93f;
        }
       
    }

    

    private void Baze(int lvl)
    {
        if (lvl >= 1) _close1.SetActive(false);
        if (lvl >= 2) _close2.SetActive(false);
        if (lvl >= 3) _close3.SetActive(false);
        if (lvl >= 4) _close4.SetActive(false);
    }

    private IEnumerator lvlUp()
    {
        int tempLvl = _player.LVL - 1;
        

        _lvl.text = Clicker.ClassReturn(tempLvl);
        Baze(tempLvl);


        yield return new WaitForSeconds(1);


        _lvl.text = Clicker.ClassReturn(_player.LVL);
        Baze(_player.LVL);
    }


    public void ClickContinum()
    {
        GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
        
    }

    public void PreferensOpen()
    {
        Instantiate(_preferens, transform);
    }
 
}


