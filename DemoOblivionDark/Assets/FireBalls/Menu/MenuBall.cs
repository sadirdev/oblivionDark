using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuBall : MonoBehaviour
{
    [HideInInspector]public BuildPanelType PanelType;
    [SerializeField] private Transform _contentPanel;
    [SerializeField] private GameObject _bgMenu;
    [SerializeField] private GameObject _panelPlay;
    [SerializeField] private PanelChoice _panelChoice;
    [SerializeField] private PanelInfo _panelInfo;
    [SerializeField] private LoadingScreenNovella _panelLoading;
    [SerializeField] private BlockGun _blockGun;

    //int test = -1;

    //[SerializeField] private ParticleSystemRenderer particleTest;
    //private ParticleSystemRenderer teptParticle;

    [SerializeField] private PlatformGame _platformGame;
    public Sprite[] Nambers;

    [SerializeField] private LvlData[] _allLVL;

    [SerializeField] private int _setLvl;


    //public void TestMesh()
    //{
    //    if (teptParticle != null) Destroy(teptParticle.gameObject);
    //    test++;
    //     teptParticle = Instantiate(particleTest);


    //    Debug.Log("Нажатие на кнопку до");
    //    teptParticle.mesh = _allLVL[test].Tower.Blocks[0].GetComponent<MeshFilter>().sharedMesh;
    //    Debug.Log("Нажатие на кнопку после");

    //    FindObjectOfType<PlatformGame>().TestTextBttn.text = _allLVL[test].Tower.Blocks[0].GetComponent<MeshFilter>().sharedMesh.name;
    //    //Debug.Log($"sharedMesh.name = {_allLVL[test].Tower.Blocks[0].GetComponent<MeshFilter>().sharedMesh.name}");
       
    //}



    private void Start()
    {
        MenuBuild(BuildPanelType.Play);
    }

    public void MenuBuild(BuildPanelType type)
    {
        EdMob.HideBanner();
        StartCoroutine(playSound());
        PanelType = type;
        if (type != BuildPanelType.Next)
        {
            
            GetComponent<Animation>().Play("OpenMenu"); // затемнение экрана и появление меню
        }
            
        else
        {
            nextPanel();
            FindObjectOfType<BGBall>().ReturnColor();
            return;
        }
        
        _bgMenu.SetActive(true);
        

        void nextPanel()
        {
            Instantiate(_panelPlay, _contentPanel).GetComponent<PanelMenuBall>().mb = this; 
            
        }

        IEnumerator playSound()
        {
            yield return new WaitForSeconds(0.7f);
            GetComponent<AudioSource>().Play();
        }
    }


    public void JumpScene()
    {
        Instantiate(_panelLoading, _contentPanel);
    }
    public void BuilChoice()
    {
        DestroyPanelContent();
        Instantiate(_panelChoice, _contentPanel);
    }
    public void BuildPanel() // Вызываеться в event в анимации
    {
        DestroyPanelContent();
        Instantiate(_panelPlay, _contentPanel).GetComponent<PanelMenuBall>().mb = this;
    }
    public void BuildInfo()
    {
        DestroyPanelContent();
        Instantiate(_panelInfo, _contentPanel);
    }
    private void DestroyPanelContent()
    {
        foreach (Transform panel in _contentPanel)
        {
           
            if (panel.TryGetComponent(out CanvasGroup canvasGroup))
            {
                canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
                {
                    Destroy(panel.gameObject);
                });
            }
        }
    }
   
        
    
    public void Play()
    {
        if(ST.sv.LvlKepler>1) EdMob.ShowBanner();
        if(_setLvl != 0) ST.sv.LvlTowerView = _setLvl;

        if (_allLVL.Length <= ST.sv.LvlTowerView) RandomLvlGenerate();
        else LvlData.CrntLvl = _allLVL[ST.sv.LvlTowerView-1];
        FindObjectOfType<BGBall>().SetColor(LvlData.CrntLvl.Colors[0]);
        //var platform = FindObjectOfType<PlatformBall>();
        //if (platform != null) Destroy(platform.gameObject);
        Instantiate(_platformGame).AllLvl = _allLVL;
        DestroyPanel();
    }
    public void Resume()
    {
        
        DestroyPanel();
        FindObjectOfType<LvlProgress>().UI.SetActive(true);
        Instantiate(_blockGun, _contentPanel);
        //Tank.BulletGun = true;
    }
    

    void DestroyPanel()
    {
        foreach (Transform panel in _contentPanel)
        {
            Destroy(panel.gameObject);
        }
        _bgMenu.gameObject.SetActive(false);
        //foreach (Transform panel in transform)
        //{
        //    if(panel.TryGetComponent<PanelMenuBall>( out PanelMenuBall c)) Destroy(panel.gameObject);

        //}
    }
    public void Test()
    {
        int coundBlock = _allLVL.Length - 1;
        LvlData.CrntLvl = _allLVL[coundBlock];
    }
    private void RandomLvlGenerate()
    {
        
        if(ST.sv.ReservLvl.Count == 0)
        {
            Debug.LogWarning("РандомГенерация");
            for (int i = 9; i < _allLVL.Length; i++)
            {
                ST.sv.ReservLvl.Add(i);
            }
        }

        int indexRand = ST.sv.ReservLvl[Random.Range(0, ST.sv.ReservLvl.Count)];
        Debug.Log($"Зарандомился Tower {indexRand+1}");


        LvlData.CrntLvl = _allLVL[indexRand];
        ST.sv.ReservLvl.RemoveAt(indexRand);


    }


    public enum BuildPanelType { Play, Restart, Next}
}
