using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    public static UnityEvent UpdateButtonGem = new UnityEvent();

    public bool OpenWithInventory;


    [SerializeField] private GameObject _buyGold;
    [SerializeField] private GameObject _buyGem;
    [SerializeField] private GameObject _buyItem;
    [SerializeField] private Inventory _inventory; 
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _gemText;
    [SerializeField] private ShopItem _prefabItem;
    [SerializeField] private Transform _content;
    [HideInInspector]public List<ShopItem> ItemsTemp;



    private void Start()
    {
        _coinsText.text = SS.sv.Player.Coins.ToString();
        _gemText.text = SS.sv.Gem.ToString();

        if (OpenWithInventory)
        {
            Buy_Gold();
            return;
        }

        BuyItem();
        _inventory = FindObjectOfType<Inventory>();
         ItemsTemp =new List<ShopItem>();

        _content.GetComponent<GridLayoutGroup>().cellSize = new Vector2(130 * AdaptiveObject.Delta(), 130 * AdaptiveObject.Delta());


        for (int i = 0; i < _inventory.Items.Length; i++)
        {
            if (_inventory.Items[i].Grocery && SS.sv.CrntLoc == Location.SuperMarket)
            {
                var item = Instantiate(_prefabItem, _content);
                item.ItemData = _inventory.Items[i];
                ItemsTemp.Add(item);
            }
            else if (_inventory.Items[i].Sale != 0 && !_inventory.Items[i].Grocery && SS.sv.CrntLoc != Location.SuperMarket)
            {

                var item = Instantiate(_prefabItem, _content);
                item.ItemData = _inventory.Items[i];
                ItemsTemp.Add(item);
            }

        }


        
    }
  
    public void Close()
    {
        if (OpenWithInventory)
        {
            Destroy(gameObject);
            return;
        }

        if(_buyGold.activeSelf)
        {
            

            _buyItem.SetActive(true);
            FindObjectOfType<ShopItemInfo>().UpdateSale();
            _buyGold.SetActive(false);
            return;
        }
        else if (_buyGem.activeSelf)
        {
            _buyItem.SetActive(true);
            FindObjectOfType<ShopItemInfo>().UpdateSale();
            _buyGem.SetActive(false);
            return;
        }
        Destroy(gameObject);
    }
    public void CoinsUpdate(bool coins)
    {
        TMP_Text TMPtest;
        int cound = 0;

        if (coins)
        {
            cound = SS.sv.Player.Coins;
            TMPtest = _coinsText;
        }
        else
        {
            cound = SS.sv.Gem;
            TMPtest = _gemText;
        }


        int before = int.Parse(TMPtest.text);
        StartCoroutine(delayAnim());

        IEnumerator delayAnim()
        {
            
            float delay = Mathf.Abs(cound - before);
            int tempAdd = (int)Mathf.Ceil(delay/150);

            if (before > cound)
            {
                while (before > cound)
                {
                    before -= tempAdd;
                    TMPtest.text = before.ToString();
                    yield return null;
                }
            }
            else
            {
                while (before < cound)
                {
                    before += tempAdd;
                    TMPtest.text = before.ToString();
                    yield return null;
                }
            }
            TMPtest.text = cound.ToString();


        }

    }
    public void Buy_Gold()
    {
        _buyItem.SetActive(false);
        _buyGem.SetActive(false);
        _buyGold.SetActive(true);
    }
    public void BuyGem()
    {
        _buyItem.SetActive(false);
        _buyGem.SetActive(true);
        _buyGold.SetActive(false);
    }
    public void BuyItem()
    {
        _buyItem.SetActive(true);
        _buyGem.SetActive(false);
        _buyGold.SetActive(false);
    }
}
