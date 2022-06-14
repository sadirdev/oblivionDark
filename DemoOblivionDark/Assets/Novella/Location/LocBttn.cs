using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocBttn : MonoBehaviour
{
    public Bttn[] BttnObj;


    [System.Serializable]
    public class Bttn
    {
        public GameObject Obj;
         //public string Name;
        public string NameRus;
        public string NameEng;
        public string NamePor;
    }


}
