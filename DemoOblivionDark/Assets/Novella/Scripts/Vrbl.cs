using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DelegateVoid();
public enum Value {  YesNo, Location} // оепелеммше днаюбхрэ 
public enum Location {Start, Portal, CityLaboratory, Laboratory, CityZ, HomeBedroom, HomeKitchen, Waterfall, HomeHallway, Vigiriya, Cave, PradatoryPath, Etale, SuperMarket, North,
    Dump, HighwayA, HighwayB, Croaville, Bar, BarLoc, BarFight, PredatoryHome, Shinava,Kagiyar,
}
[Serializable]
public class Vrbl: MonoBehaviour
{
    


    public static List<VrblList> Vrbls = new List<VrblList>() // бюпхюмрш пегскэрюрнб й оепелеммшл днаюбхрэ
        {
            new VrblList(Value.Location, new string[] { "start", "portal", "cityLaboratory", "laboratory" }),
            new VrblList(Value.YesNo, new string[] {"null(1)", "yes(2)", "no(3)", "(4)", "(5)"}),

        };

    


    public static Dictionary<Value, VrblList> VrblsDic;

   

    



    private static Dictionary<Value, VrblList> DictionaryGenerate()
    {
        if(VrblsDic!= null) VrblsDic.Clear();
        var temp = new Dictionary<Value, VrblList>();
        //for (int i = 0; i < Vrbls.Count; i++)
        //{
        //    temp.Add(Vrbls[i].Name, ref Vrbls[i]);
        //}

        foreach (var vrbl in Vrbls)
        {
            temp.Add(vrbl.Name, vrbl);
        }
        return temp;
    }
    public static void UpdateVrbls()
    {
        if(VrblsDic == null) VrblsDic = DictionaryGenerate();

    }
    public static void Test()
    {
        VrblsDic = DictionaryGenerate();

    }
    public static string Get(Value value)
    {
        return VrblsDic[value].crnt;
    }
    public static void Set(Value value, string meaning)
    {
        
        VrblsDic[value].crnt = meaning;
    }


    [Serializable]
    public class VrblList
    {
        public Value Name;
        public string[] Set;
        public  string crnt;

        public VrblList(Value name , string[] set)
        {
            Name = name;
            Set = set;
        }
    }



   

   


}
