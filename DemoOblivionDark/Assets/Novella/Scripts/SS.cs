using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

public class SS : MonoBehaviour
{

    public static Save sv = new Save();
    private static string path;
    private AsyncOperation _async;

    void Awake()
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif

        
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            Vrbl.Vrbls = sv.SaveVrbls;
        }
        else //������ ������ (����� ��� Save)
        {
            Debug.Log($"Application.systemLanguage = {Application.systemLanguage}");
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                sv.Lang = lng.rus;
            }
            else if (Application.systemLanguage == SystemLanguage.Portuguese || Application.systemLanguage == SystemLanguage.Spanish)
            {
                sv.Lang = lng.por;
            }
            else
            {
                sv.Lang = lng.eng;
            }

           
            Vrbl.UpdateVrbls();
        }
        //Debug.Log($"sv.CrntScene ={ sv.CrntScene }");


        if (SceneManager.GetActiveScene().name == "FireBalls")
        {
            Screen.orientation = UnityEngine.ScreenOrientation.Portrait;
            sv.CrntScene = LoadingScreen.CrntScene.FireBall;
        }

        if (sv.CrntScene == LoadingScreen.CrntScene.Novella) Screen.orientation = ScreenOrientation.LandscapeLeft;
        if (sv.CrntScene == LoadingScreen.CrntScene.FireBall && SceneManager.GetActiveScene().name != "FireBalls")
        {
            Screen.orientation = ScreenOrientation.Portrait;
            _async = SceneManager.LoadSceneAsync("FireBalls");
        }
        Vrbl.UpdateVrbls();
    }
    private void Start()
    {
        if(sv.Player == null) sv.Player = sv.Genos;
        
    }
 



    [Serializable]
    public class Save
    {
        public List<Vrbl.VrblList> SaveVrbls = new List<Vrbl.VrblList>();
        public List<Dialog.DialogNameAndCheck> Dialogs = new List<Dialog.DialogNameAndCheck>();
        public List<Clicker.EnemyEnum> ComliteEnemy = new List<Clicker.EnemyEnum>();

        public LoadingScreen.CrntScene CrntScene = LoadingScreen.CrntScene.Novella;
        public ClickerIconPlayer.Specifications Player;
        public Location CrntLoc = Location.Start;
        public Location CrntGoal;

        public  ClickerIconPlayer.Specifications Genos = new ClickerIconPlayer.Specifications(global::Player.Genos);
        public  ClickerIconPlayer.Specifications Virus = new ClickerIconPlayer.Specifications(global::Player.Virus);

        public List<MissionManager.MissionsSS> ActiveMissions = new List<MissionManager.MissionsSS>();
        public List<Mission> CompliteMissions = new List<Mission>();
        //public float GenosHP = 1;
        //public float GenosEXP = 0;
        //public int GenosLVL = 1;
        public int Gem = 5;
        public int SecondsReward = 0;

        public bool BitaGenos = false;
        public bool BitaVirus = false;
        public bool DonActive = false;

        public bool ActivePortal = true;
        public bool ActiveInterface = false;
        public bool ActiveIconPlayer = false;
        public bool NofifyInventory = false;
        public bool NofifyMission = false;

        public float MusicFill = 0.92f;
        public float SoundFill = 1;
        public lng Lang = lng.rus;


        public int testAdsCoundGenos;
        public int testAdsCoundVirys;
    }

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
        {
            if (pause) OnApplicationQuit();
        }
#endif

    private void OnApplicationQuit()
    {
        Quit();
    }
    public static void Quit()
    {

    sv.SaveVrbls = Vrbl.Vrbls;
    string json = JsonUtility.ToJson(sv);
    File.WriteAllText(path, json);
    }



}



public enum lng { rus, eng, por}

