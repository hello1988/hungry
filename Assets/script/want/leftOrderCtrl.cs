using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class leftOrderCtrl : MonoBehaviour, iSyncOrderOption
{
	[SerializeField]
	private page7Ctrl pageCtrl;
	[SerializeField]
	private GameObject template;

	private Dictionary<Staple, GameObject> orderObjInfo;
	private Staple removeType;

	void Awake () 
	{
		orderObjInfo = new Dictionary<Staple, GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 增加一道餐點
	public void addOrder (int menuID)
	{
		menu m = menuMgr.Instance.getMenuByID (menuID);
		Staple type = m.getUseStaple ();

		if (orderObjInfo.ContainsKey (type)) {return;}

		GameObject clone = Instantiate<GameObject> (template);
		clone.transform.SetParent ( transform );
		clone.name = string.Format ("order{0}",(int)type);
		clone.transform.localScale = Vector3.one;

		orderUnit unit = clone.GetComponent<orderUnit> ();
		Sprite sprite = spriteMgr.Instance.getSprite (spriteMgr.KeyWord.WANT_STAPLE, (int)type);
		unit.setOrder ( type, sprite );
		unit.setSelected (false, false);

		orderObjInfo.Add (type, clone);
		clone.SetActive (true);
	}

	// 刪除一道餐點
	public void delOrder (int menuID)
	{
		menu m = menuMgr.Instance.getMenuByID (menuID);
		Staple type = m.getUseStaple ();

		custom cus = DataMgr.Instance.getOrderingCustom ();
		List<int> menuIDList = cus.getConfirmMenuIDList ();
		foreach( int id in menuIDList )
		{
			menu menuData = menuMgr.Instance.getMenuByID (id);
			if (menuData.getUseStaple () == type) 
			{
				return;
			}
		}

		removeType = type;
		LeanTween.scale (orderObjInfo [type], Vector3.zero, 0.3f).setOnComplete (delOrder_step2);

	}

	public void delOrder_step2()
	{
		// if (removeType == null) {return;}
		if (!orderObjInfo.ContainsKey(removeType)) {return;}

		Destroy (orderObjInfo[removeType]);
		orderObjInfo.Remove (removeType);
	}

	// 修改餐點的分數
	public void modifyOrderNumber(int orderNum){}

	// 顯示餐點
	public void showOrder(int menuID)
	{
		rePosition ();
		menu m = menuMgr.Instance.getMenuByID (menuID);

		foreach (Staple type in orderObjInfo.Keys)
		{
			GameObject orderObj = orderObjInfo[type];
			orderUnit unit = orderObj.GetComponent<orderUnit> ();

			if (m.getUseStaple() == type) 
			{
				unit.setSelected (true);
			}
			else 
			{
				unit.setSelected (false);
			}
		}
	}

	// 重置餐點資料
	public void resetOrder()
	{
		foreach( Staple type in orderObjInfo.Keys )
		{
			Destroy (orderObjInfo[type]);
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
			if (!orderObjInfo.ContainsKey (type)) {continue;}

			GameObject obj = orderObjInfo[type];
			float posY = startPos.y - (rect.sizeDelta.y + 10) * counter;
			obj.transform.localPosition = new Vector3 (startPos.x, posY, startPos.z );
			counter++;
		}

	}
}
