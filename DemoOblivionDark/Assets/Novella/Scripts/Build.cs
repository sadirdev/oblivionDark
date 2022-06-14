using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] private GameObject _nofify;
    [SerializeField] private GameObject _cliker;
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _missionPanel;
    [SerializeField] private Shop _shop;
    [SerializeField] private SliderGame _sliderGame;
    [SerializeField] private RewardsTablo _rewardTablo;
    [SerializeField] private lvlUpPanel _lvlUpPanel;
    
  
    private void Awake()
    {
        Show.Build = this;
    }
    public void Shop(bool openWithInventory)
    {
        _shop.OpenWithInventory = openWithInventory;
        Instantiate(_shop, transform);
    }
    public void Map()
    {
        Instantiate(_map, transform);
    }

    public void Missions()
    {
        // ¬—“¿¬ ¿ ƒÀﬂ “≈—“¿
        
        // ¬—“¿¬ ¿ ƒÀﬂ “≈—“¿
        _nofify.SetActive(false);
        SS.sv.NofifyMission = false;
        Instantiate(_missionPanel, transform);
    }
    public void SliderGame(string nameItem, int complexity )
    {
        
        if (nameItem == "–˚·‡")
        {
            if (!Inventory.Contains("”‰Ó˜Í‡"))
            {
                
                Dialog.Notification(LaungeSystem.Word("ÕÂÚ”‰Ó˜ÍË"));
                return;
            }
        }

        SliderGame slider = Instantiate(_sliderGame, transform);
        slider.transform.SetSiblingIndex(0);
        slider.Build(nameItem , complexity);
    }
    public void Reward(string reward, DelegateVoid delegateVoid)
    {
        var obj = transform.GetChild(transform.childCount - 1);

        if(obj.TryGetComponent(out RewardsTablo rewardTablo))
        {
            rewardTablo.Build(reward, delegateVoid);
        }
        else
        {
            Instantiate(_rewardTablo, transform).Build(reward, delegateVoid);
        }

    }
    public void LvlUp()
    {
        Instantiate(_lvlUpPanel, transform);
    }
    public void clicker(Clicker.EnemyEnum enemy)
    {
        Clicker.EnemyName = enemy;
        ClearCanvas();
        Instantiate(_cliker, transform);
    }
    public void clicker(Clicker.EnemyEnum enemy, DelegateVoid win, DelegateVoid lose)
    {
        Clicker.NoJump = true;
        LocMngr.NoJump = true;
        Clicker.WinDel = win;
        Clicker.LoseDel = lose;
        clicker(enemy);
    }
    public void clicker(Clicker.EnemyEnum enemy, DelegateVoid win)
    {
        Clicker.NoJump = true;
        LocMngr.NoJump = true;
        Clicker.WinDel = win;
        clicker(enemy);
    }
   
    private void ClearCanvas()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
