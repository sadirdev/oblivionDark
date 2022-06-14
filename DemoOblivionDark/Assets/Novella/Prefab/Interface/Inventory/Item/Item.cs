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
        if (ItemData.name == "����") Dialog.BuildWithDic("����������", () => { Inventory.Remove("����"); ClickerIconPlayer.GetHP(0.2f); });
        else if (ItemData.name == "���") Dialog.BuildWithDic("���������", () => { Inventory.Remove("���"); ClickerIconPlayer.GetHP(0.5f); });
        else if (ItemData.name == "������")
        {
            if(SS.sv.CrntLoc == Location.PradatoryPath || SS.sv.CrntLoc == Location.PredatoryHome|| SS.sv.CrntLoc == Location.Portal)
            {
                Dialog.Build("�����������");
            }
            else if(SS.sv.CrntLoc == Location.North)
            {
                Dialog.BuildWithDic("���������", () =>
                 {
                     Show.Build.clicker(Clicker.EnemyEnum.���������, () =>
                     {
                         Debug.Log("������ ���������");
                         MissionManager.Complite(Mission.������������);
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
