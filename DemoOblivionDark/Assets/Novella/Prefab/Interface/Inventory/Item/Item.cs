using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [HideInInspector]public ItemData ItemData;
    [SerializeField]private Image _image;

    void Start()
    {
        _image.sprite = ItemData.Sprite;
    }

    public void Click()
    {
        if (ItemData.name == "Рыба") Dialog.BuildWithDic("РыбаВопрос", () => { Inventory.Remove("Рыба"); ClickerIconPlayer.GetHP(0.2f); });
        else if (ItemData.name == "Рак") Dialog.BuildWithDic("РакВопрос", () => { Inventory.Remove("Рак"); ClickerIconPlayer.GetHP(0.5f); });
        else if (ItemData.name == "Компас")
        {
            if(SS.sv.CrntLoc == Location.PradatoryPath || SS.sv.CrntLoc == Location.PredatoryHome|| SS.sv.CrntLoc == Location.Portal)
            {
                Dialog.Build("НеТаЛокация");
            }
            else if(SS.sv.CrntLoc == Location.North)
            {
                Dialog.BuildWithDic("ТаЛокация", () =>
                 {
                     Show.Build.clicker(Clicker.EnemyEnum.ДухПирата, () =>
                     {
                         Debug.Log("Миссия завершена");
                         MissionManager.Complite(Mission.КомпасПирата);
                     });
                 });
            }
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(ItemData.DescroptionRus))
            {
                Dialog.Notification(ItemData.DescroptionRus);
            }
        }



        
    }

}
