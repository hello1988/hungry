﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class page3Ctrl : pageBase 
{
	[SerializeField]
	private GameObject focusImage;	// 過濾器(大圖)
	[SerializeField]
	private GameObject filterScroll; // 過濾器捲動區
	[SerializeField]
	private GameObject budget;

	private selectFilter focusFilter;


	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);

		budget.GetComponent<slidAndClick> ().setCallBack (slidAndClick.Direction.UP, addBudget);
		budget.GetComponent<slidAndClick> ().setCallBack (slidAndClick.Direction.DOWN, minusBudget);
	}

	void Start()
	{
		initFilterScroll ();
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.E);
		setNextBtnActive(true);
		resetScrollItem ();

		custom cus = DataMgr.Instance.getOrderingCustom ();
		budget.GetComponent<numberCtrl>().setValue (cus.budget);

		focusImage.GetComponent<dragAndDrop> ().setDragable (false);
		Image img = focusImage.GetComponent<Image> ();
		img.sprite = null;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void onFilterClick( selectFilter arg )
	{
		focusFilter = arg;
		FilterType type = focusFilter.getFilterType ();
		int index = focusFilter.getFilterIndex ();

		focusImage.GetComponent<dragAndDrop> ().setDragable (true);
		Image img = focusImage.GetComponent<Image> ();
		img.sprite = spriteMgr.Instance.getSprite (type,true,index);
	}

	public void onItemDrop(GameObject item)
	{
		if (focusFilter == null) {return;}

		custom orderingCus = DataMgr.Instance.getOrderingCustom ();
		orderingCus.addPreferFilter ( focusFilter.getFilterType(), focusFilter.getFilterIndex() );

		focusFilter.setSelected (true);
	}

	public void addBudget()
	{
		custom cus = DataMgr.Instance.getOrderingCustom ();
		cus.budget = Math.Min( (cus.budget+20), 1000 );
		budget.GetComponent<numberCtrl>().setValue (cus.budget);
	}

	public void minusBudget()
	{
		custom cus = DataMgr.Instance.getOrderingCustom ();
		cus.budget = Math.Max( (cus.budget-20), 0 );
		budget.GetComponent<numberCtrl>().setValue (cus.budget);
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (4);
	}

	private void resetScrollItem()
	{
		custom cus = DataMgr.Instance.getOrderingCustom ();
		Dictionary<FilterType, List<int>> preferFilter = cus.getPreferFilter ();
		scrollCtrl ctrl = filterScroll.GetComponent<scrollCtrl> ();
		foreach (GameObject obj in ctrl.getItemList()) 
		{
			selectFilter fImg = obj.GetComponent<selectFilter> ();

			bool isSelected = preferFilter [fImg.getFilterType ()].Contains (fImg.getFilterIndex());
			fImg.setSelected (isSelected);
		}
	}

	private void initFilterScroll()
	{
		scrollCtrl ctrl = filterScroll.GetComponent<scrollCtrl> ();

		FilterType type = FilterType.COOK;
		string keyWord = spriteMgr.Instance.getFilterSpriteKeyWord (type, false);
		if (string.IsNullOrEmpty (keyWord)) {return;;}

		Dictionary<int, Sprite> spriteMap = spriteMgr.Instance.getSpriteMap (keyWord);
		foreach (int index in spriteMap.Keys) 
		{
			GameObject newObj = ctrl.addItem ();
			selectFilter fImg = newObj.GetComponent<selectFilter> ();
			fImg.setFilterType (type);
			fImg.setFilterIndex (index);
			newObj.GetComponent<Image> ().sprite = spriteMap[index];
		}

	}
}
