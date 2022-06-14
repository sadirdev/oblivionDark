using System;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ST : MonoBehaviour
{

    public static Save sv = new Save();
    private static string path;

    void Awake()
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "SaveT.json");
#else
        path = Path.Combine(Application.dataPath, "SaveT.json");
#endif

        
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        }
        else //первый запуск (Когда нет Save)
        {
          
        }
      
    }
    //void Update()
    //{
    //    Debug.Log($"SS.sv.CrntScene = {SS.sv.CrntScene}");
    //}
  

    [Serializable]
    public class Save
    {
        public int LvlTowerView = 1;
        public List<int> ReservLvl = new List<int>();

        public float ExpT360;
        public int LvlKepler = 1;
        public float FillProgress;
        public bool NewCharacter = false;
        public bool TutorialView = true;

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
        string json = JsonUtility.ToJson(sv);
        File.WriteAllText(path, json);
    }


    

}





