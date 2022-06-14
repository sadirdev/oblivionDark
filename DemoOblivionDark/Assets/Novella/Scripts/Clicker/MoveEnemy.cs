using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoveEnemy : MonoBehaviour
{

    [SerializeField] ClikerIconEnemy _enemyIcon;
     private ClickerIconPlayer _playerIcon;
    [SerializeField] Clicker _clicker;
    

    public static float SecondStap;
    public RectTransform Enemy;
    private int _clickCound=0;



    private float _buferScaleEnemy;
    private int _heightEnemyHalf;
    private int _widthEnemyHalf;

    private void Start()
    {

        _playerIcon = _clicker.IconPlayer;
        //Enemy.localScale = new Vector2(transform.localScale.x * SS.MultiplierSize, transform.localScale.y * SS.MultiplierSize);
        Enemy.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        _buferScaleEnemy = Enemy.localScale.x;

        _heightEnemyHalf = (int)((Enemy.rect.height / 2) * Enemy.localScale.y);
        _widthEnemyHalf = (int)((Enemy.rect.width / 2) * Enemy.localScale.x);
    }

    public void Click()
    {
        if (_clickCound == 0) StartCoroutine(StartMoveEnemy());
        if (ClikerIconEnemy.CrntHP <= 0) _clicker.Win(); 
        
        _enemyIcon.ClickEnemy();
        _clickCound++;
    }
  
    private IEnumerator StartMoveEnemy()
    {
        _playerIcon.SliderAnim();
        Clicker.Active = true;
        int x = 0;
        int y = 0;
        int border = 0;

        while (Clicker.Active)
        {
            float multiplierSize = Random.Range(0.8f, 1.2f);
            border = (int)(Enemy.rect.height  * multiplierSize);
            Enemy.localScale = new Vector2(_buferScaleEnemy * multiplierSize, _buferScaleEnemy * multiplierSize);
            yield return new WaitForSeconds(SecondStap); //SecondStap
            if (Clicker.Active)
            {
                x = Random.Range(-(Screen.width / 2), (Screen.width / 2) - border);
                y = Random.Range(0, -(Screen.height - border));
                Enemy.gameObject.transform.localPosition = new Vector2(x, y);
            }
        }
    }

    public IEnumerator DestroyEnemy()
    {
        int jumpCound = 0;
        int x = 0;
        int y = 0;
        int borderX = 0;
        int borderY = 0;
        while (jumpCound < 60)
        {
            MainAudio.Static.AudioSource.volume -= 0.01667f;
            float multiplierSize = Random.Range(0.8f, 1.2f);
            Enemy.localScale = new Vector2(_buferScaleEnemy * multiplierSize, _buferScaleEnemy * multiplierSize);
            borderX = (int)(_widthEnemyHalf * multiplierSize);
            borderY = (int)(_heightEnemyHalf * multiplierSize);
            x = Random.Range(-(Screen.width / 2 - borderX), Screen.width / 2 - borderX);
            y = Random.Range(-(Screen.height / 2 - borderY), Screen.height / 2 - borderY);

            yield return new WaitForSeconds(0.015f);
             Enemy.transform.localPosition = new Vector2(x, y);
            jumpCound++;
        }
        Enemy.pivot = new Vector2(0.5f, 0.5f);
        Enemy.transform.localPosition = new Vector2(0, -Screen.height/2);
        yield return new WaitForSeconds(0.9f);

        if(Clicker.EnemyName == Clicker.EnemyEnum.КислаяРожа || Clicker.EnemyName == Clicker.EnemyEnum.МастерВМайке || Clicker.EnemyName == Clicker.EnemyEnum.БлестящийСуперсплав || Clicker.EnemyName == Clicker.EnemyEnum.ГомоГомоЗек)
        {
            GetComponent<Image>().DOFade(0, 1);
        }
        else
        {
            gameObject.SetActive(false);
            BloodBoom.Build();
        }

        


        yield return new WaitForSeconds(0.5f);
        MainAudio.Static.AudioSource.volume = 1;


        _clicker.DestroyVoid(true);
    }
   
}
