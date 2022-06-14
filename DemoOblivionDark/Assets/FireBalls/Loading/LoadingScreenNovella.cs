using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenNovella : MonoBehaviour
{
    private AsyncOperation _async;
    [SerializeField] private string _loadScene;
    [SerializeField] private GameObject OPBlack;
    [SerializeField] private GameObject OPWhite;
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        if(SS.sv.Player.Name == Player.Genos)
        {
            OPBlack.SetActive(true);
            OPWhite.SetActive(false);
        }
        else
        {
            OPBlack.SetActive(false);
            OPWhite.SetActive(true);
        }
        _async = SceneManager.LoadSceneAsync(_loadScene);
        _async.allowSceneActivation = false;
        if(ST.sv.LvlKepler == 2 && !MissionManager.IsComplite(Mission.Kepler_2lvl))
        {
            SS.sv.CrntLoc = Location.Portal;
        }


        StartCoroutine(StartScene());
        ST.Quit();
        SS.Quit();
    }
    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(5);
        _async.allowSceneActivation = true;
    }
}
