﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page6Ctrl : pageBase 
{
	[SerializeField]
	private GameObject filterScroll;

	// private Button checkButton;
	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.C);
		setNextBtnActive(true);

		resetScroll ();
	}

	private void resetScroll()
	{
		custom orderingCus = DataMgr.Instance.getOrderingCustom ();
		Dictionary<DataMgr.FilterType, List<int>> preferFilter = orderingCus.getPreferFilter ();

		scrollCtrl ctrl = filterScroll.GetComponent<scrollCtrl> ();
		ctrl.reset ();
		foreach(DataMgr.FilterType type in preferFilter.Keys)
		{
			foreach( int index in preferFilter[type] )
			{
				GameObject newObj = ctrl.addItem ();
				checkFilter cFilter = newObj.GetComponent<checkFilter> ();
				cFilter.setInfo ( type, index, spriteMgr.Instance.getSprite(type, false, index) );
			}
		}
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (7);
	}

	public void deleteFilter(GameObject delItem)
	{
		checkFilter chkFilter = delItem.GetComponent<checkFilter> ();
		scrollCtrl ctrl = filterScroll.GetComponent<scrollCtrl> ();

		ctrl.delItem (delItem.name);
		custom orderingCus = DataMgr.Instance.getOrderingCustom ();
		orderingCus.removePreferFilter ( chkFilter.getFilterType(), chkFilter.getFilterIndex() );
	}
}
