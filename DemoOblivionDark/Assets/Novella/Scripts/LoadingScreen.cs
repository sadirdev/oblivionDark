using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
   
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _progress;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private string _loadScene;
    [SerializeField] private Text _title;

    private AsyncOperation _async;
    private float time = 0;
    private bool _go = true;

    private void Start()
    {
        
        GetComponent<Canvas>().worldCamera = Camera.main;
        Screen.orientation = ScreenOrientation.Portrait;
        _async = SceneManager.LoadSceneAsync(_loadScene);
        _async.allowSceneActivation = false;
        SS.sv.CrntScene = CrntScene.FireBall;
        _title.text = "KEPLER_360 " + "LEVEL " +  ST.sv.LvlKepler.ToString();

        SS.Quit();
        ST.Quit();
    }


    private void Update()
    {
        if (_go)
        {
            time += Time.deltaTime * speed;
            _slider.value = time;

            if (time > 1)
            {
                _go = false;
                time = 0.99f;
            }
        }
        else
        {
            if (!_async.allowSceneActivation)
            {
                _progress.text = "100%";
                _async.allowSceneActivation = true;
            }

        }
        _progress.text = Mathf.Round(time * 100) + "%";
    }


    public enum CrntScene { Novella, FireBall}

}
