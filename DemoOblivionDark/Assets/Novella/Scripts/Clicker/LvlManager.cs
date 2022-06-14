using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class LvlManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _textExp;
    private Slider _slider;
    private float _maxExp;
    public static LvlManager Static;
    //private bool _open = true;

    public void Start()
    {
        _slider = GetComponent<Slider>();
        Static = this;
        UpdateExp();
        _slider.value = SS.sv.Player.EXP / _maxExp;
    }
    


    public void GetExp(float exp) 
    {
        
        float overExp = SS.sv.Player.EXP + exp;
        if(overExp >= _maxExp)
        {
            LvlUp(overExp-_maxExp);
        }
        else
        {
            float crntValue = (SS.sv.Player.EXP + exp) / _maxExp;
            _slider.DOValue(crntValue, 1).SetEase(Ease.Linear);
            SS.sv.Player.EXP += exp;
            UpdateExp();
        }
        

        void LvlUp(float overExp)
        {
            SS.sv.Player.LVL++;
            SS.sv.Player.EXP = 0;   
            _textExp.text = _maxExp.ToString() + "/" + _maxExp.ToString();
            _slider.DOValue(1, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                _slider.value = 0;
                lvlUpPanel.LvlUp = true;
                Show.Build.LvlUp();
                Show.IconPlayer.Lvl.text = Clicker.ClassReturn(SS.sv.Player.LVL);
                Show.IconPlayer.Start();
                GetExp(overExp);
            });
            

        }
    }
    private void UpdateExp()
    {
        if (SS.sv.Player.LVL == 1) _maxExp = 100;
        else if (SS.sv.Player.LVL == 2) _maxExp = 180;
        else _maxExp = 320;
        _textExp.text = SS.sv.Player.EXP.ToString() + "/" + _maxExp.ToString();
    }
}
