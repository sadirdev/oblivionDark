using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "ScrObj/Mission")]
public class MissionData : ScriptableObject
{
    public Player Player;
    public Mission Name;
    //public string Title;
    public string TitleRus;
    public string TitleEng;
    public string TitlePor;
    public bool Main;
    public Sprite Icon;
    public StepMission[] Missions;


    [System.Serializable]
    public class StepMission
    {
        //public string Description;
        public string DescriptionRus;
        public string DescriptionEng;
        public string DescriptionPor;
        public Location NameLoc;
    }
}
