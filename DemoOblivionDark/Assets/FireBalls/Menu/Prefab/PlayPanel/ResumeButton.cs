using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResumeButton : MonoBehaviour
{
	public static bool ShowAds = false;

	private bool b = true;
	[SerializeField] private Image _fill;
	[SerializeField] float _speed = 0.35f;
	[SerializeField] private Text _text;

	[SerializeField] private GameObject _enableIntertet;
	[SerializeField] private GameObject _disableIntertet;

	float time = 0f;
	private CanvasGroup _canvasGroup;


	void Start()
	{

		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			b = false;
			GetComponent<Button>().enabled = false;
			_enableIntertet.SetActive(false);
			_disableIntertet.SetActive(true);
		}
		else
		{
			_enableIntertet.SetActive(true);
			_disableIntertet.SetActive(false);
		}

		_canvasGroup = GetComponent<CanvasGroup>();
		_canvasGroup.alpha = 1;

		



		if (SS.sv.Lang == lng.rus) _text.text = "Продолжить?";
		else if (SS.sv.Lang == lng.eng) _text.text = "Continue?";
		else if (SS.sv.Lang == lng.por) _text.text = "Continuar?";

	}

	void Update()
	{
		if (b)
		{
			time += Time.deltaTime * _speed;
			_fill.fillAmount = time;
			

			if (time > 1)
			{
				b = false;
				time = 0;
				StartCoroutine(HideObj());
			}
		}
	}

	IEnumerator HideObj()
    {
		yield return new WaitForSeconds(2.5f);
		_canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
		{
			Destroy(gameObject);
		});
    }
}
