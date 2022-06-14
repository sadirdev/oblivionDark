using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour
{
    [SerializeField] BuyGold _buyGold;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Sprite _gray;
    [SerializeField] private Color _grayText;
    [SerializeField] private Sprite _yellow;
    [SerializeField] private Color _redText;
    private Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
        if (SS.sv.SecondsReward > 0)
        {
            _button.enabled = false;
            GetComponent<Image>().sprite = _gray;
            _text.color = _grayText;
        }
        else
        {
            _button.enabled = true;
            GetComponent<Image>().sprite = _yellow;
            _text.color = _redText;
        }
    }

    void Update()
    {
        if(SS.sv.SecondsReward>0) _text.text = SS.sv.SecondsReward.ToString();
        else if(_button.enabled == false)
        {
            _button.enabled = true;
            GetComponent<Image>().sprite = _yellow;
            _text.color = _redText;
            _text.text = LaungeSystem.Word("Забрать");
        }

    }

    public void Click()
    {
        Debug.Log("23435");
        if (!EdMob.ShowAdsFull())
        {
            Dialog.Notification(LaungeSystem.Word("НуженИнтернет"));
            return;
        }


      



        _buyGold.Ads();
        SS.sv.SecondsReward = 60;
        _button.enabled = false;
        GetComponent<Image>().sprite = _gray;
        _text.color = _grayText;
        FindObjectOfType<CondatorSeconds>().Start();
    }


   

}
