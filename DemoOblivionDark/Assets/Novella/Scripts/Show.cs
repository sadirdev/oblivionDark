using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Show
{
    public static MissionManager Mission;
    public static LocMngr Loc;
    public static Dialog Dialog;
    public static Build Build;
    public static ClickerIconPlayer IconPlayer;

    public static void FadeImg(Image image )
    {
        if(image.color.a == 0) image.DOFade(1, DialogFon.DurationFade);
        else image.DOFade(0, DialogFon.DurationFade);

    }
    
}
