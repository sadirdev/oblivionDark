using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameLocPanel : MonoBehaviour
{
    [SerializeField] private GameObject _scrollView;
    [SerializeField] private GameObject _blockClick;
    private GameObject _blockClickTemp;


    private Animation _animation;
    private bool _openName = false;
    public static DelegateVoid Close;

    private void Awake()
    {
        Close = CloseVoid;
    }
    private void Start()
    {
        
        _animation = GetComponent<Animation>();
    }
    public void Click()
    {
        if (_openName) CloseVoid();
        else Open();
    }
    private void Open()
    {
        _blockClickTemp = Instantiate(_blockClick);

        Instantiate(_scrollView, transform);
        _animation.Play("OpenNameLoc");
        _openName = true;
    }
    public void CloseVoid()
    {
        if (!_openName) return;
        _blockClickTemp = Instantiate(_blockClick);
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        _animation.Play("CloseNameLoc");
        _openName = false;
    }
   
    
}
