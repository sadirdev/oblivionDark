using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpriteCharacter : MonoBehaviour
{
     public Image ImgSprite;
    private float _durationAnim = 0.5f;

    private float _scale;

    
    private void Start()
    {
        //ImgSprite.transform.localScale = new Vector2(ImgSprite.transform.localScale.x * SS.MultiplierSize, ImgSprite.transform.localScale.y * SS.MultiplierSize);
        ImgSprite.transform.localScale = new Vector2(ImgSprite.transform.localScale.x, ImgSprite.transform.localScale.y);
    }

    private void OnEnable()
    {

        ImgSprite.color = new Color(0.48f, 0.48f, 0.48f, 0);
        ImgSprite.DOFade(1, 0.5f);
    }
    public void AnimSpeek()
    {
        //_scale = 1* SS.MultiplierSize;
        _scale = 1;
        transform.DOScale(new Vector3(_scale, _scale, _scale), _durationAnim);
        ImgSprite.DOColor(new Color(1, 1, 1), _durationAnim);

    }
    public void AnimSilent()
    {
        _scale = 0.85f;
        transform.DOScale(new Vector3(_scale, _scale, _scale), _durationAnim);
        ImgSprite.DOColor(new Color(0.48f, 0.48f, 0.48f), _durationAnim);
    }

    public void DisableSprite()
    {
        ImgSprite.DOFade(0, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });

    }
}
