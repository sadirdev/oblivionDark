using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etale : MonoBehaviour
{
    [SerializeField] private Sprite _headquarters;
    public static DelegateVoid HouseDel;
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        HouseDel = HouseVoid;

    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()// сама локация 
    {
    }
    private void HouseVoid()
    {
        StartCoroutine(house());
        IEnumerator house() // Сам дом
        {
            LocMngr.FadeJump(_headquarters);
            yield return new WaitForSeconds(DialogFon.DurationFade);

            Dialog.BuildWithDic("СобраниеВШтабе", () =>
            {
                LocMngr.RemoveLoc();
                Dialog.BuildWithDic("Фанатки", () => 
                {
                    
                    SS.sv.Player.StatSaitama -= 0.08f;
                    SS.sv.Player.StatResident -= 0.23f;
                    SS.sv.Player.StatAgress += 0.1f;
                    MissionManager.NextStep(Mission.Kep_4lvl_ВирусШтаб);
                });
            });

        }
    }
    
    
    public void ClickHouse()
    {
      
        if (MissionManager.CrntStep(Mission.Kep_4lvl_ВирусШтаб, 1))
        {
            Dialog.BuildWithDic("ПолетНаЗдание", () =>
            {
                Show.Build.SliderGame("Здание",3);
            });
        }
        else Dialog.Notification(LaungeSystem.Word("BuildingHeroClose"));

    }
    public void ClickHighway()
    {
        Show.Loc.JumpLoc(Location.HighwayA);
    }


}
