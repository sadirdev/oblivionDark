using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.UI;

public class PlatformBall : MonoBehaviour
{
    //[SerializeField] private float _durationRotateTower;
    [SerializeField] private float _speedMove;
    private float _startY = -21;
    [SerializeField] private ObstacleRotator _obstecle1;
    [SerializeField] private ObstacleRotator _obstecle2;
    [SerializeField] private TowerBuilder _tower;
    //[SerializeField] private Transform _bridge;
    [SerializeField] private GameObject _kepler;
    [HideInInspector]public LvlData.Step CrntStep;
    [HideInInspector] public Slider Slider;

    //public Material material;

    public void Start()
    {
        if (transform.position.z == 0)
        {

            PlatformGame.Kepler = Instantiate(_kepler);
            PlatformGame.Kepler.transform.position = new Vector3(PlatformGame.Kepler.transform.position.x, _startY, PlatformGame.Kepler.transform.position.z);
            PlatformGame.Kepler.transform.DOMoveY(-1.1f, _speedMove);
        } 

        transform.position = new Vector3(transform.position.x, _startY, transform.position.z);

        transform.DOMoveY(0, _speedMove).OnComplete(() =>
        {
            if (transform.position.z == 0) Build();
          
        });




    }
    public void Build()
    {
        _tower.gameObject.SetActive(true);
        _tower.TowerSize = CrntStep.TowerSize;
        //_tower.TowerSize = 5; //потом удалить
        _tower.GetComponent<Tower>().Slider = Slider;
        StartCoroutine(_tower.Build());
        OstacleGenerate();
    }
    private void OstacleGenerate()
    {
        Instantiate(_obstecle1, transform).Pattern = CrntStep.Obstecles[0];
        if (CrntStep.Obstecles.Length > 1)
        {
            Instantiate(_obstecle2, transform).Pattern = CrntStep.Obstecles[1];
        }
    }
    //public void TowerGenerate()
    //{
    //    _tower.GetComponent<Tower>().Build();
    //}
   
    
    
    //public void UpdateObstecle()
    //{
    //    foreach (var item in FindObjectsOfType<ObstacleRotator>())
    //    {
    //        Destroy(item.gameObject);
    //    }
    //    FindObjectOfType<MenuBall>().Test();
    //    OstacleGenerate();
    //}
   
}
