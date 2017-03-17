using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homeCtrl : MonoBehaviour 
{
	[SerializeField]
	private GameObject circle;
	[SerializeField]
	private GameObject menu;
	[SerializeField]
	private GameObject customUI;

	private Vector3 menuOriPos = Vector3.zero;
	// Use this for initialization
	void Awake () 
	{
		menuOriPos = menu.transform.localPosition;
	}

	void OnEnable()
	{
		hideMenu ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void onCircleClick()
	{
		// menu 開著 那就收起來
		if (menu.activeInHierarchy) 
		{
			LeanTween.scale (menu, Vector3.zero, 0.3f).setOnComplete(hideMenu);
			LeanTween.moveLocal (menu, circle.transform.localPosition, 0.3f);
		} 
		else 
		{
			menu.transform.localScale = Vector3.zero;
			menu.transform.localPosition = circle.transform.localPosition;
			menu.SetActive (true);

			LeanTween.scale (menu, Vector3.one, 0.3f);
			LeanTween.moveLocal (menu, menuOriPos, 0.3f);
		}
	}

	public void hideMenu()
	{
		menu.SetActive (false);
	}

	public void onBackClick()
	{
		hideMenu ();
		pageMgr.Instance.prePage ();
	}

	public void onInfoClick()
	{
		if (customUI.activeInHierarchy) 
		{
			hideCustomInfo ();
		}
		else 
		{
			showCustomInfo ();
		}

	}

	private void showCustomInfo()
	{
		// TODO 更新顧客資料
		custom cus = DataMgr.Instance.getOrderingCustom();
		customUI.SetActive (true);
	}

	private void hideCustomInfo()
	{
		customUI.SetActive (false);
	}
}
