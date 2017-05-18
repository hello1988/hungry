using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class homeCtrl : MonoBehaviour 
{
	public enum CIRCLE_COLOR
	{
		YELLOW,
		GRAY,
	}

	[SerializeField]
	private GameObject circle;
	[SerializeField]
	private Sprite[] circleSprite;
	[SerializeField]
	private GameObject menu;
	[SerializeField]
	private GameObject customUI;
	[SerializeField]
	private GameObject backBtn;
	[SerializeField]
	private GameObject infoBtn;
	[SerializeField]
	private GameObject mask;

	private Vector3 menuOriPos = Vector3.zero;
	// Use this for initialization
	void Awake () 
	{
		menuOriPos = menu.transform.localPosition;
	}

	void OnEnable()
	{
		hideMenu ();
		hideCustomInfo ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void setCircleColor( CIRCLE_COLOR mode)
	{
		Image img = circle.GetComponent<Image> ();

		int index = ((int)mode) % circleSprite.Length;
		img.sprite = circleSprite[index];
	}

	public void onCircleClick()
	{
		// menu 開著 那就收起來
		if (menu.activeInHierarchy) 
		{
			hideHomeUI ();
		} 
		else 
		{
			showHomeUI ();
		}
	}

	public void hideMenu()
	{
		menu.SetActive (false);
		mask.SetActive (false);
	}

	public void hideMenuOnly()
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
			hideHomeUI (true);
			showCustomInfo ();
		}

	}

	public void showInfoBtn()
	{
		LeanTween.moveLocalY (infoBtn,-120,0.3f);
		mask.SetActive (true);
	}

	public void OnMaskClick()
	{
		hideHomeUI ();
		hideCustomInfo ();
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

	private void hideHomeUI( bool showMask = false )
	{
		if (showMask) 
		{
			LeanTween.scale (menu, Vector3.zero, 0.3f).setOnComplete(hideMenuOnly);
		} 
		else 
		{
			LeanTween.scale (menu, Vector3.zero, 0.3f).setOnComplete(hideMenu);
		}
		LeanTween.moveLocal (menu, circle.transform.localPosition, 0.3f);
		mask.SetActive (showMask);
	}

	private void showHomeUI()
	{
		menu.transform.localScale = Vector3.zero;
		menu.transform.localPosition = circle.transform.localPosition;
		menu.SetActive (true);

		infoBtn.transform.localPosition = backBtn.transform.localPosition;

		LeanTween.scale (menu, Vector3.one, 0.3f);
		LeanTween.moveLocal (menu, menuOriPos, 0.3f).setOnComplete(showInfoBtn);
	}
}
