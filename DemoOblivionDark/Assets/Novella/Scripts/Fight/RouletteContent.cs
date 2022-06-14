using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;

public class RouletteContent : MonoBehaviour
{
    [SerializeField]private Segment[] _segments = new Segment[8];
    [SerializeField] private GameObject _prefabSegment;
    [SerializeField] private GameObject _prefabFocus;
    public GameObject FocusLine;
    [SerializeField] private GameObject _fight;
    [SerializeField] private FightDialog _fightDialog;
    [SerializeField] private float _durationRotate;
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> _rotator;

    private void Start()
    {

        
        float minAngles = -22.5f;
        float maxAngles = 22.5f;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (_segments[i].Sprite != null)
            {
                Transform segment = Instantiate(_prefabSegment.transform, transform.GetChild(i));
                
                segment.GetChild(0).GetComponent<Image>().sprite = _segments[i].Sprite;
            }
            _segments[i].MinAngles = minAngles;
            _segments[i].MaxAngles = maxAngles;

            minAngles += 45;
            maxAngles += 45;
        }
        
        _fight.GetComponent<CanvasGroup>().DOFade(1, DialogFon.DurationFade).OnComplete(() =>
        {
            StartRotator();
        });

    }
   
    public void StartRotator()
    {
        _rotator = transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, -360), _durationRotate, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).OnKill(() =>
        {

            foreach (var segment in _segments)
            {
                if (segment.MinAngles <= transform.eulerAngles.z && segment.MaxAngles >= transform.eulerAngles.z)
                {
                    
                    SelectedSegmend(segment);
                    return;
                }
            }
            SelectedSegmend(_segments[0]);
        });
        void SelectedSegmend(Segment segment)
        {
            float finishArrow = (segment.MinAngles + segment.MaxAngles) / 2;

            var focus = Instantiate(_prefabFocus, transform.GetChild(Array.IndexOf(_segments, segment)));
            focus.transform.SetSiblingIndex(0);
            transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, finishArrow), 1, RotateMode.Fast).OnComplete(() =>
            {
                // Конец
                FocusLine.SetActive(true);
                _fightDialog.MovePlayer(segment.Damage, focus);
            });

        }
    }

    public void CLick()
    {
        _rotator.Kill();
    }

    [Serializable]
    class Segment
    {
        [HideInInspector] public float MinAngles;
        [HideInInspector] public float MaxAngles;
        public string Name;
        public Sprite Sprite;
        public float Damage;
    }
}
