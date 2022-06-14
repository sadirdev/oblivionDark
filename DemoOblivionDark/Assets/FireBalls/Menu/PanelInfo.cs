using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfo : MonoBehaviour
{
    [SerializeField] private Text _text;
    
    void Start()
    {
        
        

        string name;
        if(SS.sv.Player.Name == Player.Virus)
        {
            if(SS.sv.Lang == lng.rus) name = "Геноса";
            else name = "Genos";

        }
        else
        {
            if (SS.sv.Lang == lng.rus) name = "Вируса";
            else name = "Virus";
        }
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).OnComplete(() =>
        {

            if(SS.sv.Lang == lng.rus)
            {
                if (!SS.sv.ActiveInterface) StartCoroutine(TypePhrase("Спасибо за прохождение игры. Продолжение уже в разработке! Если Вам понравилась игра, поставьте оценку в Google Play :)"));
                else StartCoroutine(TypePhrase($"Для того, чтобы открыть новый блок синхронизации {name}, нужно поднять уровень дрона-сихронизатора."));
            }
            else if (SS.sv.Lang == lng.eng)
            {
                if (!SS.sv.ActiveInterface) StartCoroutine(TypePhrase("Thanks for playing the game. The sequel is already in development! If you like the game, rate it on Google Play :)"));
                else StartCoroutine(TypePhrase($"In order to unlock the new {name} Synchronization Block, you need to level up the Synchronizer Drone."));
            }
            else if (SS.sv.Lang == lng.por)
            {
                if (!SS.sv.ActiveInterface) StartCoroutine(TypePhrase("Obrigado por jogar o jogo. A sequencia ja esta em desenvolvimento! Se voce gosta do jogo, avalie-o no Google Play :)"));
                else StartCoroutine(TypePhrase($"Para abrir um novo bloco de sincronizacao para o {name}, voce precisa aumentar o nivel do drone sincronizador."));
            }



        });

      
    }

    IEnumerator TypePhrase(string text)
    {
        _text.text = "";

        foreach (char letter in text.ToCharArray())
        {
            _text.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void ClickClose()
    {
        FindObjectOfType<MenuBall>().BuilChoice();
    }
   
}
