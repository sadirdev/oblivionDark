using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyGold : MonoBehaviour
{
    

    [SerializeField] private int _crntValue;
    [SerializeField] private int _crntPrice;

    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _gemIcon;
    
    [SerializeField] Color _gray;
    [SerializeField] Color _blue;

    //[SerializeField] private Sprite _gemGray;
    [SerializeField] private Sprite _gemBlue;

    [SerializeField] private Sprite _bttnGray;
    [SerializeField] private Sprite _bttnBlue;

    private Image _bttnImg;


    private void Awake()
    {
        _valueText.text = _crntValue.ToString() + " " + LaungeSystem.Word("Coins");
        if (_crntPrice > 1) _priceText.text = _crntPrice.ToString();

        _bttnImg = GetComponent<Image>();
    }
    private void OnEnable()
    {
        Shop.UpdateButtonGem.AddListener(UpdateBttn);
        UpdateBttn();
    }
    private void OnDisable()
    {
        Shop.UpdateButtonGem.RemoveListener(UpdateBttn);
    }
    public void Ads()
    {
       

        Debug.Log("ПосмотрелРекламу");

        Coins.MoveCoins(_crntValue);

        FindObjectOfType<Shop>().CoinsUpdate(true);

        
        

    }
    public void Buy()
    {
        SS.sv.Gem -= _crntPrice;
        Coins.MoveCoins(_crntValue);

        Shop shop = FindObjectOfType<Shop>();

        shop.CoinsUpdate(true);
        shop.CoinsUpdate(false);
        Shop.UpdateButtonGem?.Invoke();



    }
    private void UpdateBttn()
    {
        if (_crntPrice <= SS.sv.Gem)
        {
            GetComponent<Button>().enabled = true;
            if (_crntPrice > 1) _priceText.color = _blue;
            _gemIcon.color = Color.white;
            _bttnImg.sprite = _bttnBlue;
        }
        else
        {
            GetComponent<Button>().enabled = false;
            if (_crntPrice > 1) _priceText.color = _gray;
            _gemIcon.color = Color.gray;
            _bttnImg.sprite = _bttnGray;
        }
    }


}
