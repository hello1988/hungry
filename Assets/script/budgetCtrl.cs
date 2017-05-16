using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class budgetCtrl : MonoBehaviour 
{
	[SerializeField]
	private GameObject dollor;
	[SerializeField]
	private GameObject mask;
	[SerializeField]
	private GameObject mode0;
	[SerializeField]
	private GameObject mode1;
	[SerializeField]
	private Image customPhoto;
	[SerializeField]
	private InputField inputBudget;
	[SerializeField]
	private float delaySec = 3;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMaskClick()
	{
		hideUI ();
	}

	public void showUI( int mode )
	{
		gameObject.SetActive (true);
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
		mask.SetActive (true);

		bool isShowMode0 = (mode == 0);
		mode0.SetActive (isShowMode0);
		mode1.SetActive (!isShowMode0);

		custom cus = DataMgr.Instance.getOrderingCustom ();
		customPhoto.sprite = cus.cusPhoto;

		inputBudget.text = Mathf.Max(cus.budget, 0).ToString();

		if(mode == 0)
		{
			if (cus.budget <= 0) 
			{
				StartCoroutine (changeToMode1 ());
			}
			else
			{
				LeanTween.delayedCall (delaySec, hideUI);
			}
		}
	}

	public void saveBudget()
	{
		int iBudget = 0;
		if( int.TryParse(inputBudget.text, out iBudget) )
		{
			custom cus = DataMgr.Instance.getOrderingCustom ();
			cus.budget = iBudget;
		}

		hideUI ();
	}

	public void hideUI()
	{
		StopAllCoroutines ();
		mask.SetActive (false);
		LeanTween.scale (gameObject, Vector3.zero, 0.3f).setOnComplete(hideUI_Step2);
		LeanTween.move (gameObject, dollor.transform, 0.3f);
	}

	public void hideUI_Step2()
	{
		gameObject.SetActive (false);
	}

	private IEnumerator changeToMode1()
	{
		yield return new WaitForSeconds (delaySec);
		mode0.SetActive (false);
		mode1.SetActive (true);

	}

}
