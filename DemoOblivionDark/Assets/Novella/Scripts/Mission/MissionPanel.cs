using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MissionPanel : MonoBehaviour
{
    
    
    [SerializeField] private MissionList _prefabMissionList;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _secondary;

    private CanvasGroup _canvasGroup;


    
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 0.3f);
        GenerationMissionList();


    }

    public void GenerationMissionList()
    {
        foreach (Transform item in _content)
        {
            Destroy(item.gameObject);
        }

        List<MissionManager.MissionsSS> secondaryMis = new List<MissionManager.MissionsSS>();


        foreach (var mission in SS.sv.ActiveMissions)
        {

            if (mission.MissionBlock.Player == SS.sv.Player.Name)
            {
                if (mission.MissionBlock.Main)
                {
                    MissionList button = Instantiate(_prefabMissionList, _content);
                    button.Missions = mission;

                }
                else
                {
                    secondaryMis.Add(mission);
                }
            }

        }

        if (secondaryMis.Count > 0) Instantiate(_secondary, _content);


        foreach (var mission in secondaryMis)
        {

            MissionList button = Instantiate(_prefabMissionList, _content);
            button.Missions = mission;

        }
    }

    public void ClosePanel()
    {
        FindObjectOfType<MissionManager>().UpdateGoalMap();
        _canvasGroup.DOFade(0, 0.3f).OnComplete(() => { Destroy(gameObject); });

        
    }

}
