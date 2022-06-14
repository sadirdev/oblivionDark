using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Inventory : MonoBehaviour
{

    public static bool OpenBool = false;
    [SerializeField] public  ItemData[] Items;
    [SerializeField] private AudioSource _addSound;

    private Animation _animation;
    private Button _buttonOpen;
    [SerializeField] private GameObject _nofifi;
    [SerializeField] private Button _buttonClose;
    [SerializeField] private Transform _canvas;
    [SerializeField] private Transform _buttonTransform;
    [SerializeField] private GameObject _scroll;
    [SerializeField] private ShopItem _itemPrefab;
    [SerializeField] private Coins _coinsPrefab;
    private Coins _coinsTemp;
    private CanvasGroup _scrollTemp;
    private Button _buttonCloseTemp;
    private float _durationAnimate = 0.55f;
    public delegate void AddDel(string item);
    public delegate void RemoveCoundDel(string item, int cound);
    public delegate bool ContainsDel(string item);
    public delegate bool ContainsCoundDel(string item, int cound);
    public delegate string Name(string item);

    public static Name NameLang;
    public static AddDel Add;
    public static AddDel Remove;
    public static RemoveCoundDel RemoveCound;
    public static ContainsDel Contains;
    public static ContainsCoundDel ContainsCound;

    private void Start()
    {
        _nofifi.SetActive(SS.sv.NofifyInventory);
        Add = AddVoid;
        Remove = RemoveVoid;
        RemoveCound = RemoveCoundVoid;
        Contains = ContainsVoid;
        ContainsCound = ContainsCoundVoid;
        _animation = GetComponent<Animation>();
        _buttonOpen = GetComponent<Button>();
        NameLang = NameLangVoid;
    }


    public string NameLangVoid(string name)
    {
        foreach (var item in Items)
        {
            if(item.name == name)
            {
                if (SS.sv.Lang == lng.rus) return item.NameRus;
                else if (SS.sv.Lang == lng.eng) return item.NameEng;
                else if (SS.sv.Lang == lng.por) return item.NamePor;
            }
        }
        return null;
    }
    private void AddVoid(string itemName)
    {
        foreach (var item in SS.sv.Player.ItemsInventory)
        {
            if (itemName == item.Name)
            {
                item.Cound++;
                _addSound.Play();
                return;
            }
        }
        _addSound.Play();
        SS.sv.NofifyInventory = true;
        _nofifi.SetActive(true);
        SS.sv.Player.ItemsInventory.Add(new Item(itemName));
    }
    private void RemoveVoid(string itemName)
    {
        var tempItem = SearchItem(itemName);
        tempItem.Cound--;
        if (tempItem.Cound == 0) SS.sv.Player.ItemsInventory.Remove(tempItem);
        

        
        if(transform.childCount >2) //Чтоб обновить открытый инвентарь
        {
            AddItem();
        }
    }
    private void RemoveCoundVoid(string itemName, int cound)
    {
        var tempItem = SearchItem(itemName);
        tempItem.Cound -= cound;
        if (tempItem.Cound == 0) SS.sv.Player.ItemsInventory.Remove(tempItem);



        if (transform.childCount > 2) //Чтоб обновить открытый инвентарь
        {
            AddItem();
        }
    }
    private bool ContainsVoid(string item)
    {
        return SS.sv.Player.ItemsInventory.Contains(SearchItem(item));
    }
    private bool ContainsCoundVoid(string item, int cound)
    {
        return SearchItem(item).Cound >= cound;
    }
    private Item SearchItem(string name)
    {
        foreach (var item in SS.sv.Player.ItemsInventory)
        {
            if (item.Name == name) return item;
        }
        return null;
    }

    public void Open()
    {
        OpenBool = true;
        SS.sv.NofifyInventory = false;
        _nofifi.SetActive(false);
        _buttonCloseTemp = Instantiate(_buttonClose, _canvas);
        
        _buttonOpen.enabled = false;
        _animation.Play("OpenInventory");
        
        _scrollTemp = Instantiate(_scroll.GetComponent<CanvasGroup>(), transform);
        AddItem();
        StartCoroutine(setParent());

        IEnumerator setParent()
        {
            yield return new WaitForSeconds(_durationAnimate/2);
            _scrollTemp.DOFade(1, 0.5f);
            _coinsTemp = Instantiate(_coinsPrefab, transform);
            yield return new WaitForSeconds(_durationAnimate/2);
            _buttonCloseTemp.onClick.AddListener(() => Close());
            transform.SetParent(_canvas);
        }
      
    }
    private void AddItem()
    {
        Transform content = _scrollTemp.transform.GetChild(0).GetChild(0);
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in SS.sv.Player.ItemsInventory)
        {
            foreach (var itemData in Items)
            {
                if (itemData.name == item.Name)
                {
                    if(!(item.Name == "РукиДомашнихЗабот" || item.Name == "Сенсор" || item.Name == "Ускорители" || item.Name == "Ядро"))
                    {
                        itemData.Cound = item.Cound;

                        Instantiate(_itemPrefab, content).ItemData = itemData;
                    }
                    
                   
                }
            }
        }
    }
    public void Close()
    {
        OpenBool = false;
        Destroy(_buttonCloseTemp.gameObject);
        transform.SetParent(_buttonTransform);
        _coinsTemp.Close();
        StartCoroutine(destroyScroll());
        _animation.Play("CloseInventory");
        IEnumerator destroyScroll()
        {
            _scrollTemp.transform.GetChild(1).gameObject.SetActive(false); // Отключить верхний градиент
            //yield return new WaitForSeconds(_durationAnimate / 2);
            _scrollTemp.DOFade(0, 0.3f);
            yield return new WaitForSeconds(_durationAnimate / 2);
            _buttonOpen.enabled = true;
            for (int i = 2; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

        }
    }
    [System.Serializable]
    public class Item
    {
        public Item(string name)
        {
            Name = name;
        }
        public string Name;
        public int Cound = 1;
    }
}
