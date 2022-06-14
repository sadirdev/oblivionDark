using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System;

public class Preferens : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;

    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sound;

    [SerializeField] private TMP_Text _textLaunge;

    private lng[] _lngs = new lng[] { lng.rus, lng.eng, lng.por };
    private int _indexLng;

    private void Start()
    {

        _indexLng = Array.IndexOf(_lngs, SS.sv.Lang);



        _music.value = SS.sv.MusicFill;
        _sound.value = SS.sv.SoundFill;
    }



    public void ChangeVolumeMusic(float volume)
    {
        _mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
        SS.sv.MusicFill = volume;
    }
    public void ChangeVolumeSound(float volume)
    {
        _mixer.audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 6, volume));
        SS.sv.SoundFill = volume;
    }

    public void NextLaunge()
    {
        _indexLng++;
        UpdateLng();
    }
    public void PrevLaunge()
    {
        _indexLng--;
        UpdateLng();
    }


    private void UpdateLng()
    {
        if (_indexLng < 0) _indexLng = _lngs.Length - 1;
        else if (_indexLng >= _lngs.Length) _indexLng = 0;

        SS.sv.Lang = _lngs[_indexLng];
        foreach (var item in FindObjectsOfType<AdaptiveText>())
        {
            item.Translate();
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
