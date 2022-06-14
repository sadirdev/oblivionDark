using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LocButton : MonoBehaviour
{
    [SerializeField] private Location[] _nameLocs;
    
    [SerializeField] private GameObject[] _neighboringLocations;


    void Start()
    {

        GameObject goal = null;
        foreach (Location nameLoc in _nameLocs)
        {
            if (SS.sv.CrntLoc == nameLoc)
            {
                Instantiate(Map.PrefabFocus, transform);
                GetComponent<Button>().enabled = true;
                GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.Linear);
                GetComponent<Animation>().Play("MapIncreaseBttn");
                //Neighboring();
            }
            if (SS.sv.CrntGoal == nameLoc) goal =  Instantiate(Map.PrefabGoal, transform);
        }
        if(goal != null) goal.transform.transform.SetSiblingIndex(transform.childCount - 1);

        ///////
        GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.Linear);
        GetComponent<Button>().enabled = true;
        GetComponent<Animation>().Play("MapIncreaseBttn");

        //void Neighboring()
        //{
        //    foreach (var neighbour in _neighboringLocations)
        //    {
        //        neighbour.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.Linear);
        //        neighbour.GetComponent<Button>().enabled = true;
        //        neighbour.GetComponent<Animation>().Play("MapIncreaseBttn");
        //    }
        //}

    }
    public void Click()
    {
        for (int i = 0; i < _nameLocs.Length; i++)
        {
            if(i==0 && _nameLocs[i]== SS.sv.CrntLoc)
            {
                Destroy(FindObjectOfType<Map>().gameObject);
                return;
            }
            else if(_nameLocs[i] == SS.sv.CrntLoc)
            {
                Show.Loc.JumpLoc(_nameLocs[0], true);
                Destroy(FindObjectOfType<Map>().gameObject);
                return;
            }
        }
        


        if (gameObject.name == "Waterfall" && SS.sv.CrntLoc != _nameLocs[0])
        {

            Dialog.BuildWithDic("ПерелётНаВодопад", () =>
            {
                if (SS.sv.Player.HP < 0.3f)
                {
                    Dialog.Build("NotHP");
                    return;
                }

                SS.sv.CrntLoc = _nameLocs[0];
                ClickerIconPlayer.GetHP(-0.3f);
                Show.Loc.JumpLoc(Location.Waterfall, true);
                Destroy(FindObjectOfType<Map>().gameObject);
            });
            return;
        }
        else if((gameObject.name == "HighwayB") && SS.sv.CrntLoc != Location.Croaville && SS.sv.CrntLoc != Location.HighwayB)
        {
            
            Dialog.Notification(LaungeSystem.Word("ГородДалеко"));
            return;
        }
        else if ((gameObject.name != "HighwayB") && (SS.sv.CrntLoc == Location.Croaville && SS.sv.CrntLoc == Location.HighwayB))
        {

            Dialog.Notification(LaungeSystem.Word("ГородДалеко"));
            return;
        }
        //else if((gameObject.name == "HighwayB"|| gameObject.name == "Etale") && (SS.sv.CrntLoc != _nameLocs[0] || SS.sv.CrntLoc != _nameLocs[1]))
        //{
        //    if(SS.sv.CrntLoc != Location.Cave && SS.sv.CrntLoc != Location.BarLoc && SS.sv.CrntLoc != Location.Bar)
        //    {
        //        Dialog.Notification("Город Кроавиль находиться слишком далеко, вам нужен перевозчик");
        //        return;
        //    }

        //}
        Show.Loc.JumpLoc(_nameLocs[0], true);
        Destroy(FindObjectOfType<Map>().gameObject);
    }
   
}
