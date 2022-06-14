using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightDialog : MonoBehaviour
{
    public static string EnemyName;

    [SerializeField] private Sprite _playerSprite;
    [SerializeField] private Enemy[] _enemes;



    [SerializeField] private Text _text;
    [SerializeField] private Button _bttnDialog;
    [SerializeField] private RouletteContent _rouletteContent;
    [SerializeField] private FightIcon _iconPlayer;
    [SerializeField] private FightIcon _iconEnemy;

    private GameObject _focus;
    private Enemy _crntEnemy;

    //public static int EnemyHP = 500;
    //public static int EnemyDamage = 40;
    private stapCkick crntStap = stapCkick.moveEnemy;
    enum stapCkick {moveEnemy, movePlayer, startRotator}

    private void Start()
    {
        _crntEnemy = SelectEnemy(EnemyName);
        //_iconPlayer.EnterHp(SS.sv.PlayerHP, _playerSprite);
        _iconEnemy.EnterHp(_crntEnemy.HP, _crntEnemy.Sprite);
    }

    public void MoveEnemy()
    {
        _iconPlayer.UpdateSlider(-_crntEnemy.Damage);
        _text.text = "Получено " + _crntEnemy.Damage.ToString() + " единиц урона.";
        Destroy(_focus);
        _rouletteContent.FocusLine.SetActive(false);
        crntStap = stapCkick.startRotator;
    }

    public void MovePlayer(float damage, GameObject focus)
    {
        _iconEnemy.UpdateSlider((int)-damage);
        _text.text = "Враг получил " + damage.ToString() + " единиц урона.";
        _bttnDialog.enabled = true;
        _focus = focus;
        crntStap = stapCkick.moveEnemy;
    }
    private void StartRotator()
    {
        _text.text = "";
        _bttnDialog.enabled = false;
        _rouletteContent.StartRotator();
        crntStap = stapCkick.movePlayer;
    }
    public void ClickDialog()
    {
        if(crntStap == stapCkick.moveEnemy)
        {
            MoveEnemy();
        }
        else if( crntStap == stapCkick.startRotator)
        {
            StartRotator();
        }
    }

    Enemy SelectEnemy(string name)
    {
        foreach (var enemy in _enemes)
        {
            if (enemy.Name == name) return enemy;
        }
        return null;
    }

    [System.Serializable]
    class Enemy
    {
        public string Name;
        public Sprite Sprite;
        public int HP;
        public int Damage;

    }
}
