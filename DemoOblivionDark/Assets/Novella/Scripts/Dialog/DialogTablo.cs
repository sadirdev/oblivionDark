using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DialogTablo : MonoBehaviour
{
    [SerializeField] private Button _bttnNext;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private MenuBuilder _menuBttn;
    [SerializeField] private MenuBuilder _menuBttnGem;
    [SerializeField] private GameObject _nameBox;
    [SerializeField] private Transform _contentMenu;
    [SerializeField] private Transform _contentChar;
    [SerializeField] private float _speedLetter;

    [SerializeField] private DialogFon _fonScr;
    [SerializeField] private DialogData _crntDialog;
    private PhraseList _crntPhrase;
    private string _crntText;
    private List<buferEditVerbl> _buferEdit = new List<buferEditVerbl>();
    [HideInInspector] public int JumpIndex=0;
    private Coroutine _crntCoroutineLetter;
    //public static DelegateVoid CloseVoid;
    public static List<ButtonJumpFade> StopButtonJumpFade =new List<ButtonJumpFade>();
    public static Location BuferLocation;

    public static Dictionary<string, CloseDel> Close = new Dictionary<string, CloseDel>();

    

    //private void Awake()
    //{
    //    Vrbl.UpdateVrbls();
    //}
    public void ShowSprite(DialogData dialog, bool hideInterface)
    {
        
        Vrbl.Set(Value.YesNo, "null(1)"); 
        if (hideInterface)Interface.AllHide();
        _fonScr.gameObject.GetComponent<AspectRatioFitter>().enabled = true;
        _crntDialog = dialog;
        JumpIndex = 0;
        NextPhrase();
    }
  

    public void BttnClickNextPhrase()
    {
        

        

        if (_text.text.Length == _crntText.Length)
        {
            NextPhrase();
        }
        else
        {
            StopCoroutine(_crntCoroutineLetter);
            _text.text = _crntText;
            
            while (_text.text.Contains("/"))
            {
                _text.text = _text.text.Replace('/', ' ');
            }
           
            ShowMenu();
        }
       
    }
    public void NextPhrase()
    {
        if(_crntPhrase == null) _crntPhrase = _crntDialog.Phrases[0];
       
        if (_crntPhrase.CloseDialog)
        {
            if(!string.IsNullOrEmpty(_text.text))
            {
                CloseDiaolg();
                return;
            }
            
        }
       
        

        _crntPhrase = _crntDialog.Phrases[JumpIndex];

        if (SS.sv.Lang == lng.rus) _crntText = _crntPhrase.TextRus;
        else if (SS.sv.Lang == lng.eng) _crntText = _crntPhrase.TextEng;
        else if (SS.sv.Lang == lng.por) _crntText = _crntPhrase.TextPor;

        // НОВАЯ ФРАЗА
        JumpIndex = _crntPhrase.JumpNextIndex;
        // Изменить переменные
        VrblEdit();
        // Проверить условия 
        VrblIf();
        
        //Блок пропуска
        if(_crntPhrase.SkipPhrase)
        {
            NextPhrase();
            return;
        }
        // Показать текст
        _crntCoroutineLetter = StartCoroutine(TypePhrase(_crntText));
        // Показать меню
        ShowMenu();
        // Показать фон
        Fon();
        // Анимация спрайтов
        UpdateSprite();
    }
    private void Fon()
    {
        if (_crntPhrase.Fon != null)
        {
            _fonScr.Show(_crntPhrase.Fon);
        } 
        
       
    }
   
    IEnumerator TypePhrase(string text)
    {
        if (_crntPhrase.Menu != null && _crntPhrase.Menu.Count > 1 && !string.IsNullOrEmpty(_text.text)) yield break;
        _text.text = "";

        foreach (char letter in text.ToCharArray())
        {
            if(letter =='/')
            {
                _text.text += ' ';
                yield return new WaitForSeconds(1);
            }
                
            else
            {
                _text.text += letter;
                yield return new WaitForSeconds(_speedLetter);
            }
            
        }
        
    }
    
    void ShowMenu()
    {
        if (!_bttnNext.enabled) _bttnNext.enabled = true;
        if (_crntPhrase.Menu != null && _crntPhrase.Menu.Count > 1)
        {
            MenuBuilder(_crntPhrase.Menu);
        }

        void MenuBuilder(List<PhraseList.MenuList> menuList)
        {
            _bttnNext.enabled = false;
            foreach (var menu in menuList)
            {
                MenuBuilder menuBulder = null;
                if (menu.ChoiceRus == "GEM")
                {
                    if(SS.sv.Gem > 0)
                    {
                        menuBulder = Instantiate(_menuBttnGem, _contentMenu);
                        menuBulder.Text.text = "";
                    }
                }
                else
                {
                    menuBulder = Instantiate(_menuBttn, _contentMenu);
                    if (SS.sv.Lang == lng.rus) menuBulder.Text.text = menu.ChoiceRus;
                    else if (SS.sv.Lang == lng.eng) menuBulder.Text.text = menu.ChoiceEng;
                    else if (SS.sv.Lang == lng.por) menuBulder.Text.text = menu.ChoicePor;
                }
                if(menuBulder != null)
                {
                    menuBulder.JumpNext = menu.JumpNextIndex;
                    menuBulder.dt = gameObject.GetComponent<DialogTablo>();
                }
               

            }
        }
    }
    void VrblEdit()
    {
        if(_crntPhrase.VrblEdit != null && _crntPhrase.VrblEdit.Count > 0)
        {
            foreach (var vrbl in _crntPhrase.VrblEdit)
            {

                _buferEdit.Add(new buferEditVerbl(vrbl.Get, vrbl.Set));
                
                //Vrbl.VrblsDic[vrbl.Get].crnt = vrbl.Set;
            }
        }
    }
    void VrblIf()
    {
        if(_crntPhrase.VrblIf != null && _crntPhrase.VrblIf.Count >0)
        {
            
            for (int i = 0; i < _crntPhrase.VrblIf.Count; i++)
            {
                bool temp = true;
                foreach (var edit in _crntPhrase.VrblIf[i].VrblEdit)
                {
                    if (Vrbl.VrblsDic[edit.Get].crnt != edit.Set)
                    {
                        temp = false;
                        break;
                    }
                }
                if (temp)
                {
                    JumpIndex = _crntPhrase.VrblIf[i].JumpNextIndex;
                    return;
                }
            }
        }
    }
    private void UpdateSprite()
    {
        _name.text = "";
        for (int i = 0; i < _contentChar.childCount; i++)
        {
            SpriteCharacter charScr = _contentChar.GetChild(i).GetComponent<SpriteCharacter>();
            if (i < _crntPhrase.ActiveChars.Count)
            {
                var NameAndSprite = CharacterManeger.CharDict[_crntDialog.Characters[_crntPhrase.ActiveChars[i].IndexChar]];
                if (NameAndSprite.Sprite != null)
                {
                    charScr.ImgSprite.sprite = NameAndSprite.Sprite;
                    //charScr.ImgSprite.SetNativeSize();

                    if (_crntPhrase.ActiveChars[i].SpeekChar)
                    {
                        if(SS.sv.Lang == lng.rus) _name.text = NameAndSprite.NameRus;
                        else if (SS.sv.Lang == lng.eng) _name.text = NameAndSprite.NameEng;
                        else if (SS.sv.Lang == lng.por) _name.text = NameAndSprite.NamePor;
                        charScr.AnimSpeek();
                    }
                    else charScr.AnimSilent();

                    _contentChar.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    _contentChar.GetChild(i).gameObject.SetActive(false);
                }
            }

            else charScr.DisableSprite();
        }
        if (string.IsNullOrEmpty(_name.text)) _nameBox.SetActive(false);
        else _nameBox.SetActive(true);
    }                                               
    private void CloseDiaolg()
    {
        
        Dialog.DialogNotReplay = "";
        Interface.AllShow();
        foreach (var vrbl in _buferEdit)
        {
            Vrbl.Set(vrbl.value, vrbl.crnt);
        }
        if (!LocMngr.NoJump) Show.Loc.JumpLoc(SS.sv.CrntLoc);
        if (_crntPhrase.CompliteDialog)
        {
            Dialog.DictDialog[_crntDialog.name].Remove = true;
            foreach (var dialog in SS.sv.Dialogs)
            {
                if (dialog.Name == _crntDialog.name)
                {
                    dialog.Remove = true;
                    break;
                }
            }
        }

        
        _fonScr.Hide();
        gameObject.GetComponent<CanvasGroup>().DOFade(0, DialogFon.DurationFade).OnComplete(() => { DestoyTablo(); });
        //gameObject.GetComponent<Animation>().Play("CloseTablo");
        _bttnNext.enabled = false;
        if(BuferLocation == SS.sv.CrntLoc)
        {
            foreach (var bttnFade in StopButtonJumpFade)
            {
                bttnFade.gameObject.SetActive(true);
            }
        }
       
        if (Close.ContainsKey(_crntDialog.name))
        {
            var tempCloseDel = Close[_crntDialog.name];
            Close.Remove(_crntDialog.name);

            if (Vrbl.Get(Value.YesNo) == "null(1)") tempCloseDel._1();
            else if (Vrbl.Get(Value.YesNo) == "yes(2)")
            {
                if (tempCloseDel._1 == null || tempCloseDel._4 != null) tempCloseDel.Yes();
                else tempCloseDel._1();
            }
                
            else if(Vrbl.Get(Value.YesNo) == "no(3)")
            {
                if (tempCloseDel.No != null) tempCloseDel.No();
            }
            else if (Vrbl.Get(Value.YesNo) == "(4)") tempCloseDel._4();
            else if (Vrbl.Get(Value.YesNo) == "(5)") tempCloseDel._5();

            
        }

       
        LocMngr.NoJump = false;
        
    }
    public void Esc()
    {
        Dialog.DictDialog[_crntDialog.name].Remove = true;
        foreach (var dialog in SS.sv.Dialogs)
        {
            if (dialog.Name == _crntDialog.name)
            {
                dialog.Remove = true;
                break;
            }


        }
        CloseDiaolg();
    }
    public void DestoyTablo()
    {
        Destroy(gameObject);
    }
    public void DestroyMenu()
    {
        foreach (Transform item in _contentMenu)
        {
            Destroy(item.gameObject);
        }
    }
    class buferEditVerbl
    {
        public Value value;
        public string crnt;
        public buferEditVerbl(Value value, string crnt)
        {
            this.value = value;
            this.crnt = crnt;
        }
    }
    public class CloseDel
    {
        public DelegateVoid _1;
        public DelegateVoid Yes;
        public DelegateVoid No;
        public DelegateVoid _4;
        public DelegateVoid _5;

        public CloseDel(DelegateVoid yes, DelegateVoid no)
        {
            Yes = yes;
            No = no;
        }
        public CloseDel(DelegateVoid _1, DelegateVoid _2, DelegateVoid _3, DelegateVoid _4, DelegateVoid _5)
        {
            this._1 = _1;
            Yes = _2;
            No = _3;
            this._4 = _4;
            this._5 = _5;
        }
    }

}
