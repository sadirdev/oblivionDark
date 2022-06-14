using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExpAddBall : MonoBehaviour
{
    //private PanelMenuBall _panelMenu;
    [SerializeField] private ProgressLvlT360 _sliderProgress;
    [SerializeField] private Text _exp;
    [SerializeField] private Text _expAdd;
    [HideInInspector] public static float CrntPoitnExp; // Количество очков для анимации(которое перейдёт в общий счет)
    private AudioSource _audio;
    
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        if (_exp != null) _exp.text = ST.sv.ExpT360.ToString();
        if (_expAdd != null) _expAdd.text = CrntPoitnExp.ToString();
        StartCoroutine(delayAnim());

        IEnumerator delayAnim()
        {
            _sliderProgress.Add(1.3f); // Включием анимацию слайдера
            //yield return new WaitForSeconds(0.6f);
            FindObjectOfType<PanelMenuBall>().LvlGenerate();
            float tempAdd = CrntPoitnExp / 100;
            //int tempAdd =(int)Mathf.Ceil(CrntPoitnExp / 200);
            float tempPoint = ST.sv.ExpT360;
            ST.sv.ExpT360 += CrntPoitnExp;
            float test = 0;
            
            _audio.Play();
            while (CrntPoitnExp > 0)
            {
                CrntPoitnExp -= tempAdd;
                tempPoint += tempAdd;
                _exp.text = ((int)tempPoint).ToString();
                _expAdd.text = ((int)CrntPoitnExp).ToString();
                test += Time.deltaTime;
                yield return new WaitForSeconds(0.015f);
            }
            _audio.Stop();
            _exp.text = ST.sv.ExpT360.ToString();
            _sliderProgress.Handle.enabled = false;
            GetComponent<CanvasGroup>().DOFade(0, 1);
        }
    }


  

}
