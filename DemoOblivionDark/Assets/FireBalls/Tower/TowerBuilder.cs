using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerBuilder : MonoBehaviour
{
    

    [HideInInspector]public int TowerSize;

    private float _speedBuild =0.1f;
    [SerializeField] private Transform _buildPoint;
    [SerializeField] private TMP_Text _towerSizeView;
    private int _indexColor = 0;
    private int _indexBlock = 0;
    private int _indexScale = 0;
    private bool _deltaPositionMove = false;

    private List<Block> _blocks;
   
    public IEnumerator Build()
    {
        _blocks = new List<Block>();
        //var sizeView = FindObjectOfType<TowerSizeView>().SizeView;
       
        Transform currentPoint = _buildPoint;

        float speedBuild = _speedBuild;
        for (int i = 0; i < TowerSize; i++) 
        {
            yield return new WaitForSeconds(speedBuild);
            speedBuild -= 0.005f;
            Block newBlock = BuildBlock(currentPoint);
            newBlock.SetColor(LvlData.CrntLvl.Colors[_indexColor]);
           
            newBlock.SetScale(LvlData.CrntLvl.Tower.Scale[_indexScale]);
            AlternateIndexScale();
            

            
            AlternateIndexColor();
            AlternateIndexBlock();
            _blocks.Add(newBlock);
            currentPoint = newBlock.transform;
            _towerSizeView.text = _blocks.Count.ToString();
        }
        if (ST.sv.TutorialView) 
        {
            FindObjectOfType<LvlProgress>().TutorialBuild();
            ST.sv.TutorialView = false;
        } 
        else Tank.BulletGun = true;
        ResumeButton.ShowAds = true;
        
        GetComponent<Tower>().BuildList(_blocks);
    }
    void AlternateIndexColor()
    {
        _indexColor++;
        if (_indexColor == LvlData.CrntLvl.Colors.Length) _indexColor= 0;
    }
    void AlternateIndexBlock()
    {
        _indexBlock++;
        if (_indexBlock == LvlData.CrntLvl.Tower.Blocks.Length) _indexBlock = 0;
    }
    void AlternateIndexScale()
    {
        _indexScale++;
        if (_indexScale == LvlData.CrntLvl.Tower.Scale.Length) _indexScale = 0;
    }


    private Block BuildBlock(Transform currentBuildPoint)
    {
        return Instantiate(LvlData.CrntLvl.Tower.Blocks[_indexBlock], GetBuildPoint(currentBuildPoint), Quaternion.Euler(currentBuildPoint.transform.eulerAngles.x, currentBuildPoint.transform.eulerAngles.y+ LvlData.CrntLvl.Tower.DeltaRotate, currentBuildPoint.transform.eulerAngles.z), _buildPoint);
    }

    private Vector3 GetBuildPoint(Transform  currentSegment)
    {
        if (_deltaPositionMove)
        {
            _deltaPositionMove = false;
            return new Vector3(_buildPoint.position.x  + LvlData.CrntLvl.Tower.DeltaPosition, currentSegment.position.y + currentSegment.localScale.z+0.05f, _buildPoint.position.z);
        }
        else
        {
            _deltaPositionMove = true;
            return new Vector3(_buildPoint.position.x, currentSegment.position.y + currentSegment.localScale.z+0.05f, _buildPoint.position.z);
        }
        
    }
}
