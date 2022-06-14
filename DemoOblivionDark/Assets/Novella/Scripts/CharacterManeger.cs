using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManeger : MonoBehaviour
{
    public CharactersList[] Characters;
    public DialogData[] DialogData;
    public Clicker.Enemy[] Enemies;
   
    [HideInInspector] public static Dictionary<Character, CharactersList> CharDict = new Dictionary<Character, CharactersList>();


    private void Awake()
    {
        GenerateEnemy();
        CharDict.Clear();
        foreach (var item in Characters)
        {
            CharDict.Add(item.Character, new CharactersList(item.NameRus, item.NameEng, item.NamePor, item.Sprite));
        }


        //foreach (var data in DialogData)
        //{
        //    foreach (var phrase in data.Phrases)
        //    {
        //        if (!phrase.SkipPhrase)
        //        {
        //            string phraseTemp = phrase.TextRus;
        //            phraseTemp = phraseTemp.ToUpper();

        //            if (string.IsNullOrWhiteSpace(phraseTemp))
        //            {
        //                Debug.LogWarning($"data={data.name} , CHOUCE = {phraseTemp}");
        //            }

        //            foreach (var menu in phrase.Menu)
        //            {

        //                string CHOICETemp = menu.ChoiceRus;
        //                CHOICETemp = CHOICETemp.ToUpper();


        //                if (string.IsNullOrWhiteSpace(CHOICETemp))
        //                {
        //                    Debug.LogWarning($"data={data.name} , CHOUCE = {CHOICETemp}");
        //                }

        //                //for (int i = 0; i < CHOICETemp.Length; i++)
        //                //{
        //                //    char c = CHOICETemp[i];
        //                //    if ((c >= 'А') && (c <= 'Я'))
        //                //    {
        //                //        Debug.LogWarning($"data={data.name} , CHOUCE = {CHOICETemp}");
        //                //        break;
        //                //    }

        //                //}



        //            }
        //        }

        //    }
        //}
    }
    private void GenerateEnemy()
    {
        

        Clicker.DictEnemy.Clear();
        foreach (var enemy in Enemies)
        {
            Clicker.DictEnemy.Add(enemy.Name, enemy);
        }
    }

    [System.Serializable]
    public class CharactersList
    {
        public Character Character;
        //public string Name;
        public string NameRus;
        public string NameEng;
        public string NamePor;
        public Sprite Sprite;
        public CharactersList(string nameRus, string nameEng, string namePor, Sprite sprite)
        {
            NameRus = nameRus;
            NameEng = nameEng;
            NamePor = namePor;
            Sprite = sprite;
        }
    }
}

public enum Character { GenosGood, Virus, SaitamaGood, SaitamaBad, Kuseno, Null, Sonic, WatchDog, CrabMan, Bang, АтомныйСамурай, King, Фанатка1, Фанатка2, СайтамаГражданский,
    Дочь, Богомол, ОтецДочери, Зенко, Битаб, Мумен, Маругори, Бандит, DrGenus}


