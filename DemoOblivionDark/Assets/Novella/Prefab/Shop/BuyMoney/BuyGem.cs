using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.Purchasing;

public class BuyGem : MonoBehaviour
{


    //[SerializeField] private PurchaseManager _purchaseManager;

    [SerializeField] private int _value;
    [SerializeField] private float _price;
    [SerializeField] private int _indexBuyId;


    [SerializeField] private TMP_Text _textValue;
    private TMP_Text _textPrice;


    private void Start()
    {
        //PurchaseManager.OnPurchaseConsumable += PurchaseManager_OnPurchaseConsumable;

        _textPrice = transform.GetChild(0).GetComponent<TMP_Text>();
        _textValue.text = _value.ToString();
        _textPrice.text = "US $" + _price.ToString();
    }
    //private void PurchaseManager_OnPurchaseConsumable(PurchaseEventArgs args)
    //{
    //    //Debug.Log($"args.purchasedProduct.definition.id = {args.purchasedProduct.definition.id}");
    //    if(_purchaseManager.C_PRODUCTS[_indexBuyId] == args.purchasedProduct.definition.id)
    //    {
    //        Debug.Log($"Потрачено {_price} баксов");
    //        SS.sv.Gem += _value;
    //        FindObjectOfType<Shop>().CoinsUpdate(false);
    //    }
    //}

    public void Buy()
    {
        //_purchaseManager.BuyConsumable(_indexBuyId);
    }


   
}
