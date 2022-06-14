using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "ScrObj/Item")]
public class ItemData : ScriptableObject
{
    [HideInInspector] public int Cound =1;
    public bool Grocery;
    public bool One;
    public string NameRus;
    public string NameEng;
    public string NamePor;
    public Sprite Sprite;
    public string DescroptionRus;
    public string DescroptionEng;
    public string DescroptionPor;
    [Space]
    [Range(1,4)]
    public int Lvl;
    public int Sale;
}
