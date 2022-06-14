using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BGBall : MonoBehaviour
{
    public Image FonGame;
    [SerializeField] private Color _returnColor;
    
    ///[SerializeField] private Image _fonMenu;

    [SerializeField] private Fon[] _fons;
    private void Awake()
    {
        //FonGame = GetComponent<Image>();

        //int indexFon = ST.sv.LvlKepler - 1;
        //while (indexFon >= _fons.Length)
        //{
        //    indexFon -= _fons.Length;
        //}
        //FonGame.sprite = _fons[0].Sprite;
        //Debug.Log($"_fons[0].Sprite = {_fons[0].Sprite.name}");
        ////_fonMenu.sprite = _fons[indexFon];
    }

   
    public ParticleSystem FonGenerate()
    {
        int indexFon = ST.sv.LvlKepler - 1;
        while (indexFon >= _fons.Length)
        {
            indexFon -= _fons.Length;
        }
        if (FonGame.sprite != _fons[indexFon].Sprite)
        {
            
            FonGame.DOColor(Color.black, DialogFon.DurationFade).OnComplete(() =>
            {
                FonGame.sprite = _fons[indexFon].Sprite;
                FonGame.DOColor(Color.white, DialogFon.DurationFade).OnComplete(() => { FindObjectOfType<NextButton>().Enable(); });
            });
        }
        return _fons[indexFon].Particle;
        //_fonMenu.sprite = _fons[indexFon];
    }

    public void SetColor(Color color)
    {
        Color newColor = new Color(color.r + 0.2714f, color.g + 0.2938f, color.b - 0.0943f);
        //DOColor(LvlData.CrntLvl.Colors[0], 0.5f);
        FonGame.DOColor(newColor, 0.5f);
    }
    public void ReturnColor()
    {
        FonGame.DOColor(_returnColor, 0.5f);
    }

    [System.Serializable]
    public class Fon
    {
        public Sprite Sprite;
        public ParticleSystem Particle;
    }

    
}
