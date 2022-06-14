using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private GameObject _barman;
    private void Start()
    {
        LocMngr.LocUpdate += Jump;
        Interface.NameLoc(LaungeSystem.NameLoc());
        if (MissionManager.CrntStep(Mission.ПроверкаНовобранца, 2)) _barman.SetActive(true);
        else
        {
            if(SS.sv.Player.Name== Player.Genos) _barman.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        LocMngr.LocUpdate -= Jump;
    }


    private void Jump()
    {
        if (MissionManager.CrntStep(Mission.ПроверкаНовобранца, 2)) _barman.SetActive(true);
        else
        {
            if (SS.sv.Player.Name == Player.Genos) _barman.SetActive(false);
        }
    }
    public void ClickBarman()
    {
        if(SS.sv.Player.Name == Player.Genos)
        {
            Dialog.BuildWithDic("ГароуУзнать", () => { MissionManager.NextStep(Mission.ПроверкаНовобранца); });
        }
        else
        {
            MissionManager.Add(Mission.КулычныеБои);
            if (MissionManager.CrntStep(Mission.КулычныеБои, 0) || MissionManager.CrntStep(Mission.КулычныеБои, 1))
            {
                
                if (!Dialog.IsComplite("КулачныеБоиНачало"))
                {
                    Dialog.BuildWithDic("КулачныеБоиНачало", () => { MissionManager.SetStep(Mission.КулычныеБои, 1); Show.Loc.JumpLoc(Location.BarFight); });
                }
                else Dialog.BuildWithDic("КулачныйБой1", () => { Show.Loc.JumpLoc(Location.BarFight); });
            }
            else if (MissionManager.CrntStep(Mission.КулычныеБои, 2))
            {
                Dialog.BuildWithDic("КулачныйБой2", () => { Show.Loc.JumpLoc(Location.BarFight); });
            }
            else if (MissionManager.CrntStep(Mission.КулычныеБои, 3))
            {
                Dialog.BuildWithDic("КулачныйБой3", () => { Show.Loc.JumpLoc(Location.BarFight); });
            }
            else if (MissionManager.CrntStep(Mission.КулычныеБои, 4))
            {
                Dialog.BuildWithDic("КулачныйБой4", () => { Show.Loc.JumpLoc(Location.BarFight); });
            }
        }
        
        
    }
    public void ClickExit()
    {
        Show.Loc.JumpLoc(Location.BarLoc);
    }
}
