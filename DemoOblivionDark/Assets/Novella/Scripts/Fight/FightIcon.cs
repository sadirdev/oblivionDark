using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FightIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;
    private int _maxHp;
    private int _crntHp;
    
    public void EnterHp(int maxHP, Sprite sprite)
    {
        _icon.sprite = sprite;
        _maxHp = _crntHp = maxHP;
        _text.text = _crntHp.ToString();
    }

    public void UpdateSlider(int shiftHP)
    {
        _crntHp += shiftHP;
        _slider.DOValue((float)_crntHp / _maxHp, 0.8f);
        _text.text = _crntHp.ToString();
    }
}
