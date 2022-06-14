using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

 
public class MissionList : MonoBehaviour
{
    public MissionManager.MissionsSS Missions;
    [SerializeField] private Image _imageBttn;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _titleName;
    [SerializeField] private TMP_Text _titleDescription;
    [SerializeField] private Sprite _selectSpriteBttn;
    [SerializeField] private GameObject _choiseText;

    


    private void Start()
    {

        

        _icon.sprite = Missions.MissionBlock.Icon;
        if (Missions.Select) 
        {
            _imageBttn.sprite = _selectSpriteBttn;
            _choiseText.SetActive(true);
        } 

        if (SS.sv.Lang == lng.rus)
        {
            _titleName.text = Missions.MissionBlock.TitleRus;
            _titleDescription.text = Missions.MissionBlock.Missions[Missions.CrntStep].DescriptionRus;
        }
        else if (SS.sv.Lang == lng.eng)
        {
            _titleName.text = Missions.MissionBlock.TitleEng;
            _titleDescription.text = Missions.MissionBlock.Missions[Missions.CrntStep].DescriptionEng;
        }
        else if (SS.sv.Lang == lng.por) 
        {
            _titleName.text = Missions.MissionBlock.TitlePor;
            _titleDescription.text = Missions.MissionBlock.Missions[Missions.CrntStep].DescriptionPor;
        } 

        

    }

    public void Click()
    {
        if (Missions.Select == true) return;
        foreach (var missions in SS.sv.ActiveMissions)
        {
            missions.Select = false;
        }
        
        Missions.Select = true;
        FindObjectOfType<MissionPanel>().GenerationMissionList();
        if (Missions.MissionBlock.Missions[Missions.CrntStep].NameLoc != Location.Start) FindObjectOfType<MissionManager>().AddSound.Play();
    }


}
