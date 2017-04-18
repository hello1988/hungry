using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Const;

public class leftOrderCtrl : MonoBehaviour, iSyncOrderOption, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private page7Ctrl pageCtrl;
	[SerializeField]
	private GameObject template;

	private Dictionary<Staple, List<int>> orderInfo;
	private Dictionary<int, GameObject> orderObjInfo;
	private GameObject touchPoint;
	private Vector3 startPos = Vector3.zero;

	void Awake () 
	{
		touchPoint = new GameObject ("touchPoint");
		touchPoint.transform.SetParent (transform.parent);

		orderInfo = new Dictionary<Staple, List<int>> ();
		foreach (Staple type in Enum.GetValues(typeof(Staple))) 
		{
			orderInfo.Add (type, new List<int>());
		}

		orderObjInfo = new Dictionary<int, GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		startPos = touchPoint.transform.localPosition;
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		Vector3 endPos = touchPoint.transform.localPosition;

		float offsetY = (endPos - startPos).y;
		if (offsetY > 300) 
		{
			// next order
			pageCtrl.nextOrder();
		}
		else if (offsetY < -300) 
		{
			// pre order
			pageCtrl.preOrder();
		}
	}

	// 增加一道餐點
	public void addOrder (int menuID)
	{
		menu m = menuMgr.Instance.getMenuByID (menuID);
		Staple type = m.getUseStaple ();
		if (orderInfo [type].Contains (menuID)) {return;}

		orderInfo [type].Add (menuID);
		GameObject clone = Instantiate<GameObject> (template);
		clone.transform.SetParent ( transform );
		clone.name = string.Format ("order{0}",menuID);
		clone.transform.localScale = Vector3.one;

		orderUnit unit = clone.GetComponent<orderUnit> ();
		Sprite sprite = spriteMgr.Instance.getSprite (spriteMgr.KeyWord.WANT_STAPLE, menuID);
		unit.setOrderImg ( sprite );
		unit.showDot (false);

		orderObjInfo.Add (menuID, clone);
		clone.SetActive (true);
	}

	// 刪除一道餐點
	public void delOrder (int menuID)
	{
		menu m = menuMgr.Instance.getMenuByID (menuID);
		Staple type = m.getUseStaple ();
		if (!orderInfo [type].Remove (menuID)) {return;}

		Destroy (orderObjInfo[menuID]);
		orderObjInfo.Remove (menuID);
	}

	// 修改餐點的分數
	public void modifyOrderNumber(int orderNum)
	{
		
	}

	// 顯示餐點
	public void showOrder(int menuID)
	{
		rePosition ();

		foreach (int ID in orderObjInfo.Keys)
		{
			GameObject orderObj = orderObjInfo[ID];
			orderUnit unit = orderObj.GetComponent<orderUnit> ();

			if (menuID == ID) 
			{
				unit.showOrderImg ();
			}
			else 
			{
				unit.showDot ();
			}
		}
	}

	// 重置餐點資料
	public void resetOrder()
	{
		foreach (Staple type  in orderInfo.Keys) 
		{
			orderInfo[type].Clear ();
		}

		foreach( int menuID in orderObjInfo.Keys )
		{
			Destroy (orderObjInfo[menuID]);
		}
		orderObjInfo.Clear ();
	}

	private void rePosition ()
	{
		RectTransform rect = template.GetComponent<RectTransform> ();
		Vector3 startPos = template.transform.localPosition;

		int counter = 0;
		foreach (Staple type in Enum.GetValues(typeof(Staple))) 
		{
			if (orderInfo [type].Count <= 0) {continue;}

			for (int index = 0; index < orderInfo [type].Count; index++) 
			{
				int menuID = orderInfo [type] [index];
				GameObject obj = orderObjInfo[menuID];
				float posY = startPos.y - (rect.sizeDelta.y + 10) * counter;
				obj.transform.localPosition = new Vector3 (startPos.x, posY, startPos.z );
				counter++;
			}
		}
	}
}
