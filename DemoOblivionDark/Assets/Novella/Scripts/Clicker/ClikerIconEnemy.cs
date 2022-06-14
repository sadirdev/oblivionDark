using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ClikerIconEnemy : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _lvl;
    private float _durationClick = 0.1f;
    private float _damage;
    public static float CrntHP;


    private void Start()
    {
        CrntHP = 1;

    }
    public void Build(Sprite icon, float damage, string lvl)
    {
        _damage = damage;
        _icon.sprite = icon;
        _lvl.text = lvl;
    }

    public void ClickEnemy()
    {
        CrntHP -= _damage;
        _slider.DOValue(CrntHP, _durationClick);
        if (Clicker.ImpossibleWin)
        {
            float q = Show.IconPlayer.Slider.value - _damage;
            Show.IconPlayer.Slider.DOValue(q, _durationClick);
        }
    }
}
