using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class page5Ctrl : pageBase 
{
	[SerializeField]
	private GameObject switchFilterUI;
	[SerializeField]
	private GameObject indexScroll;
	[SerializeField]
	private GameObject tipTxt;

	private FilterType filterType =  FilterType.STAPLE;
	void Awake () 
	{
		
	}

	void Start()
	{
		switchFilterUI.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.F);

		resetScroll ();
		setTipTxtActive( true );
	}

	public void showSwitchFilter()
	{
		switchFilterUI.GetComponent<switchCtrl> ().showUI ();
	}

	public void OnSubFilterClick( GameObject clickedObj )
	{
		nextPage();
	}

	public void nextPage(  )
	{
		pageMgr.Instance.nextPage (6);
	}

	public void resetScroll( FilterType type )
	{
		filterType = type;
		resetScroll ();
	}

	public void resetScroll()
	{
		// TODO 之後再做顧客偏好篩選
		System.Random seed = new System.Random();
		custom orderingCustom = DataMgr.Instance.getOrderingCustom();
		scrollCtrl ctrl = indexScroll.GetComponent<scrollCtrl> ();
		ctrl.reset ();

		Dictionary<int, Sprite> spriteMap = spriteMgr.Instance.getIndexSpriteMap (filterType, false);
		foreach( int index in spriteMap.Keys )
		{
			GameObject newObj = ctrl.addItem ();
			// TODO 幾道菜等表單建好再計算
			int menuNumber = (seed.Next () % 9) + 1;

			subFilterCtrl sCtrl = newObj.GetComponent<subFilterCtrl> ();
			sCtrl.setInfo (spriteMap [index], menuNumber);
			sCtrl.setSubIndex (index);
		}
	}

	public void setTipTxtActive( bool active )
	{
		tipTxt.SetActive (active);
	}
}
