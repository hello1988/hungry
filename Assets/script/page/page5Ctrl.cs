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
		custom cus = DataMgr.Instance.getOrderingCustom ();
		subFilterCtrl ctrl = clickedObj.GetComponent<subFilterCtrl>();
		int idx = ctrl.getSubIndex();
		cus.sortMenu (filterType, idx);

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
		Dictionary<int,int> filterMap = new Dictionary<int, int> ();
		custom orderingCustom = DataMgr.Instance.getOrderingCustom();
		foreach( menu m in orderingCustom.getPreferMenu() )
		{
			int subIdx = m.getSubIndexByType (filterType);
			if( !filterMap.ContainsKey(subIdx) )
			{
				filterMap.Add (subIdx, 0);
			}
			filterMap [subIdx] += 1;
		}

		scrollCtrl ctrl = indexScroll.GetComponent<scrollCtrl> ();
		ctrl.reset ();

		Dictionary<int, Sprite> spriteMap = spriteMgr.Instance.getIndexSpriteMap (filterType, false);
		foreach( int index in spriteMap.Keys )
		{
			if (!filterMap.ContainsKey (index)) {continue;}

			GameObject newObj = ctrl.addItem ();
			int menuNumber = filterMap[index];

			subFilterCtrl sCtrl = newObj.GetComponent<subFilterCtrl> ();
			sCtrl.setInfo (spriteMap [index], menuNumber);
			sCtrl.setSubIndex (index);
		}
	}

	public void setTipTxtActive( bool active )
	{
		tipTxt.SetActive (active);
		UIMgr.Instance.setHomeBtnVisible (active);
	}
}
