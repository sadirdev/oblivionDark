using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LvlProgress : MonoBehaviour
{

    //public Slider Slider;
    //[SerializeField] private Tower _tower;
    [SerializeField] private Text _pointCounter;
    [SerializeField] private Bonus _bonus;
    [SerializeField] private TutorialClick _tutorialClick;
    //[HideInInspector]public float CrntTowerSize;
    public GameObject UI;
    public static DelegateVoid Bonus;
    public static DelegateVoid PointUpdate;

    private void Start()
    {
        ExpAddBall.CrntPoitnExp = 0;
        Bonus = BonusVoid;
        PointUpdate = PointUpdateVoid;
    }


    public void TutorialBuild()
    {
        Instantiate(_tutorialClick, transform);
    }

    private void PointUpdateVoid()
    {


        

        ExpAddBall.CrntPoitnExp += (int)Tank.Bonus;
        _pointCounter.text = ExpAddBall.CrntPoitnExp.ToString();
    }
    private void BonusVoid()
    {
        int bonus = 10;
        ExpAddBall.CrntPoitnExp += bonus;

        Instantiate(_bonus, transform).Cound.text ="+" + bonus.ToString();
    }
}
