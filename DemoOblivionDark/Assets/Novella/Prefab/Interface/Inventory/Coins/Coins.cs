using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    
    [SerializeField] private Text _coins;
    private Vector2 _startPosition;
    public delegate void AddDel(int coins);
    public static AddDel MoveCoins;
    public static AddDel MoveGem;
    void Start()
    {
        

        if (_coins != null)
        {
            _startPosition = transform.position;
            transform.localPosition = new Vector2(50, transform.localPosition.y);
            transform.DOLocalMoveX(0, 0.5f);
            _coins.text = SS.sv.Player.Coins.ToString();
        }
        else MoveCoins = MoveCoinsVoid;

        MoveGem = MoveGemVoid;


    }

    private void MoveCoinsVoid(int coins)
    {
        SS.sv.Player.Coins += coins;
    }
    private void MoveGemVoid(int cound)
    {
        SS.sv.Gem += cound;
    }
    

    public void Close()
    {
        transform.DOLocalMoveX(_startPosition.x, 0.5f);
        GetComponent<CanvasGroup>().DOFade(0, 0.5f);
    }

    public void ClickCoins()
    {
        FindObjectOfType<Inventory>().Close();
        Show.Build.Shop(true);
    }
    
}
