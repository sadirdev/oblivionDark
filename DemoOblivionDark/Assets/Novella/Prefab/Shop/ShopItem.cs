using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [HideInInspector]public ItemData ItemData;
    [SerializeField] private Image _bgColor;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _focusPrefab;
    [HideInInspector]public GameObject _focusTemp;
    [SerializeField] GameObject CoundFrame;
    [SerializeField] TMP_Text CoudText;
    [SerializeField] GameObject _focus;

    [SerializeField] private Sprite _gray;
    [SerializeField] private Sprite _green;
    [SerializeField] private Sprite _blue;
    [SerializeField] private Sprite _purple;

    



    void Start()
    {
        Sprite color = _gray;
        if (ItemData == null) return; 
        switch (ItemData.Lvl)
        {
            case 1:
                color = _gray;
                break;
            case 2:
                color = _green;
                break;
            case 3:
                color = _blue;
                break;
            case 4:
                color = _purple;
                break;
        }
        _bgColor.sprite = color;
        _icon.sprite = ItemData.Sprite;
        if(ItemData.Cound > 1 && Inventory.OpenBool)
        {
            CoundFrame.SetActive(true);
            CoudText.text = ItemData.Cound.ToString();
        }
        if( !Inventory.OpenBool && ItemData.One && Inventory.Contains(ItemData.name)) Instantiate(_focus, transform);

        

    }
    public void Click()
    {
        if(Inventory.OpenBool)
        {
            if (ItemData.name == "Рыба") Dialog.BuildWithDic("РыбаВопрос", () => { Inventory.Remove("Рыба"); ClickerIconPlayer.GetHP(0.2f); });
            else if (ItemData.name == "Рак") Dialog.BuildWithDic("РакВопрос", () => { Inventory.Remove("Рак"); ClickerIconPlayer.GetHP(0.5f); });
            else if (ItemData.name == "Компас" && MissionManager.CrntStep(Mission.КомпасПирата,1))
            {
                if (SS.sv.CrntLoc == Location.PradatoryPath || SS.sv.CrntLoc == Location.PredatoryHome || SS.sv.CrntLoc == Location.Portal)
                {
                    Dialog.Build("НеТаЛокация");
                }
                else if (SS.sv.CrntLoc == Location.North)
                {
                    Dialog.BuildWithDic("ТаЛокация", () =>
                    {
                        FindObjectOfType<Inventory>().Close();
                        Show.Build.clicker(Clicker.EnemyEnum.ДухПирата, () =>
                        {
                            Show.Build.Reward(LaungeSystem.RewardCoins(2500), () => 
                            { 
                                Coins.MoveCoins(2500);
                                MissionManager.Complite(Mission.КомпасПирата);
                            });

                            Show.Build.Reward(LaungeSystem.RewardExp(90), () => { LvlManager.Static.GetExp(90); });


                        });
                    });
                }
                else
                {
                    Dialog.Build("ВообщеМимо");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(ItemData.DescroptionRus))
                {
                    Dialog.Notification(ItemData.DescroptionRus);
                }
            }
            return;
        }
        foreach (var item in FindObjectOfType<Shop>().ItemsTemp)
        {
            if(item._focusTemp != null)
            {
                Destroy(item._focusTemp);
            }
        }
        _focusTemp = Instantiate(_focusPrefab, transform);
       
        FindObjectOfType<ShopItemInfo>().Click(ItemData,_bgColor.sprite);
    }
   

  

}
