using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialClick : MonoBehaviour
{
    [SerializeField] private GameObject _click;




    void Start()
    {
        if(SS.sv.CrntScene == LoadingScreen.CrntScene.Novella) StartCoroutine(enumeratorClick());
        else StartCoroutine(enumeratorKeep());
    }
    IEnumerator enumeratorClick()
    {
        while (true)
        {
            _click.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _click.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    IEnumerator enumeratorKeep()
    {
        _click.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        
        
        while (true)
        {
            _click.SetActive(true);
            yield return new WaitForSeconds(3);
            _click.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

    }


    public void Close()
    {
        if (SS.sv.CrntScene == LoadingScreen.CrntScene.FireBall) Tank.BulletGun = true;
        Destroy(gameObject);
    }
}
