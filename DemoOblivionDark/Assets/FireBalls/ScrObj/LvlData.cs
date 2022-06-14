using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

[Serializable]
[CreateAssetMenu(fileName = "TowerLvl 1", menuName = "ScrObj/TowerLvl")]
public class LvlData : ScriptableObject
{
    public static LvlData CrntLvl;
    //public static int CrntStep;

    public Step[] LvlStep;
    public TowerData Tower;
    //public Block[] Blocks = new Block[1];
    public Color[] Colors;


    [Serializable]
    public class Step
    {
        public int TowerSize;
        public PatternObst[] Obstecles = new PatternObst[1];
        
    }

    [Serializable]
    public class PatternObst
    {
        public Transform PatternObj;
        public float Degrees;
        public float Duration;
        public float Delay;
        public LoopType LoopType;
        public Ease Ease;
        public MoveMulty[] MoveMulty = null;
    }

    [Serializable]
    public class MoveMulty
    {
        public float Degrees;
        public float Duration;
        public Ease Ease;
    }



}


