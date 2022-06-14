using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(TowerBuilder))]
public class Tower : MonoBehaviour
{
    public Slider Slider;
    public PlatformBall Platform;
   
    [SerializeField] private TMP_Text _sizeView;
    private int _towerSize;
    private List<Block> _blocks;

    //public event UnityAction<int> SizeUpdate;

    private void Start()
    {
        transform.DORotate(new Vector3(270, 0, 360), 10, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);


    }

    //public void Build()
    //{
    //    ExpAddBall.CrntPoitnExp = 0;
    //    _towerBulder = GetComponent<TowerBuilder>();
    //    StartCoroutine(_towerBulder.Build());
    //}
    public void BuildList(List<Block> blocks)
    {
        
        _blocks = blocks;


        foreach (var block in _blocks)
        {
            block.BulletHit += OnBulletHit;
        }
       
        _towerSize = _blocks.Count;
       
        
    }
    private void OnBulletHit(Block hitedBlock)
    {
        hitedBlock.BulletHit -= OnBulletHit;
        _blocks.Remove(hitedBlock);
        //foreach (var block in _blocks)
        //{
        //    block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y - block.transform.localScale.z, block.transform.position.z);
        //}
        transform.position = new Vector3(transform.position.x, transform.position.y - hitedBlock.transform.localScale.z-0.05f, transform.position.z);
        TowerSizeUpdated();
        LvlProgress.PointUpdate();
        _sizeView.text = _blocks.Count.ToString();

        //Когда завершил шаг
        if (_blocks.Count == 0)
        {
            StartCoroutine(completeLvl());
        }
        IEnumerator completeLvl()
        {
            Tank.BulletGun = false;
            
            foreach (var item in FindObjectsOfType<ObstacleRotator>())
            {
                item.StopRotate();
            }
            
            yield return new WaitForSeconds(1.5f);
            
            if(PlatformGame.PlatformBalls.Count>1)
            {
                CompliteStep();
            }
            else
            {
                CompliteLvl();
            }
        }
        
    }
    public void TowerSizeUpdated()
    {
        
        Slider.DOValue((_towerSize - _blocks.Count) / (float)_towerSize, 0.3f);
    }
    private void CompliteLvl()
    {
        FindObjectOfType<MenuBall>().MenuBuild(MenuBall.BuildPanelType.Next);
        FindObjectOfType<PlatformGame>().ComplateLvl();
    }
    private void CompliteStep()
    {
        FindObjectOfType<PlatformGame>().CompliteStep();
    }
}
