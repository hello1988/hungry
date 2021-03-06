﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Const;

public class page8Ctrl : pageBase
{
	[SerializeField]
	private GameObject checkOrderUI;

	private List<orderData> orderDataList;
	private int curOrder = 0;

	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);
		circleColor = homeCtrl.CIRCLE_COLOR.GRAY;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.H2);
		setNextBtnActive (true);

		orderDataList = new List<orderData> ();
		List<custom> customList = DataMgr.Instance.getConfirmCustomList ();
		foreach( custom cus in customList )
		{
			Dictionary<int,int> confirmMenu = cus.getConfirmMenu ();
			List<int> menuIDList = cus.getConfirmMenuIDList ();

			for (int index = 0; index < menuIDList.Count; index++) 
			{
				int menuID = menuIDList [index];
				menu m = menuMgr.Instance.getMenuByID (menuID);
				Staple type = m.getUseStaple ();

				orderData data = new orderData ();
				data.orderFloor = spriteMgr.Instance.getSprite (spriteMgr.KeyWord.WANT_ORDER, menuID);
				data.orderNum = confirmMenu[menuID];
				data.customPhoto = cus.cusPhoto;

				data.nextSprite = spriteMgr.Instance.getSprite (spriteMgr.KeyWord.ORDER_STAPLE_L, (int)type);
				orderDataList.Add (data);
			}
		}

		curOrder = 0;
		checkOrderCtrl checkCtrl = checkOrderUI.GetComponent<checkOrderCtrl> ();
		checkCtrl.setInfo (orderDataList[curOrder]);

	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (9);
	}

	public void preOrder()
	{
		// Debug.logger.Log (string.Format("preOrder : ({0} -1) < 0",curOrder));
		if ((curOrder - 1) < 0) {return;}

		curOrder--;
		checkOrderCtrl checkCtrl = checkOrderUI.GetComponent<checkOrderCtrl> ();
		// checkCtrl.setInfo (orderDataList[curOrder]);
		checkCtrl.showPreOrder(orderDataList[curOrder]);

	}

	public void nextOrder()
	{
		// Debug.logger.Log (string.Format("nextOrder : ({0} +1) > ({1}-1)",curOrder,orderDataList.Count));
		if ((curOrder + 1) > (orderDataList.Count-1)) {return;}

		curOrder++;
		checkOrderCtrl checkCtrl = checkOrderUI.GetComponent<checkOrderCtrl> ();
		// checkCtrl.setInfo (orderDataList[curOrder]);
		checkCtrl.showNextOrder(orderDataList[curOrder]);

	}
}

public class orderData
{
	public Sprite orderFloor;
	public int orderNum;
	public Sprite customPhoto;

	public Sprite nextSprite;
}