using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Const;

// public class page8Ctrl : pageBase, IPointerDownHandler, IPointerUpHandler
public class page7Ctrl : pageBase, iSyncOrderOption
{
	[SerializeField]
	private GameObject[] syncList;
	[SerializeField]
	private GameObject chooseUI;

	private custom curCustom;
	List<int> menuIDList;
	private int curOrderIndex;

	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (showChoosUI);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.H);
		chooseUI.SetActive (false);

		orderInit ();

		setNextBtnActive (true);
	}

	public void showChoosUI()
	{
		if (DataMgr.Instance.getConfirmCustomList ().Count <= 1) 
		{
			// 一個人點餐就跳過第P8
			nextPage (9);
			return;
		}

		chooseUI.SetActive (true);
		chooseUICtrl ctrl = chooseUI.GetComponent<chooseUICtrl> ();
		ctrl.showUI ();
	}

	public void nextPage( int pageID)
	{
		pageMgr.Instance.nextPage (pageID);
	}

	public void preOrder()
	{
		curOrderIndex = Math.Max ( (curOrderIndex-1), 0);
		showOrder (menuIDList [curOrderIndex]);
	}

	public void nextOrder()
	{
		curOrderIndex = Math.Min ( (curOrderIndex+1), (menuIDList.Count-1));
		showOrder (menuIDList [curOrderIndex]);
	}

	public void OnOrderUnitClick( Staple type )
	{
		for (int index = 0; index < menuIDList.Count; index++) 
		{
			menu m = menuMgr.Instance.getMenuByID (menuIDList[index]);
			if (m.getUseStaple () != type) {continue;}

			curOrderIndex = index;
			showOrder (menuIDList[index]);
			break;
		}
	}

	public void OnDeleteOrderClick()
	{
		if (menuIDList.Count <= 1) {return;}

		int menuID = menuIDList [curOrderIndex];
		curCustom.deleteConfirmMenu (menuID);

		// IDList 重抓 index 修正
		menuIDList = curCustom.getConfirmMenuIDList ();
		curOrderIndex = Math.Min ( curOrderIndex, menuIDList.Count-1 );

		delOrder (menuID);
		LeanTween.delayedCall (0.5f, showOrder);
	}

	// 增加一道餐點
	public void addOrder (int menuID)
	{
		syncOption ("addOrder", menuID);
	}

	// 刪除一道餐點
	public void delOrder (int menuID)
	{
		syncOption ("delOrder", menuID);
	}

	// 修改餐點的分數
	public void modifyOrderNumber(int number)
	{
		int menuID = menuIDList [curOrderIndex];
		curCustom.modifyConfirmMenu ( menuID, number );

		int orderNum = curCustom.getConfirmMenu () [menuID];
		syncOption ("modifyOrderNumber", orderNum);
	}

	// 顯示餐點
	public void showOrder()
	{
		showOrder (menuIDList [curOrderIndex]);
	}

	// 顯示餐點
	public void showOrder(int menuID)
	{
		syncOption ("showOrder", menuID);
	}

	// 重置餐點資料
	public void resetOrder()
	{
		curCustom = DataMgr.Instance.getOrderingCustom ();

		syncOption ("resetOrder");

		menuIDList = curCustom.getConfirmMenuIDList ();
		for( int index = 0;index < menuIDList.Count;index++ )
		{
			addOrder (menuIDList [index]);
		}

		curOrderIndex = 0;
		showOrder(menuIDList [curOrderIndex]);
	}

	public void OnMaskClick()
	{
		chooseUICtrl ctrl = chooseUI.GetComponent<chooseUICtrl> ();
		ctrl.hideUI ();
	}

	private void orderInit()
	{
		resetOrder ();
	}

	private void syncOption( string functionName, object arg = null )
	{
		foreach( GameObject obj in syncList )
		{
			obj.SendMessage ( functionName, arg );
		}
	}
}
