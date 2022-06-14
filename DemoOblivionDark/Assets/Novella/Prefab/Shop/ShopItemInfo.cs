using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemInfo : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private GameObject _focus;
    [SerializeField] private Transform _item;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _greenText;
    [SerializeField] private TMP_Text _grayText;
    [SerializeField] private TMP_Text _name;

    [SerializeField] private GameObject _bttnGreen;
    [SerializeField] private GameObject _bttnGray;

    [SerializeField] private Image _bgColor;
    [SerializeField] private Image _itemSprite;

    [SerializeField] private Color _nameColorGray;
    [SerializeField] private Color _nameColorGreen;
    [SerializeField] private Color _nameColorBlue;
    [SerializeField] private Color _nameColorPurple;

    private ItemData _itemData;
    
    private int _saleBufer;
    private int _lvlBufer;
    

    private void Start()
    {
        _bttnGray.SetActive(false);
        _bttnGreen.SetActive(false);
        _name.gameObject.SetActive(false);
        _item.gameObject.SetActive(false);
        _description.gameObject.SetActive(false);
    }


    public void Click(  ItemData itemData, Sprite bgColor)
    {
        _itemData = itemData;
        if (!_description.gameObject.activeSelf)
        {
            
            _name.gameObject.SetActive(true);
            _item.gameObject.SetActive(true);
            _description.gameObject.SetActive(true);
        }

        _itemSprite.sprite = itemData.Sprite;
        _bgColor.sprite = bgColor;

        //RectTransform itemRect = Instantiate(item, _item).GetComponent<RectTransform>();
        //itemRect.anchorMax = new Vector2(1, 1);
        //itemRect.rect.Set(0, 0, 0, 0);


        //for (int i = 1; i < itemRect.transform.childCount; i++)
        //{
        //    Debug.Log($"{itemRect.transform.GetChild(i).name}");

        //    Destroy(itemRect.transform.GetChild(i).gameObject);
        //}

        if (_itemData.Lvl == 1) _name.color = _nameColorGray;
        else if (_itemData.Lvl == 2) _name.color = _nameColorGreen;
        else if (_itemData.Lvl == 3) _name.color = _nameColorBlue;
        else if (_itemData.Lvl == 4) _name.color = _nameColorPurple;

        _description.text = _itemData.DescroptionRus;

        if(SS.sv.Lang == lng.rus) _name.text = _itemData.NameRus;
        else if(SS.sv.Lang == lng.eng) _name.text = _itemData.NameEng;
        else if (SS.sv.Lang == lng.por) _name.text = _itemData.NamePor;


        if (_itemData.One && Inventory.Contains(_itemData.name))
        {
            _bttnGray.SetActive(false);
            _bttnGreen.SetActive(false);


           
            return;
        }

        if (_itemData.Lvl<= SS.sv.Player.LVL && _itemData.Sale <= SS.sv.Player.Coins)
        {
            
            _bttnGray.SetActive(false);
            _bttnGreen.SetActive(true);
            _greenText.text = _itemData.Sale.ToString();
        }
        else if(_itemData.Lvl > SS.sv.Player.LVL)
        {
            _bttnGray.SetActive(true);
            _bttnGreen.SetActive(false);
            _grayText.text = Clicker.ClassReturn(_itemData.Lvl) + " class";
        }
        else if(_itemData.Sale > SS.sv.Player.Coins)
        {
            _bttnGray.SetActive(true);
            _bttnGreen.SetActive(false);
            _grayText.text = _itemData.Sale.ToString();
        }

        _saleBufer = _itemData.Sale; _lvlBufer = _itemData.Lvl;
        
    }
    public void UpdateSale()
    {
       
        if (_lvlBufer <= SS.sv.Player.LVL && _saleBufer <= SS.sv.Player.Coins)
        {
            _bttnGray.SetActive(false);
            _bttnGreen.SetActive(true);
            _greenText.text = _saleBufer.ToString();
        }
        else if (_lvlBufer > SS.sv.Player.LVL)
        {
            _bttnGray.SetActive(true);
            _bttnGreen.SetActive(false);
            _grayText.text = Clicker.ClassReturn(_lvlBufer) + " class";
        }
        else if (_saleBufer > SS.sv.Player.Coins)
        {
            _bttnGray.SetActive(true);
            _bttnGreen.SetActive(false);
            _grayText.text = _saleBufer.ToString();
        }
        if (!_item.gameObject.activeSelf)
        {
            _bttnGray.SetActive(false);
            _bttnGreen.SetActive(false);
        }
    }
    public void Buy()
    {
        foreach (var item in _shop.ItemsTemp)
        {
            if (item._focusTemp != null)
            {
                if(item.ItemData.name == "Сенсор")
                {
                    SS.sv.Player.StatHealth += 0.1f;
                    SS.sv.Player.StatDamage += 0.17f;
                }
                else if (item.ItemData.name == "Ускорители")
                {
                    SS.sv.Player.StatHealth += 0.3f;
                    SS.sv.Player.StatDamage += 0.2f;
                }
                else if (item.ItemData.name == "Ядро")
                {
                    SS.sv.Player.StatHealth += 0.3f;
                    SS.sv.Player.StatDamage += 0.3f;
                }

                Destroy(item._focusTemp.gameObject);
                Inventory.Add(item.ItemData.name);
                if(item.ItemData.One) Instantiate(_focus, item.transform);

                Coins.MoveCoins(-item.ItemData.Sale);
                Start();
                _shop.CoinsUpdate(true);

            }
        }
    }
    
}
