using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// public class page8Ctrl : pageBase, IPointerDownHandler, IPointerUpHandler
public class page7Ctrl : pageBase, iSyncOrderOption
{
	[SerializeField]
	private GameObject[] syncList;

	private custom curCustom;
	List<int> menuIDList;
	private int curOrderIndex;

	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.H);

		orderInit ();

		setNextBtnActive (true);
	}


	public void nextPage()
	{
		if (DataMgr.Instance.getConfirmCustomList ().Count <= 1) 
		{
			// 一個人點餐就跳過第P8
			pageMgr.Instance.nextPage (9);
		}
		else 
		{
			pageMgr.Instance.nextPage (8);
		}
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
