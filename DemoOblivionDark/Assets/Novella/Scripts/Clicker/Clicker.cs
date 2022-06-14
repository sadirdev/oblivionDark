using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Clicker : MonoBehaviour
{
    public static EnemyEnum EnemyName;
    public static bool Active = false;

    [SerializeField] private TMP_Text _nameRole;
    [HideInInspector]public ClickerIconPlayer IconPlayer;
    [SerializeField] private ClikerIconEnemy _iconEnemy;
    [SerializeField] private MoveEnemy _moveEnemy;
    [SerializeField] private GameObject _tutorial;
    public static DelegateVoid WinDel;
    public static DelegateVoid LoseDel;
    public static bool NoJump = false;
    private static int _crntItamLvl;




    public static Dictionary<EnemyEnum, Enemy> DictEnemy = new Dictionary<EnemyEnum, Enemy>();

    private float _durationPlayerHP;
    private float _clickDamage;
    public static bool ImpossibleWin = false;

   

   private void GetExp()
    {
        
        float addExp = 0;
        if (EnemyName == EnemyEnum.EnemyPortal) addExp = 21;
        else if (EnemyName == EnemyEnum.Flower) addExp = 25;
        else if (EnemyName == EnemyEnum.Богомол) addExp = 30;
        else if (EnemyName == EnemyEnum.Паучиха) addExp = 38;
        else if (EnemyName == EnemyEnum.ДухПирата) addExp = 93;
        
        else if (EnemyName == EnemyEnum.Глорибас) addExp = 32;

        LvlManager.Static.GetExp(addExp); 
    }
    


    public void Win()
    {
        StartCoroutine(_moveEnemy.DestroyEnemy());
        DisableClicker();

        SS.sv.ComliteEnemy.Add(EnemyName);

       
    }
    public void Loose()
    {
        DisableClicker();
        
        DestroyVoid(false);
        Show.IconPlayer.Start();
    }
    public static bool NeedApgrateClass()
    {
        int enemyLvl = DictEnemy[EnemyName].Lvl;

        if (enemyLvl <= _crntItamLvl || enemyLvl == 1)
        {
            Dialog.Notification("Не получилось: у вас закончилась энергия. Попробуйте ещё раз.");
            return false;
        }
        
        if (enemyLvl == 2) Dialog.Notification("Чтобы одолеть противника класса B, тебе нужно приобрести Сенсоры в городе Z.");
        else if (enemyLvl == 3) Dialog.Notification("Чтобы одолеть противника класса A, тебе нужно приобрести Ускорители.");
        else if (enemyLvl == 4) Dialog.Notification("Чтобы одолеть противника класса S, тебе нужно приобрести Палладиевое ядро.");
        return true;
    }
    private void DisableClicker()
    {
        
        _moveEnemy.GetComponent<Button>().enabled = false;
        Active = false;
        IconPlayer.Stop();
       
    }
    public void DestroyVoid(bool win) 
    {
        MainAudio.Static.PlayFon();
        GetComponent<CanvasGroup>().DOFade(0, DialogFon.DurationFade * 2).OnComplete(() =>
        {
            Destroy(gameObject);
            if (!NoJump)
            {
                Debug.Log("Прыжок на кликере"); 
                Show.Loc.JumpLoc(SS.sv.CrntLoc);
            }
            
            if(win)
            {
                if (WinDel != null) WinDel();
                GetExp();
            }
            else
            {
                if (LoseDel == null) NeedApgrateClass();
                else LoseDel();
            }
            LoseDel = null;
            WinDel = null;
            NoJump = false;
            Interface.ButtonShow();

        });
    }

    private void Start()
    {
       

        GetComponent<CanvasGroup>().DOFade(1, DialogFon.DurationFade);
        IconPlayer = FindObjectOfType<ClickerIconPlayer>();
        IconPlayer.Clicker = this;
        IconPlayer.Repair.SetActive(false);
        Interface.ButtonHide();
        Build();
    }
    
    private void Build()
    {
        MainAudio.Static.PlayBattle();
        Enemy crntEnemy = DictEnemy[EnemyName];
        if (EnemyName == EnemyEnum.EnemyPortal) _tutorial.SetActive(true);
        DifficultyGenerate(crntEnemy);
        IconPlayer.Build(_durationPlayerHP);
        _iconEnemy.Build(crntEnemy.Icon, _clickDamage, ClassReturn(crntEnemy.Lvl));
        Image imgEmeny = _moveEnemy.Enemy.GetComponent<Image>();
        imgEmeny.sprite = crntEnemy.Sprite;
        //imgEmeny.SetNativeSize();
        imgEmeny.color = crntEnemy.Color;


        if (SS.sv.Lang == lng.rus) _nameRole.text = crntEnemy.NameRus;
        else if (SS.sv.Lang == lng.eng) _nameRole.text = crntEnemy.NameEng;
        else if (SS.sv.Lang == lng.por) _nameRole.text = crntEnemy.NamePor;


        //Enemy SelectedEnemy(EnemyEnum name)
        //{
        //    foreach (var enemy in _enemies)
        //    {
        //        if (name == enemy.Name) return enemy;
        //    }
        //    return null;
        //}
    }
    private void DifficultyGenerate(Enemy enemy)
    {
        int deltaLvl=1;
        _crntItamLvl = 1;
        if (Inventory.Contains != null)
        {
            if (Inventory.Contains("Ядро")) _crntItamLvl = 4;
            else if (Inventory.Contains("Ускорители")) _crntItamLvl = 3;
            else if (Inventory.Contains("Сенсор")) _crntItamLvl = 2;
            else _crntItamLvl = 1;

            
        }

        deltaLvl = enemy.Lvl - _crntItamLvl;

        if (deltaLvl < 0 )
        {
            ImpossibleWin = false;
            _durationPlayerHP = 35;
            _clickDamage = 0.02f;
            MoveEnemy.SecondStap = 2;
        }
        else if(deltaLvl == 0)
        {
            ImpossibleWin = false;
            _durationPlayerHP = 20;
            _clickDamage = 0.019f;
            MoveEnemy.SecondStap = 1.5f;
        }
        else if (deltaLvl == 1)
        {
            ImpossibleWin = true;
            _durationPlayerHP = 20;
            _clickDamage = 0.004f;
            MoveEnemy.SecondStap = 1;
        }
        else if (deltaLvl == 2)
        {
            ImpossibleWin = true;
            _durationPlayerHP = 15;
            _clickDamage = 0.002f;
            MoveEnemy.SecondStap = 0.5f;
        }
        else
        {
            ImpossibleWin = true;
            _durationPlayerHP = 10;
            _clickDamage = 0.001f;
            MoveEnemy.SecondStap = 0.4f;
        }
        //_clickDamage = 0.5f;//Потом Убрать
    }
    public static string ClassReturn(int lvl)
    {
        if (lvl == 2) return "B";
        else if (lvl == 3) return "A";
        else if (lvl == 4) return "S";
        else return "C";
    }
    

    [System.Serializable]
    public class Enemy
    {
        public string NameRus;
        public string NameEng;
        public string NamePor;

        public int Lvl = 1;
        public EnemyEnum Name;
        public Sprite Icon;
        public Sprite Sprite;
        public Color Color = Color.white;


    }
    

    public enum EnemyEnum { EnemyPortal, Sonic, Flower, Богомол, КислаяРожа, МастерВМайке, БлестящийСуперсплав, ГомоГомоЗек, Глорибас, Паучиха, ДухПирата }

}
