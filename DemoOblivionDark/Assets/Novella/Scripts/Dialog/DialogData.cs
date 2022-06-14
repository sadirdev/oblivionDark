using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Dialog", menuName = "ScrObj/Dialog")]
public class DialogData : ScriptableObject
{
    string[] _beforeArr;


    public List<Character> Characters = new List<Character>();
   
    public List<PhraseList> Phrases;
    public int crntIndex;

    public void AddPhrase()
    {
        PhraseList crntPh = new PhraseList();
        Phrases.Add(crntPh);
        crntIndex = Phrases.Count - 1;
    }
    public void RemovePhrase()
    {
        UpdateJumpIndexBefore();

        Phrases.RemoveAt(crntIndex);
        if (crntIndex != 0) crntIndex--;

        UpdateJumpIndexAfter();
    }

    public void SwapPhrase(string direction)
    {
        UpdateJumpIndexBefore();

        if (direction == "=>")
        {
            PhraseList temp = Phrases[crntIndex+1];
            Phrases[crntIndex+1] = Phrases[crntIndex];
            Phrases[crntIndex] = temp;
            crntIndex++;
        }
        if (direction == "<=")
        {
            PhraseList temp = Phrases[crntIndex - 1];
            Phrases[crntIndex - 1] = Phrases[crntIndex];
            Phrases[crntIndex] = temp;
            crntIndex--;
        }
        UpdateJumpIndexAfter();
    }

    void UpdateJumpIndexBefore()
    {
        _beforeArr = new string[Phrases.Count];
        for(int i = 0; i < Phrases.Count; i++)
        {
            _beforeArr[i] = Phrases[i].TextRus;
        }
    }

    void UpdateJumpIndexAfter()
    {
        for (int i = 0; i < Phrases.Count; i++)
        {
            if (Phrases[i].Menu != null && Phrases[i].Menu.Count > 1)
            {
                for (int j = 0; j < Phrases[i].Menu.Count; j++)
                {
                    
                    IndexUpdate(ref Phrases[i].Menu[j].JumpNextIndex);
                }
            }
            else if(Phrases[i].VrblIf != null && Phrases[i].VrblIf.Count != 0)
            {
                foreach (var If in Phrases[i].VrblIf)
                {
                    IndexUpdate(ref If.JumpNextIndex);
                    
                }
            }
            IndexUpdate(ref Phrases[i].JumpNextIndex);
            
        }

        void IndexUpdate( ref int index)
        {
            if (index >= Phrases.Count) index = 0;
            index = Array.IndexOf(_beforeArr, Phrases[index].TextRus);
            if (index == -1) index = 0;
        }
    }
    
    
}

[System.Serializable]
public class PhraseList
{
    public List<ActiveChar> ActiveChars;
    //public string Text;
    public string TextRus;
    public string TextEng;
    public string TextPor;

    public Sprite Fon;
    public int JumpNextIndex;
    public bool CloseDialog;
    public bool CompliteDialog;
    public bool SkipPhrase;

    //public List<Vrbl.EditorVrbl> EditVrbl = null;
    public List<VrblEditList> VrblEdit = null;
    public List<VrblIfList> VrblIf = null;
    public List<MenuList> Menu = null;

    public void AddActiveChar()
    {
        if (ActiveChars == null) ActiveChars = new List<ActiveChar>();
        ActiveChars.Add(new ActiveChar(0));
    }
    public void AddActiveCharDefould(List<Character> characters)
    {
        if (ActiveChars == null) ActiveChars = new List<ActiveChar>();
        for (int i = 0; i < characters.Count; i++)
        {
            ActiveChars.Add(new ActiveChar(i));
        }

    }

    public void AddVrblEdit()
    {
        if (VrblEdit == null) VrblEdit = new List<VrblEditList>();
        VrblEditList crntEdit = new VrblEditList();
        VrblEdit.Add(crntEdit);
    }
    public void RemoveEdit(VrblEditList edit)
    {
        VrblEdit.Remove(edit);
        if (VrblEdit.Count == 0) VrblEdit = null;
    }

    public void AddVrblIf()
    {
        if (VrblIf == null) VrblIf = new List<VrblIfList>();
        VrblIfList crntIf = new VrblIfList();
        VrblIf.Add(crntIf);
    }
    public void RemoveIf(VrblIfList If)
    {
        VrblIf.Remove(If);
        if (VrblIf.Count == 0) VrblIf = null;
    }



    public void AddMenu()
    {
        MenuList crntM = new MenuList();
        if (Menu == null) Menu = new List<MenuList>();
        Menu.Add(crntM);
    }
    public void RemoveMenu(MenuList menu)
    {
        Menu.Remove(menu);
        if (Menu.Count == 1) Menu = null;
    }

    [System.Serializable]
    public class MenuList
    {
        //public string Choice;
        public string ChoiceRus;
        public string ChoiceEng;
        public string ChoicePor;
        public int JumpNextIndex;
    }
    [System.Serializable]
    public class ActiveChar
    {
        public int IndexChar;
        public bool SpeekChar = false;
        public ActiveChar(int indexChar)
        {
            IndexChar = indexChar;
        }
    }
    [System.Serializable]
    public class VrblEditList
    {
        public string Set;
        public Value Get;
    }

    [System.Serializable]
    public class VrblIfList
    {
        public List<VrblEditList> VrblEdit = null;
        public int JumpNextIndex;

        public void AddVrblEdit()
        {
            if (VrblEdit == null) VrblEdit = new List<VrblEditList>();
            VrblEditList crntEdit = new VrblEditList();
            VrblEdit.Add(crntEdit);
        }
        public void RemoveEdit(VrblEditList edit)
        {
            VrblEdit.Remove(edit);
            if (VrblEdit.Count == 0) VrblEdit = null;
        }
    }

}
