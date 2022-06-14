using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelChoice : MonoBehaviour
{
    [SerializeField] private Color _yellow;
    [SerializeField] private Image _frameVirus;
    [SerializeField] private Image _frameGenos;
    private MenuBall _menuBall;




    void Start()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);
        _menuBall = FindObjectOfType<MenuBall>();

        Debug.Log($"SS.sv.Player.Name = {SS.sv.Player.Name}");
        Debug.Log($"SS.sv.Virus.Name = {SS.sv.Virus.Name}");
        Debug.Log($"SS.sv.Genos.Name = {SS.sv.Genos.Name}");


        if (ST.sv.NewCharacter)
        {
            if (SS.sv.Player.Name == Player.Virus) _frameVirus.DOColor(_yellow, 0.5f).SetLoops(-1, LoopType.Yoyo);
            else _frameGenos.DOColor(_yellow, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
       

    }

    public void ClickKepler()
    {
        
        if (_menuBall.PanelType == MenuBall.BuildPanelType.Next) _menuBall.PanelType = MenuBall.BuildPanelType.Play;
        _menuBall.BuildPanel();
    }

    public void ClickGenos()
    {
        if (SS.sv.Player.Name == Player.Virus || !SS.sv.ActiveInterface)
        {
            _menuBall.BuildInfo();
            return;
        }
            

        SS.sv.CrntScene = LoadingScreen.CrntScene.Novella;
        ST.sv.NewCharacter = false;
        FindObjectOfType<MenuBall>().JumpScene();
        
    }

    public void ClickVirus()
    {
        if (SS.sv.Player.Name == Player.Genos || !SS.sv.ActiveInterface)
        {
            _menuBall.BuildInfo();
            return;
        }
            

        SS.sv.CrntScene = LoadingScreen.CrntScene.Novella;
        ST.sv.NewCharacter = false;
        FindObjectOfType<MenuBall>().JumpScene();
        
    }

   

}
