using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdaptiveText : MonoBehaviour
{
    [SerializeField] private string _rus;
    [SerializeField] private string _eng;
    [SerializeField] private string _por;




    private float _maxSize;
    private TMP_Text _text;

    public void Start()
    {
        _text = GetComponent<TMP_Text>();
        _maxSize = _text.fontSizeMax;
        _text.fontSizeMax = _maxSize * AdaptiveObject.Delta();

        Translate(); 
    }


    public void Translate()
    {
        if (string.IsNullOrEmpty(_rus)) return;
        switch (SS.sv.Lang)
        {
            case lng.rus: _text.text = _rus; break;
            case lng.eng: _text.text = _eng; break;
            case lng.por: _text.text = _por; break;
        }
    }
   
}
