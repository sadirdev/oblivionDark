using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

[Serializable]
[CreateAssetMenu(fileName = "Tower 1", menuName = "ScrObj/Tower")]
public class TowerData : ScriptableObject
{
    public Block[] Blocks = new Block[1];
    public Mesh Mesh;

    public float[] Scale;
    
    public float DeltaRotate;
    public float DeltaPosition;



}


