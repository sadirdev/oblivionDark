using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{


    [SerializeField]private GameObject _prefabTablo;
    [Header("Скрипты")]
    [SerializeField] private CharacterManeger _charMng;
    public static Dictionary<string, DialogDataAndCheck> DictDialog = new Dictionary<string, DialogDataAndCheck>();
    public static string DialogNotReplay;

    public delegate void BuildVoidDel(string nameDialog, DelegateVoid voidDel);
    public delegate void DelYesNo(string nameDialog, DelegateVoid yes, DelegateVoid no);
    public delegate void Del6(string nameDialog, DelegateVoid _1, DelegateVoid _2, DelegateVoid _3, DelegateVoid _4, DelegateVoid _5);
    public delegate void BuildDel(string nameDialog);
    public delegate bool CompliteDel(string nameDialog);

    public static BuildVoidDel BuildWithDic;
    public static DelYesNo BuildYesNo;
    public static BuildDel Build;
    public static BuildDel Notification;
    public static BuildVoidDel NotificationWithDic;
    public static DelYesNo NotificationYesNo;
    public static Del6 Build_5;
    public static CompliteDel IsComplite;

    private void Awake()
    {
        Show.Dialog = this;
        BuildWithDic = BuildWithVoidVoid;
        Build = BuildVoid;
        Build_5 = Build_5Void;
        IsComplite = CompliteVoid;
        Notification = NotificationVoid;
        NotificationWithDic = NotificationWithDicVoid;
        NotificationYesNo = NotificationYesNoVoid;
        BuildYesNo = BuildYesNoVoid;
        Start1();
    }

    private void Start1()
    {
        //BuildDict();
        DictDialog.Clear();
        foreach (var dialog in _charMng.DialogData)
        {
            DialogDataAndCheck dataAndCheck=null;


            foreach (var nameAndCheck in SS.sv.Dialogs)
            {
                if(dialog.name == nameAndCheck.Name)
                {
                    dataAndCheck = new DialogDataAndCheck(dialog, nameAndCheck.Remove);
                    break;
                }
            }
            if(dataAndCheck == null)
            {
                dataAndCheck = new DialogDataAndCheck(dialog, false);
                SS.sv.Dialogs.Add(new DialogNameAndCheck(dialog.name, false));
            }
            DictDialog.Add(dialog.name, dataAndCheck);


        }


    }
    private void NotificationVoid(string notification)
    {
        DialogTablo.StopButtonJumpFade.Clear();
        DialogData dataTemp = new DialogData();
        dataTemp.Phrases = new List<PhraseList>();
        dataTemp.name = notification;
        dataTemp.Phrases.Add(new PhraseList());
        dataTemp.Phrases[0].CloseDialog = true;

        if(SS.sv.Lang == lng.rus) dataTemp.Phrases[0].TextRus = notification;
        else if (SS.sv.Lang == lng.por) dataTemp.Phrases[0].TextPor = notification;
        else if (SS.sv.Lang == lng.eng) dataTemp.Phrases[0].TextEng = notification;

        dataTemp.Phrases[0].ActiveChars = new List<PhraseList.ActiveChar>();
        GameObject tablo = Instantiate(_prefabTablo, transform);
        tablo.GetComponent<DialogTablo>().ShowSprite(dataTemp, false);
    }
    private void NotificationWithDicVoid(string notification, DelegateVoid yes)
    {
        DialogTablo.Close.Add(notification, new DialogTablo.CloseDel(yes, null, null, null, null) );
        NotificationVoid(notification);
    }
    private void NotificationYesNoVoid(string notification, DelegateVoid yes, DelegateVoid no)
    {
        DialogTablo.Close.Add(notification, new DialogTablo.CloseDel(yes,no));
        NotificationVoid(notification);
    }

    private void BuildVoid(string nameDialog)
    {
        if(NameLocPanel.Close!=null) NameLocPanel.Close();
        DialogTablo.StopButtonJumpFade.Clear();
        if (DialogNotReplay == nameDialog) return;
        DialogNotReplay = nameDialog;

        var crntDiaog = DictDialog[nameDialog].DialogData;
        if (crntDiaog == null) Debug.LogWarning("Не найден диалоговый блок");

        GameObject tablo = Instantiate(_prefabTablo, transform);

        SaveActiveBttn();
        tablo.GetComponent<DialogTablo>().ShowSprite(crntDiaog, true);
    }
   
    private void BuildWithVoidVoid(string nameDialog, DelegateVoid yes)
    {
        DialogTablo.Close.Add(nameDialog, new DialogTablo.CloseDel(yes, null, null, null, null)) ;
        BuildVoid(nameDialog);
    }
    private void BuildYesNoVoid(string nameDialog, DelegateVoid yes, DelegateVoid no)
    {
        DialogTablo.Close.Add(nameDialog, new DialogTablo.CloseDel(yes, no));
        BuildVoid(nameDialog);
    }
    private void Build_5Void(string nameDialog, DelegateVoid _1, DelegateVoid _2, DelegateVoid _3, DelegateVoid _4, DelegateVoid _5)
    {
        DialogTablo.Close.Add(nameDialog, new DialogTablo.CloseDel(_1, _2, _3, _4, _5));
        BuildVoid(nameDialog);
    }



    private bool CompliteVoid(string nameDialog)
    {
        return DictDialog[nameDialog].Remove;
    }
    private void SaveActiveBttn()
    {
        DialogTablo.BuferLocation = SS.sv.CrntLoc;
        foreach (var bttn in FindObjectsOfType<ButtonJumpFade>())
        {
            if (bttn.enabled)
            {
                bttn.gameObject.SetActive(false);
                DialogTablo.StopButtonJumpFade.Add(bttn);
            }
        }
    }

   
    [System.Serializable]
    public class DialogNameAndCheck
    {
        public string Name;
        public bool Remove;
        public DialogNameAndCheck(string dialog , bool remove)
        {
            Name = dialog;
            Remove = remove;
        }
    }
    public class DialogDataAndCheck
    {
        public DialogData DialogData;
        public bool Remove;
        public DialogDataAndCheck(DialogData dialogData, bool remove)
        {
            DialogData = dialogData;
            Remove = remove;
        }
    }
    


}
