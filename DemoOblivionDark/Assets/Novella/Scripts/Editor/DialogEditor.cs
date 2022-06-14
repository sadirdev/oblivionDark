using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(DialogData))]
public class DialogEditor : Editor
{
    private lng lng = lng.rus;
    GUIStyle styleActive;
    private DialogData db;
    private string[] _activeChar;

    //Dictionary<Vrbl.Get, Vrbl.VrblList> _vrblsDic;
    PhraseList _crntPh;
    PhraseList.VrblIfList _crntIf;

    private string[] _phraseArray;
    private void OnEnable()
    {
        db = (DialogData)target;
        if (db.Phrases == null) db.Phrases = new List<PhraseList>();

        db.crntIndex = 0;
        Vrbl.UpdateVrbls();
        foreach (var crntPh in db.Phrases)
        {
            if (crntPh.VrblEdit != null)
            {
               // crntPh.VrblEdit = null;
                //foreach (var edit in crntPh.VrblEdit)
                //{
                //    // edit.Get = (Value)EditorGUILayout.EnumPopup(edit.Get);


                //    string[] tempArr = Vrbl.VrblsDic[edit.Get].Set;
                //    int index = Array.IndexOf(tempArr, edit.Set);
                //    if (index < 0) index = 0;
                //    edit.Set = Vrbl.VrblsDic[edit.Get].Set[index];
                //    if (edit.Set == "yes")
                //    {
                //        Vrbl.Test();
                //        edit.Set = "yes(2)";
                //    }



                //}

            }
        }
    }
    private void OnDisable()
    {
        if (target != null)
            EditorUtility.SetDirty(target);
    }

    public override void OnInspectorGUI()
    {
        //if (_crntPh != null) _crntPh.VrblEdit = null;

        styleActive = new GUIStyle(GUI.skin.button);

        _activeChar = new string[db.Characters.Count];

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button($"Сейчас язык {lng}"))
        {
            if (lng == lng.rus) lng = lng.eng;
            else if (lng == lng.eng) lng = lng.por;
            else if (lng == lng.por) lng = lng.rus;
        }
        if(lng != lng.rus)
        {
            if (GUILayout.Button("Обновить текст"))
            {
                foreach (var phrase in db.Phrases)
                {
                    if(lng == lng.eng) phrase.TextEng = phrase.TextRus;
                    else if (lng == lng.por) phrase.TextPor = phrase.TextRus;
                    if(phrase.Menu != null)
                    {
                        foreach (var choice in phrase.Menu)
                        {
                            if (lng == lng.eng) choice.ChoiceEng = choice.ChoiceRus;
                            else if (lng == lng.por) choice.ChoicePor = choice.ChoiceRus;
                        }
                    }
                   
                }
            }
        }
        EditorGUILayout.EndHorizontal();


        for (int i = 0; i< db.Characters.Count; i++)
        {
            db.Characters[i] = (Character)EditorGUILayout.EnumPopup(db.Characters[i]);
            _activeChar[i] = $"{db.Characters[i]}";
        }
        EditorGUILayout.BeginHorizontal();
        if(db.Characters.Count>0)
        {
            if (GUILayout.Button("Удалить персонажа")) db.Characters.RemoveAt(db.Characters.Count - 1);
        }
        
        if (GUILayout.Button("Добавить персонажа")) db.Characters.Add(new Character());
        EditorGUILayout.EndHorizontal();


        _phraseArray = new string[db.Phrases.Count];

        EditorGUILayout.LabelField("");

        if(db.Phrases.Count >0 && db.Phrases[db.Phrases.Count-1].CloseDialog)
        {
            if(GUILayout.Button("Прыжок по порядку"))
            {
                for (int i = 0; i < db.Phrases.Count-1; i++)
                {
                    db.Phrases[i].JumpNextIndex = i+1;
                }
            }
            EditorGUILayout.LabelField("");
        }
        if(db.Phrases.Count != 0)
        {
            _crntPh = db.Phrases[db.crntIndex];
            for (int i = 0; i < db.Phrases.Count; i++)
            {
                string textBttn = "";
                if (lng == lng.rus) textBttn = db.Phrases[i].TextRus;
                else if (lng == lng.eng) textBttn = db.Phrases[i].TextEng;
                else if (lng == lng.por) textBttn = db.Phrases[i].TextPor;


                _phraseArray[i] = textBttn;
                if (db.crntIndex == i)
                {
                    EditorGUILayout.HelpBox($"{textBttn}", MessageType.None);
                }
                else
                {
                    if(db.Phrases[i].SkipPhrase) styleActive.normal.textColor = Color.green;
                    else if (db.Phrases[i].VrblEdit != null && db.Phrases[i].VrblEdit.Count >0) styleActive.normal.textColor = Color.cyan;
                    else styleActive.normal.textColor = Color.white;
                    if (GUILayout.Button($"{textBttn}", styleActive))
                    {
                        db.crntIndex = i;
                    }
                }
            }
        }
        else
        {
            _crntPh = null;
        }
        EditorGUILayout.BeginHorizontal();

        if(db.Phrases.Count > 1)
        {
            if (db.crntIndex != 0)
            {
                if (GUILayout.Button("<==")) db.SwapPhrase("<=");
            }

            if (db.crntIndex != db.Phrases.Count - 1)
            {
                if (GUILayout.Button("==>")) db.SwapPhrase("=>");
            }
        }
        if(db.crntIndex != 0)
        {
            if (GUILayout.Button("<=")) db.crntIndex--;
        }
        if (db.crntIndex < db.Phrases.Count-1)
        {
            if (GUILayout.Button("=>")) db.crntIndex++;
        }


        if (GUILayout.Button("Добавить фразу"))
        {
            db.AddPhrase();
            db.Phrases[db.Phrases.Count - 1].AddActiveCharDefould(db.Characters);
        }
            

        if(db.Phrases.Count != 0)
        {
            if (GUILayout.Button("Удалить")) db.RemovePhrase();
            
        }
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("");

        if (_crntPh != null)
        {
            CrntPh();
        }

        //base.OnInspectorGUI();
    }

    void CrntPh()
    {
        EditVoid();
        

        EditorGUILayout.LabelField("");
        if(_crntPh.ActiveChars != null)
        {
            for (int i = 0; i < _crntPh.ActiveChars.Count; i++)
            {
                string info = "Удали нахуй";
                if (i == 0) info = "Левый персонаж";
                else if (i == 1) info = "Правый персонаж";
                else if (i == 2) info = "Правый центральный персонаж";
                else if (i == 3) info = "Левый центральный персонаж";
                EditorGUILayout.BeginHorizontal();
                _crntPh.ActiveChars[i].IndexChar = EditorGUILayout.Popup(info, _crntPh.ActiveChars[i].IndexChar, _activeChar);
                _crntPh.ActiveChars[i].SpeekChar = EditorGUILayout.Toggle(_crntPh.ActiveChars[i].SpeekChar, GUILayout.MaxWidth(30));
                EditorGUILayout.EndHorizontal();
            }
        }
        
        EditorGUILayout.BeginHorizontal();
        if(_crntPh.ActiveChars == null || _crntPh.ActiveChars.Count <4)
            if (GUILayout.Button("Добавить активного персонажа")) _crntPh.AddActiveChar();
        if (_crntPh.ActiveChars != null && _crntPh.ActiveChars.Count !=0)
            if (GUILayout.Button("Удалить активного персонажа")) _crntPh.ActiveChars.RemoveAt(_crntPh.ActiveChars.Count -1);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("");

        //ТЕКСТ
        EditorGUILayout.BeginHorizontal();
        if (lng == lng.rus) _crntPh.TextRus = EditorGUILayout.TextArea(_crntPh.TextRus, GUILayout.Height(40), GUILayout.Width(800));
        else if (lng == lng.eng) _crntPh.TextEng = EditorGUILayout.TextArea(_crntPh.TextEng, GUILayout.Height(40), GUILayout.Width(800));
        else if (lng == lng.por) _crntPh.TextPor = EditorGUILayout.TextArea(_crntPh.TextPor, GUILayout.Height(40), GUILayout.Width(800));

        if (_crntPh.Fon == null)
        {
            if (GUILayout.Button("Img", GUILayout.MaxWidth(40), GUILayout.MaxHeight(40))) 
            {
                _crntPh.Fon = Transform.FindObjectOfType<CharacterManeger>().Characters[0].Sprite;
            }

        }
        else _crntPh.Fon = (Sprite)EditorGUILayout.ObjectField(_crntPh.Fon, typeof(Sprite), false, GUILayout.MaxHeight(40), GUILayout.MaxWidth(40));

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if(_crntPh.Menu ==null || _crntPh.Menu.Count <2) _crntPh.JumpNextIndex = EditorGUILayout.Popup( _crntPh.JumpNextIndex, _phraseArray);
        if(GUILayout.Button("Добавить выбор", GUILayout.MaxWidth(120)))
        {
            _crntPh.AddMenu();
            if(_crntPh.Menu.Count == 1) _crntPh.AddMenu();
        }
        

        EditorGUILayout.EndHorizontal();


        if (_crntPh.Menu != null && _crntPh.Menu.Count > 1)
        {
            if (_crntPh.VrblIf != null) _crntPh.VrblIf = null;

            int nunberChoice = 1;
            foreach (var menu in _crntPh.Menu)
            {
                EditorGUILayout.LabelField($"Вариант выбора № {nunberChoice}");
                EditorGUILayout.BeginHorizontal();
                if(lng == lng.rus) menu.ChoiceRus = EditorGUILayout.TextField(menu.ChoiceRus);
                else if (lng == lng.eng) menu.ChoiceEng = EditorGUILayout.TextField(menu.ChoiceEng);
                else if (lng == lng.por) menu.ChoicePor = EditorGUILayout.TextField(menu.ChoicePor);
                if (GUILayout.Button("X", GUILayout.MaxWidth(30)))
                {
                    _crntPh.RemoveMenu(menu);
                    break;
                }
                EditorGUILayout.EndHorizontal();
                menu.JumpNextIndex = EditorGUILayout.Popup(menu.JumpNextIndex, _phraseArray);
                EditorGUILayout.LabelField("");
                nunberChoice++;
            }
        }
        else
        {
            if (_crntPh.Menu != null) _crntPh.Menu = null;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (_crntPh.VrblIf != null && _crntPh.VrblIf.Count > 0) EditorGUILayout.LabelField("");
            else activeButton("Завершить диалог", ref _crntPh.CloseDialog);
            if(_crntPh.CloseDialog) activeButton("Удалить даилог", ref _crntPh.CompliteDialog);
            EditorGUILayout.EndHorizontal();
            activeButton("Блок пропуска", ref _crntPh.SkipPhrase);
            EditorGUILayout.EndHorizontal();

            if (_crntPh.VrblIf != null && _crntPh.VrblIf.Count != 0)
            {
                EditorGUILayout.BeginVertical("Box");
                // УСЛОВИЯ
                IfVoid();
                EditorGUILayout.EndVertical();
            }

            else
            {
                if (GUILayout.Button("Добавить If")) _crntPh.AddVrblIf();
            }
        }

       
        
            

        void EditVoid()
        {
            // Переменные 
            //_crntPh.VrblEdit = null;
            if (_crntPh.VrblEdit != null)
            {
                foreach (var edit in _crntPh.VrblEdit)
                {
                    EditorGUILayout.BeginHorizontal();
                    edit.Get = (Value)EditorGUILayout.EnumPopup(edit.Get);


                    string[] tempArr = Vrbl.VrblsDic[edit.Get].Set;
                    int index = Array.IndexOf(tempArr, edit.Set);
                    index = EditorGUILayout.Popup(index, tempArr);
                    if (index < 0) index = 0;
                    edit.Set = Vrbl.VrblsDic[edit.Get].Set[index];
                    if (GUILayout.Button("X", GUILayout.MaxWidth(30)))
                    {
                        _crntPh.RemoveEdit(edit);
                        break;
                    }
                    EditorGUILayout.EndHorizontal();
                    
                }

            }
            if (GUILayout.Button("Добавить изменение переменной")) _crntPh.AddVrblEdit();
        }
        void IfVoid()
        {
            // Условия
            if (!_crntPh.VrblIf.Contains(_crntIf)) _crntIf = _crntPh.VrblIf[0];

            EditorGUILayout.BeginHorizontal();
            for (int i =0 ; i< _crntPh.VrblIf.Count; i++)
            {
                string ifName = "";
                if (i == _crntPh.VrblIf.Count - 1) ifName = "else";
                else ifName = $"if_{i + 1}";
                if (_crntIf == _crntPh.VrblIf[i])
                {
                    EditorGUILayout.HelpBox(ifName, MessageType.None);
                }
                else
                {
                    if (GUILayout.Button(ifName))
                    {
                        _crntIf = _crntPh.VrblIf[i];
                    }
                }
            }
            
            if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
            {
                _crntPh.RemoveIf(_crntIf);
                return;
            }
                
            if (GUILayout.Button("+", GUILayout.MaxWidth(20))) _crntPh.AddVrblIf();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("");
            if (_crntIf != null) EditArray();

            void EditArray()
            {
                if ((_crntIf.VrblEdit == null || _crntIf.VrblEdit.Count == 0)) _crntIf.AddVrblEdit();
                if (_crntIf == _crntPh.VrblIf[_crntPh.VrblIf.Count - 1]) _crntIf.VrblEdit = null;
                if (_crntIf.VrblEdit != null)
                {
                    foreach (var Edit in _crntIf.VrblEdit)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Если", GUILayout.MaxWidth(35));
                        Edit.Get = (Value)EditorGUILayout.EnumPopup(Edit.Get);

                        EditorGUILayout.LabelField("=", GUILayout.MaxWidth(12));
                        string[] tempArr = Vrbl.VrblsDic[Edit.Get].Set;
                        int index = Array.IndexOf(tempArr, Edit.Set);
                        index = EditorGUILayout.Popup(index, tempArr);
                        if (index < 0) index = 0;
                        Edit.Set = Vrbl.VrblsDic[Edit.Get].Set[index];
                        if (GUILayout.Button("X", GUILayout.MaxWidth(30)))
                        {
                            _crntIf.RemoveEdit(Edit);
                            break;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }

                
                
                if (GUILayout.Button("Добавить условие")) _crntIf.AddVrblEdit();

                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("То прыжок на :");
                _crntIf.JumpNextIndex = EditorGUILayout.Popup(_crntIf.JumpNextIndex, _phraseArray);
            }
        }
    }

    

    bool activeButton(string nameButton,ref bool boolbutton)
    {
        styleActive.normal.textColor = Color.red;
        if (boolbutton)
        {
            if (GUILayout.Button(nameButton, styleActive)) boolbutton = false;
        }
        else
        {
            if (GUILayout.Button(nameButton)) boolbutton = true;
        }
        return boolbutton;
    }
    bool activeButton(string nameGet, string nameNext,ref bool boolbutton)
    {
        if (boolbutton)
        {
            if (GUILayout.Button(nameNext, styleActive)) boolbutton = false;
        }
        else
        {
            if (GUILayout.Button(nameGet)) boolbutton = true;
        }
        return boolbutton;
    }
}
