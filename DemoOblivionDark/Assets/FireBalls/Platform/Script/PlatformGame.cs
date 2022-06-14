using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlatformGame : MonoBehaviour
{
    [SerializeField] private TestColor _test;
    [SerializeField] private PlatformBall _platformObj;
    [SerializeField] private Transform _position2;
    [SerializeField] private Transform _position3;
    [SerializeField] private Transform _centerRotate;
    [SerializeField] private Transform _sliderGroup;
    [SerializeField] private Slider _slider;
    [SerializeField] private ParticleSystemRenderer _blockEffect;
    [SerializeField] public Text TestTextBttn;
    private ParticleSystem _particleFon;
    private Transform _centerRotateTemp;
    [HideInInspector] public LvlData[] AllLvl;
    private Sequence _sequence;
    private float _speedMoveKepler = 0.5f;
    private int _crntStep = 1;
    private ParticleSystem _particleFonInstate;

    public static List<PlatformBall> PlatformBalls;
    public static GameObject Kepler;

    private void Start()
    {
        _particleFon = FindObjectOfType<BGBall>().FonGenerate();
        PlatformBalls = new List<PlatformBall>();
        PlatformBall platform;
        platform = Instantiate(_platformObj, transform);
        platform.CrntStep = LvlData.CrntLvl.LvlStep[0];
        _blockEffect.mesh = LvlData.CrntLvl.Tower.Blocks[0].GetComponent<MeshFilter>().sharedMesh;


        //_bgBall.FonGame.DOColor(LvlData.CrntLvl.Colors[0], 0.5f);
        //Debug.Log($"LvlData.CrntLvl.Colors[0].r{LvlData.CrntLvl.Colors[0].r*255}");
        _particleFon.startColor = new Color(LvlData.CrntLvl.Colors[0].r+0.06f, LvlData.CrntLvl.Colors[0].g+0.086f, LvlData.CrntLvl.Colors[0].b-0.2f, _particleFon.startColor.a);
        _particleFonInstate = Instantiate(_particleFon, transform);
        platform.Slider = Instantiate(_slider, _sliderGroup);
        PlatformBalls.Add(platform);
        //platform = new PlatformBall();

        //_testTextBttn.text = _blockEffect.mesh.name;
        if (LvlData.CrntLvl.LvlStep.Length > 1)
        {
            platform = Instantiate(_platformObj, transform);
            platform.transform.rotation = _position2.rotation;
            platform.transform.position = _position2.position;
           
            platform.CrntStep = LvlData.CrntLvl.LvlStep[1];
            platform.Slider = Instantiate(_slider, _sliderGroup);
            PlatformBalls.Add(platform);
            //platform = new PlatformBall();
        }
        if (LvlData.CrntLvl.LvlStep.Length > 2) 
        {
            platform = Instantiate(_platformObj, transform);
            platform.transform.rotation = _position3.rotation;
            platform.transform.position = _position3.position;
            platform.CrntStep = LvlData.CrntLvl.LvlStep[2];
            platform.Slider = Instantiate(_slider, _sliderGroup);
            PlatformBalls.Add(platform);
            //platform = new PlatformBall();
        }
        for (int i = 3; i < LvlData.CrntLvl.LvlStep.Length; i++)
        {
            platform = Instantiate(_platformObj, transform);
            if (i % 2 == 0) // четное
            {
                platform.transform.position = new Vector3(_position3.position.x, _position3.position.y,  18 * i); 
                platform.transform.rotation = _position3.rotation;
            }
            else// Не четное
            {
                
                platform.transform.position = new Vector3(_position2.position.x, _position2.position.y , _position2.position.z + 18 * (i-1));
                platform.transform.rotation = _position2.rotation;
            }
            
            platform.CrntStep = LvlData.CrntLvl.LvlStep[i];
            platform.Slider = Instantiate(_slider, _sliderGroup);
            PlatformBalls.Add(platform);
        }


        //if (LvlData.CrntLvl.LvlStep.Length > 3) Instantiate(_platformObj, transform);
    }
    
    public void ComplateLvl()
    {
        
        transform.GetChild(0).gameObject.SetActive(false);

        _centerRotateTemp = Instantiate(_centerRotate);
        transform.parent = _centerRotateTemp;
        Kepler.transform.parent = _centerRotateTemp;

        //Kepler.transform.DOMove(new Vector3(0, -40, -21), 2);
        _centerRotateTemp.transform.DOMove(new Vector3(0, -40, -31), 2*_speedMoveKepler).OnComplete(() => { Destroy(_centerRotateTemp.gameObject); });
    }
    public void CompliteStep()
    {
        _centerRotateTemp = Instantiate(_centerRotate);
        
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOMoveZ(-12.5f + transform.position.z, 2*_speedMoveKepler).SetEase(Ease.Linear));
        if(_crntStep % 2 != 0) 
        {
            transform.parent = _centerRotateTemp;
            int rotate = 36;
            if (_crntStep > 1) rotate = 72;
            
            _sequence.Append(_centerRotateTemp.transform.DORotate(new Vector3(0, rotate, 0), 1*_speedMoveKepler, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(LastMove));
        }
        else 
        {
            transform.parent = _centerRotateTemp;
            _sequence.Append(_centerRotateTemp.transform.DORotate(new Vector3(0, -72, 0), 1*_speedMoveKepler, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(LastMove));
        }
       
    }

    private void LastMove()
    {
        _particleFonInstate.transform.position = _particleFon.transform.position;
        _particleFonInstate.transform.rotation = _particleFon.transform.rotation;
        transform.parent = null;
        _crntStep++;
        transform.DOMoveZ(-9.5f + transform.position.z, 2 * _speedMoveKepler).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(PlatformBalls[0].gameObject);
            PlatformBalls.RemoveAt(0);
            PlatformBalls[0].Build();
            Destroy(_centerRotateTemp.gameObject);
        });
    }

    public void UpdateLvl()
    {

        //FindObjectOfType<MenuBall>().TestMesh();



        Destroy(gameObject);

        Destroy(FindObjectOfType<Tank>().gameObject);
        FindObjectOfType<MenuBall>().Play();

        //List<Color> colors = new List<Color>();
        //int colorIndex = -1;
        //for (int i = 0; i < 12; i++)
        //{
        //    colors.Add(AllLvl[i].Colors[0]);
        //}
        //for (int i = 12; i < AllLvl.Length; i++)
        //{
        //    colorIndex++;
        //    if (colors.Count == colorIndex) colorIndex = 0;
        //    AllLvl[i].Colors[0] = colors[colorIndex];
        //}

        Instantiate(_test, transform.GetChild(0)).AllLvl = AllLvl;
    }
}
