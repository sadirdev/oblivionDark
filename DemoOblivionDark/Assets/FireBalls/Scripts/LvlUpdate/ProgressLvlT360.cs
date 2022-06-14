using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class ProgressLvlT360 : MonoBehaviour
{
    
    [SerializeField] private Slider _slider;
    [SerializeField] public Image Handle;
    [SerializeField] private Transform _percent;
    [SerializeField] private Transform _positionStart;
    [SerializeField] private Transform _positionFinish;
    [SerializeField] private Color _yellow;
    private Color _blue;
    [SerializeField] private Text _crntlvl;
    [SerializeField] private Text _nextlvl;
    private float _durationLvlUpdate = 1;
    private float _addFill;
    private TweenerCore<float, float, FloatOptions> _animSlider;
    private void Awake()
    {
        _blue = _crntlvl.color;
       
    }
    private void Start()
    {
        if (ST.sv.LvlKepler == 0) ST.sv.LvlKepler = 1;
        _slider.value = ST.sv.FillProgress;
        //Debug.LogWarning($"_slider.value = {_slider.value}");
        if (_slider.value < 0.5f) _percent.position = _positionStart.position;
        else _percent.position = _positionFinish.position;

        LvlUpdate();
       
    }
    void LvlUpdate()
    {
        

        if (ST.sv.LvlKepler < 10) _crntlvl.text = "0" + ST.sv.LvlKepler.ToString();
        else _crntlvl.text = ST.sv.LvlKepler.ToString();
        int nextLvl = ST.sv.LvlKepler + 1;
        if (nextLvl < 10) _nextlvl.text = "0" + nextLvl.ToString();
        else _nextlvl.text = nextLvl.ToString();
        FindObjectOfType<BGBall>().FonGenerate();
    }

    public void CheckBlockNextbttn()
    {

    }


    public void Add(float delay)
    {
        LvlPoint();
        Handle.enabled = true;

        _animSlider = _slider.DOValue(_slider.value + _addFill, delay).SetEase(Ease.Linear);
        if(_slider.value + _addFill >=1)
        {
            FindObjectOfType<NextButton>().Disable();
            ST.sv.LvlKepler++;
            MissionManager.UpdateMissions();
         
        }
    }
    private void LvlPoint()
    {
        int lvlPoint = 0;
        //if (ST.sv.LvlKepler == 1) lvlPoint = 100;

        //else lvlPoint = 120;
        if (ST.sv.LvlKepler == 1) lvlPoint = 4000;
        else if (ST.sv.LvlKepler == 2) lvlPoint = 7000;
        else if (ST.sv.LvlKepler == 3) lvlPoint = 10000;
        else if (ST.sv.LvlKepler == 4) lvlPoint = 12000;
        else if (ST.sv.LvlKepler == 5) lvlPoint = 15000;
        else lvlPoint = 17000;
        _addFill = ExpAddBall.CrntPoitnExp / lvlPoint;
         ST.sv.FillProgress += _addFill;
       
        
    }
    public void KillAnim()
    {
        
        _animSlider.Kill(); 
        ST.sv.FillProgress = 0;
       
        lvlUpdate(_crntlvl, true);
        lvlUpdate(_nextlvl, false);


        void lvlUpdate(Text text, bool start)
        {
            text.transform.DOScale(1.8f, _durationLvlUpdate).SetLoops(2, LoopType.Yoyo);
            text.DOColor(_yellow, _durationLvlUpdate).OnComplete(() =>
            {
                if(start)
                {
                    FindObjectOfType<BttnExit>().Start();
                    Start();
                }
                    
                text.DOColor(_blue, _durationLvlUpdate);
            });
        }
        
    }

    
}
