using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private GameObject _nofify;
    public AudioSource AddSound;
    [SerializeField] private MissionData[] _missions;

    public delegate void VoidMission(Mission name);
    public delegate void SetStepDel(Mission name, int index);
    public delegate bool CrntStepDel(Mission name, int index);
    public delegate bool IsCompliteDel(Mission name);

    public static VoidMission Add;
    public static CrntStepDel CrntStep;
    public static IsCompliteDel IsComplite;
    public static IsCompliteDel Cointains;
    public static VoidMission Complite;
    public static VoidMission NextStep;
    public static SetStepDel SetStep;

    private void Awake()
    {
         
        foreach (var actMiss in SS.sv.ActiveMissions)
        {
            foreach (var miss in _missions)
            {
                if (miss.Name == actMiss.Name)
                {
                    actMiss.MissionBlock = miss;
                }
            }
        }
    }
 

    private void Start()
    {
        Show.Mission = this;
        Add = AddVoid;
        CrntStep = CrntStepVoid;

        Cointains = ContainsVoid;
        IsComplite = IsCompliteVoid;
        Complite = CompliteVoid;
        NextStep = NextStepVoid;
        SetStep = SetStepVoid;
        UpdateMissions();
    }

    private void AddVoid(Mission name)
    {
        bool allFalse = true;
        
        foreach (Mission missionName in SS.sv.CompliteMissions)
        {
            if (name == missionName)
            {
                return;
            }

        }

        foreach (var missionSS in SS.sv.ActiveMissions)
        {
            if (missionSS.MissionBlock.Name == name ) { return; }
            
            if (missionSS.Select == true && missionSS.MissionBlock.Player == SS.sv.Player.Name) allFalse = false; 
        }

        foreach (var missionData in _missions)
        {
            if (missionData.Name == name)
            {
                SS.sv.ActiveMissions.Add(new MissionsSS(missionData, allFalse, name));
                if (SS.sv.CrntScene == LoadingScreen.CrntScene.Novella) 
                {
                    AddSound.Play();
                    _nofify.SetActive(true);
                }
                
                SS.sv.NofifyMission = true;
                UpdateGoalMap();
                SS.Quit();
            }

        }
        
        
    }

    private bool CrntStepVoid(Mission name, int index)
    {
       
        
        foreach (var missionSS in SS.sv.ActiveMissions)
        {
            if (missionSS.MissionBlock.Name == name)
            {
                if (index == missionSS.CrntStep) return true;
                break;
            }
        }
        return false;
    }
    private void SetStepVoid(Mission name, int index)
    {
        foreach (var missionSS in SS.sv.ActiveMissions)
        {
            if (missionSS.MissionBlock.Name == name)
            {
                missionSS.CrntStep = index;
                AddSound.Play();
                _nofify.SetActive(true);
                SS.sv.NofifyMission = true;
                SS.Quit();
                return;
            }
        }
    }
    private bool IsCompliteVoid(Mission name)
    {
        foreach (Mission missionName in SS.sv.CompliteMissions)
        {
            if (name == missionName) return true;
        }
        return false;
    }
    private bool ContainsVoid(Mission name)
    {
        foreach (var missionSS in SS.sv.ActiveMissions)
        {
            if (missionSS.MissionBlock.Name == name)
            {
                return true;
            }
        }
        foreach (var missionSS in SS.sv.CompliteMissions)
        {
            if (missionSS == name)
            {
                return true;
            }
        }
        return false;
    }

    private void CompliteVoid(Mission name)
    {

        if (SS.sv.CompliteMissions.Contains(name)) return;
        Debug.Log("Миссия завершена");
        foreach (var mission in SS.sv.ActiveMissions)
        {
            if (mission.MissionBlock.Name == name)
            {

                SS.sv.ActiveMissions.Remove(mission);
                SS.sv.CompliteMissions.Add(name);
                SS.Quit();
                break;
            }
        }
        UpdateGoalMap();
    }
    private void NextStepVoid(Mission name)
    {
        foreach (var mission in SS.sv.ActiveMissions)
        {
            if (mission.MissionBlock.Name == name)
            {
                if (mission.MissionBlock.Missions.Length - 1 == mission.CrntStep)
                {
                    CompliteVoid(name);
                    return;
                }
                AddSound.Play();
                _nofify.SetActive(true);
                SS.sv.NofifyMission = true;
                mission.CrntStep++;
                SS.Quit();
            }
        }
        UpdateGoalMap();
    }


    public void UpdateGoalMap()
    {
      
        if (!(SS.sv.ActiveMissions.Count > 0))
        {
            SS.sv.CrntGoal = Location.Start;
            return;
        }

        foreach (var mission in SS.sv.ActiveMissions)
        {
            if (mission.Select && SS.sv.Player.Name == mission.MissionBlock.Player)
            {
                SS.sv.CrntGoal = mission.MissionBlock.Missions[mission.CrntStep].NameLoc;
                return;
            }

        }
        SS.sv.CrntGoal = Location.Start;
        foreach (var mission in SS.sv.ActiveMissions)
        {
            if(SS.sv.Player.Name == mission.MissionBlock.Player && !mission.Select)
            {
                mission.Select = true;
                SS.sv.CrntGoal = mission.MissionBlock.Missions[mission.CrntStep].NameLoc;
                return;
            }
            
        }
    }

  
   
    public static void UpdateMissions()
    {
        if (SS.sv.CrntScene == LoadingScreen.CrntScene.Novella) return;
        if (ST.sv.LvlKepler == 1)
        {
            //SS.sv.Player = SS.sv.Virus;
            //ST.sv.NewCharacter = true;
        }
        else if (ST.sv.LvlKepler >= 2 && CrntStep(Mission.Kepler_2lvl,0))
        {
            Debug.Log($"CrntStep(Mission.Kep_3lvl_DarkHero,3) = {CrntStep(Mission.Kep_3lvl_DarkHero, 3)}");
            GetPlayer(Player.Genos);
            
            SS.sv.CrntLoc = Location.Portal;
            ST.sv.NewCharacter = true;
        }
        else if (ST.sv.LvlKepler >= 3 && CrntStep(Mission.Kep_3lvl_DarkHero,3))
        {
            GetPlayer(Player.Virus);
            Complite(Mission.Kep_3lvl_DarkHero);
            Add(Mission.Kep_4lvl_ВирусШтаб);
            ST.sv.NewCharacter = true;
        }
        else if (ST.sv.LvlKepler >= 4 && CrntStep(Mission.Kep_4lvl_ВирусШтаб,4))
        {

            GetPlayer(Player.Genos);
            Complite(Mission.Kep_4lvl_ВирусШтаб);
            Debug.Log($"SS.sv.Player.Name = {SS.sv.Player.Name}");
            Add(Mission.Kep_5lvl_ГеносСпасиДев);
            ST.sv.NewCharacter = true;

        }
        else if (ST.sv.LvlKepler >= 5 && CrntStep(Mission.Kep_5lvl_ГеносСпасиДев,4))
        {
            GetPlayer(Player.Virus);
            Complite(Mission.Kep_5lvl_ГеносСпасиДев);
            Add(Mission.Kep_6lvl_ВирусПриставка);
            ST.sv.NewCharacter = true;
        }
        else if (ST.sv.LvlKepler >= 6 && CrntStep(Mission.Kep_6lvl_ВирусПриставка,4))
        {
            SS.sv.ActivePortal = false;
            GetPlayer(Player.Genos);
            Complite(Mission.Kep_6lvl_ВирусПриставка);
            Add(Mission.Kep_7lvl_ГеносМумен);
            ST.sv.NewCharacter = true;
        }
    }
    public static void GetPlayer(Player player)
    {
        if (player == SS.sv.Player.Name) return;
        if(player == Player.Genos)
        {
            SS.sv.Virus = SS.sv.Player;
            SS.sv.Virus.Name = Player.Virus;
            SS.sv.Player = SS.sv.Genos;
            
        }
        else
        {
            SS.sv.Genos = SS.sv.Player;
            SS.sv.Genos.Name = Player.Genos;
            SS.sv.Player = SS.sv.Virus;
            
        } 
        if(LvlManager.Static != null) LvlManager.Static.Start();

    }

    [System.Serializable]
    public class MissionsSS
    {
        public Mission Name;
        public MissionData MissionBlock;
        public int CrntStep = 0;
        public bool Select = false;
        public MissionsSS(MissionData missionData, bool select, Mission name)
        {
            MissionBlock = missionData;
            Select = select;
            Name = name;
        }
    }
    

}

public enum Mission { LostIdentity, Kepler_2lvl, Kep_3lvl_DarkHero, ПопастьВЛабораторию, ЛютыйГолод, Kep_4lvl_ВирусШтаб, Kep_5lvl_ГеносСпасиДев, Kep_6lvl_ВирусПриставка, Kep_7lvl_ГеносМумен, КулычныеБои, НепокорныйСундук, ПроверкаНовобранца, КомпасПирата, КоролеваПауков}