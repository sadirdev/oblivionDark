using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class SliderGame : MonoBehaviour
{
    private float _durationArrow = 3;
    private bool _playGame = false;
    private TweenerCore<float, float, FloatOptions> _arrowMove;
    private CanvasGroup _canvasGroup;
    [SerializeField] Slider _slider;
    //private Inventory _inventory;
    [SerializeField] private Image _imageFill;
    [SerializeField] private Image _imageItem;
    [SerializeField] private Image _arrowTop;
    [SerializeField] private Image _arrowDown;
    [SerializeField] private Sprite[] _itemsImg;
    [SerializeField] private RectTransform _clickArea;
    [SerializeField] private SliderCheck _sliderCheck;
    [SerializeField] private Transform _fill;
    private Sprite _itemSprite;

    private float _startArea;
    private float _finishArea;

    private float _crntTrue;
    private float _fillFalse = 0.2f;
    private float _addFillClick;


    public void Build(string nameItem, int complexity)
    {
        MainAudio.Static.PlaySlider();
        Complexity(complexity);
        _clickArea.anchorMin = new Vector2(_startArea, 0);
        _clickArea.anchorMax = new Vector2(_finishArea, 1);


        _slider.value = 0;
        _canvasGroup = GetComponent<CanvasGroup>();
        Interface.ButtonHide();
        _canvasGroup.alpha = 0;
        _imageFill.fillAmount = 0;
        _canvasGroup.DOFade(1, DialogFon.DurationFade).OnComplete(() =>
        {
            _playGame = true;
            _arrowMove = _slider.DOValue(1, _durationArrow).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
            _crntTrue = _imageFill.fillAmount;
           
        });

        foreach (var sprite in _itemsImg)
        {
            if(sprite.name == nameItem)
            {
                
                _itemSprite = sprite;
                _imageFill.sprite = sprite;
                _imageItem.sprite = sprite;
            }
        }
    }

    void Update()
    {
        if (_playGame)
        {
            if (ClickerIconPlayer.StaticHP.value <= 0)
            {
                Dialog.Notification(LaungeSystem.Word("NoEnergy"));
                Close(false);
            }
        }
    }
    public void Click()
    {
        if (!_playGame) return;

        if (_slider.value >= _startArea && _slider.value <= _finishArea)
        {
            ClickTrue();
        }
        else ClickFalse();
    }

    private void ClickTrue()
    {
        SliderCheckGeneration(true);
        //FadeObject.GreenImg(_clickArea); 
        FadeObject.GreenImg(_arrowTop);
        FadeObject.GreenImg(_arrowDown);
        _crntTrue += _addFillClick;

        _imageFill.DOColor(Color.green, 0.3f).OnComplete(() =>
        {
            _imageFill.DOColor(Color.white, 0.3f);
        });
        _imageFill.DOFillAmount(_crntTrue, 0.3f).OnComplete(()=> 
        {
            
            if (_crntTrue >= 1) Close(true);
        });
    }
    private void ClickFalse()
    {
        SliderCheckGeneration(false);
        ClickerIconPlayer.GetHPColor(-_addFillClick);
        //FadeObject.RedImg(_clickArea);
        FadeObject.RedImg(_arrowTop);
        FadeObject.RedImg(_arrowDown);

        ClickerIconPlayer.StaticHP.DOValue(ClickerIconPlayer.StaticHP.value - _fillFalse, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (ClickerIconPlayer.StaticHP.value <= 0)
            {
                Close(false);
            }
        });
    }
    private void SliderCheckGeneration(bool hit)
    {
        _sliderCheck.Hit = hit;
        Transform sliderHit = Instantiate(_sliderCheck.transform, _fill);
        sliderHit.parent = _slider.transform;
    }
    private void Complexity(int value)
    {
        if(value == 1)
        {
            _startArea = 0.45f;
            _finishArea = 0.55f;
            _durationArrow = 2.3f;
            _addFillClick = 1f / 8f;
        }
        if (value == 2)
        {
            _startArea = 0.45f;
            _finishArea = 0.55f;
            _durationArrow = 2f;
            _addFillClick = 1f / 10f;
        }
        if (value == 3)
        {
            _startArea = 0.45f;
            _finishArea = 0.55f;
            _durationArrow = 2f;
            _addFillClick = 1f / 15f;
        }
        else if(value == 4)
        {
            _startArea = 0.45f;
            _finishArea = 0.55f;
            _durationArrow = 2;
            _addFillClick = 1f / 23f;
        }
    }

   
    public void Close(bool win)
    {
        
        SS.sv.Player.HP = ClickerIconPlayer.StaticHP.value;
        Interface.ButtonShow();
        _playGame = false;
        _arrowMove.Kill();
        MainAudio.Static.PlayFon(); 
        _canvasGroup.DOFade(0, DialogFon.DurationFade).OnComplete(() =>
        {
            Destroy(gameObject);
        });
        if(win)
        {
            
            if (_itemSprite.name == "Здание")
            {
                Etale.HouseDel();
            }
            else if(_itemSprite.name == "Сок")
            {
                Dialog.BuildWithDic("СокДостал", () => 
                {

                    SS.sv.Player.StatAgress -= 0.1f;
                    
                    SS.sv.Player.StatSaitama += 0.13f;
                    Show.Build.Reward(LaungeSystem.Word("NewBlock"), () =>
                    {
                        MissionManager.SetStep(Mission.Kep_4lvl_ВирусШтаб, 4);
                        Show.Loc.JumpLoc(Location.SuperMarket); 
                    }); 
                    Show.Build.Reward(LaungeSystem.RewardExp(120), () => { LvlManager.Static.GetExp(120);  });

                });
            }
            else if(_itemSprite.name == "Рамен")
            {
                Inventory.Remove("Лапша");
                Dialog.BuildWithDic("УжинГотов", () => { MissionManager.NextStep(Mission.Kep_5lvl_ГеносСпасиДев); SS.sv.Player.StatSaitama += 0.15f; });
            }
            else if(_itemSprite.name == "Рыба")
            {
                Dialog.Notification(LaungeSystem.RewardItem(_itemSprite.name));
                Inventory.Add(_itemSprite.name);
            }
            else if(_itemSprite.name == "Текстолит")
            {
                
                Dialog.BuildWithDic("МониторПочинил", () => {  Show.Loc.JumpLoc(Location.CityZ); MissionManager.NextStep(Mission.Kep_6lvl_ВирусПриставка); Inventory.Remove("Кремний"); Inventory.Remove("Текстолит");  Inventory.RemoveCound("Конденсатор", 3); Debug.Log("Yes"); SS.sv.Player.StatAgress -= 0.1f; });
                
            }
            else if (_itemSprite.name == "Бачок")
            {
               
                if(SS.sv.Player.Name == Player.Genos)
                {
                    Dialog.Notification(LaungeSystem.RewardItem("Компас"));
                    Inventory.Add("Компас");
                    MissionManager.Add(Mission.КомпасПирата);
                }
                else
                {
                    Dialog.Notification(LaungeSystem.RewardItem("Конденсатор"));
                    Inventory.Add("Конденсатор");
                }
            }
            else if (_itemSprite.name == "Кремний")
            {
                Dialog.Notification(LaungeSystem.RewardItem(_itemSprite.name));
                Inventory.Add("Кремний");
            }
            else if(_itemSprite.name == "Уборка")
            {
                Dialog.Notification("Квартира убрана");
                SS.sv.Player.StatSaitama += 0.2f;
                MissionManager.NextStep(Mission.Kep_7lvl_ГеносМумен);
            }
            else if(_itemSprite.name == "Камушек")
            {

                Dialog.BuildWithDic("ЗенкоСайтама", () => 
                { 
                    
                    Show.Build.Reward("Новый блок синхронизации «Kepler 360» открыт.", () =>
                    {
                        Inventory.Add("Катана");
                        MissionManager.NextStep(Mission.Kep_7lvl_ГеносМумен);
                    });
                    Show.Build.Reward("Получено : 150 единиц опыта.", () => { LvlManager.Static.GetExp(150); });
                });
            }
        }
        else
        {
            
        }
    }

    
}
