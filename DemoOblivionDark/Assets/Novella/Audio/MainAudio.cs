using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainAudio : MonoBehaviour
{
    public static MainAudio Static;

    [SerializeField] private AudioMixerGroup _mixer;

    [SerializeField] private AudioClip[] _audioClipsGenos;
    [SerializeField] private AudioClip[] _audioClipsVirus;
    [SerializeField] public AudioSource AudioSource;
    [SerializeField] private AudioClip _startAudio;
    [SerializeField] private AudioClip _battleAudio;
    [SerializeField] private AudioClip _sliderAudio;

    private int _crntIndex;
    private AudioClip[] _crntAudioClips;

    private IEnumerator _fon;

    private void Awake()
    {
        Static = this;
        _fon = MusicPlay();
    }

    private void Start()
    {
        if (Dialog.IsComplite("start"))
        {
            PlayFon();
        }
       

        _mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, SS.sv.MusicFill));
        _mixer.audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, SS.sv.SoundFill));
    }

    public void PlayFon()
    {
        

        SS.sv.Player.IndexMusic++;
        _crntIndex++;

        if (_crntAudioClips == null)
        {
            if (SS.sv.Player.Name == Player.Genos)
            {
                _crntAudioClips = _audioClipsGenos;
            }
            else
            {
                _crntAudioClips = _audioClipsVirus;
            }
        }

        if (SS.sv.Player.IndexMusic >= _crntAudioClips.Length)
        {
            SS.sv.Player.IndexMusic = 0;
            _crntIndex = 0;
        }
        //AudioSource.Stop();
        StopCoroutine(_fon);
        _fon = MusicPlay();

        StartCoroutine(_fon);
       
    }
    public void PlayStart()
    {
        //StopFon();
        AudioSource.clip = _startAudio;
        AudioSource.Play();
    }
    public void PlayBattle()
    {
       // StopFon();
        AudioSource.clip = _battleAudio;
        AudioSource.Play();
    }
    public void PlaySlider()
    {
       //StopFon();
        AudioSource.clip = _sliderAudio;
        AudioSource.Play();
    }

    IEnumerator MusicPlay()
    {
        
        _crntIndex = SS.sv.Player.IndexMusic;
        if (SS.sv.Player.Name == Player.Genos)
        {
            _crntAudioClips = _audioClipsGenos;
        }
        else
        {
            _crntAudioClips = _audioClipsVirus;
        }
        
        while (true)
        {
            AudioSource.clip = _crntAudioClips[_crntIndex];
            AudioSource.Play();
            yield return new WaitForSeconds(AudioSource.clip.length);
            _crntIndex++;
            SS.sv.Player.IndexMusic++;
            if (_crntIndex>= _crntAudioClips.Length)
            {
                SS.sv.Player.IndexMusic = 0;
                _crntIndex = 0;
            }
        }
    }
    //public void StopFon()
    //{
    //    SS.sv.Player.IndexMusic++;
    //    if (SS.sv.Player.IndexMusic >= _crntAudioClips.Length)
    //    {
    //        SS.sv.Player.IndexMusic = 0;
    //        _crntIndex = 0;
    //    }
    //    //AudioSource.Stop();
    //    StopCoroutine(_fon);
    //}
}
