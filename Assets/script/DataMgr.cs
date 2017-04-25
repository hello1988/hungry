using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const
{
	public enum FilterType
	{
		COOK,	// 料理方式
		FOOD,	// 食材
		STAPLE,	// 主食
	}

	// 料理方式子分類
	public enum Cook
	{
		FRY = 1,		// 炸
		RAW = 2,		// 生食
		GRILL = 3,		// 烤
		PARCH = 4,		// 炒
		BROIL = 5,		// 煮
		STEAM = 6,		// 蒸
		DECOCT = 7,		// 煎
	}

	// 食材子分類
	public enum Food
	{
		BEEF = 1,		// 牛肉
		LAMB = 2,		// 羊肉
		PORK = 3,		// 豬肉
		CHICKEN = 4,	// 雞肉
		SEA_FOOD = 5,	// 海鮮
		VEGETABLE = 6,	// 素
	}

	// 主食子分類
	public enum Staple
	{
		RICE = 1,		// 飯
		SUSHI = 2,		// 壽司
		NOODLE = 3,		// 麵
		ROLL = 4,		// 手卷
		SWEETS = 5,		// 甜食
		SOUP = 6,		// 湯
		DRINK = 7,		// 飲料
		DUMPLINGS = 8,	// 餃子
		POT = 9,		// 鍋物
	}

}

public class DataMgr : MonoBehaviour 
{

#pragma warning disable 0414
	// 店員編號
	private string staffNumber = "";
	// 桌號
	private string tableNumber = "";
#pragma warning restore 0414
	// 顧客人數
	private int customNum = 0;
	// 搜尋到的顧客
	private List<custom> searchCustomList = new List<custom>();
	// 要點餐的顧客
	private List<custom> confirmCustomList = new List<custom>();

	private custom orderingCustom;

	private static DataMgr _instance = null;
	public static DataMgr Instance
	{
		get{return _instance;}
	}

	// Use this for initialization
	private void Awake ()
	{
		_instance = this;
	}

	public void Start()
	{
		prepareFakeCustom ();
	}

	public void setStaffNumber( string staff )
	{
		staffNumber = staff;
	}

	public void setTableNumber( string table )
	{
		tableNumber = table;
	}

	public void setCustomNum( int num )
	{
		customNum = Math.Min( num, 7 );
	}

	public List<custom> getSearchCustomList()
	{
		return searchCustomList;
	}


	public void setConfirmCustomList( List<custom> cusList )
	{
		confirmCustomList = cusList;

		int newCustomNum = customNum - confirmCustomList.Count;
		if (newCustomNum > 0) 
		{
			for( int count = 0; count < newCustomNum;count++ )
			{
				custom newCustom = custom.createDefaultCustom (string.Format("新顧客{0}",count+1));
				confirmCustomList.Add (newCustom);
			}
		}
	}

	public List<custom> getConfirmCustomList()
	{
		if (confirmCustomList.Count > 0) 
		{
			return confirmCustomList;
		}

		List<custom> defaultList = new List<custom> ();
		defaultList.Add (getOrderingCustom());
		return defaultList;
	}

	public void setOrderingCustom( custom orderCus )
	{
		foreach (custom cus in confirmCustomList) 
		{
			if (orderCus.customID != cus.customID) {continue;}

			orderingCustom = orderCus;
			break;
		}

	}

	public custom getOrderingCustom()
	{
		//Test
		if( orderingCustom == null )
		{
			orderingCustom = custom.createDefaultCustom ("default");
		}
		return orderingCustom;
	}

	public void nextCustom()
	{
		if (orderingCustom == null) 
		{
			orderingCustom = confirmCustomList [0];
		}
		else 
		{
			for (int index = 0;index < confirmCustomList.Count;index++) 
			{
				if (confirmCustomList [index].customID != orderingCustom.customID) {continue;}

				if ((index + 1) >= confirmCustomList.Count) {break;}

				orderingCustom = confirmCustomList [index + 1];
				break;
			}
		}
	}

	// TEST
	private void prepareFakeCustom()
	{
		custom cus = new custom ();
		cus.cusPhoto = Resources.Load<Sprite>("custom/photo1");
		cus.cusName = "陳大明";
		searchCustomList.Add (cus);

		cus = new custom ();
		cus.cusPhoto = Resources.Load<Sprite>("custom/photo2");
		cus.cusName = "王小明";
		searchCustomList.Add (cus);

		cus = new custom ();
		cus.cusPhoto = Resources.Load<Sprite>("custom/photo1");
		cus.cusName = "路人1號";
		searchCustomList.Add (cus);

		cus = new custom ();
		cus.cusPhoto = Resources.Load<Sprite>("custom/photo2");
		cus.cusName = "路人2號";
		searchCustomList.Add (cus);

		cus = new custom ();
		cus.cusPhoto = Resources.Load<Sprite>("custom/photo2");
		cus.cusName = "路人3號";
		searchCustomList.Add (cus);

		cus = new custom ();
		cus.cusPhoto = Resources.Load<Sprite>("custom/photo1");
		cus.cusName = "路人4號";
		searchCustomList.Add (cus);

		cus = new custom ();
		cus.cusPhoto = Resources.Load<Sprite>("custom/photo2");
		cus.cusName = "路人5號";
		searchCustomList.Add (cus);
	}

	public void resetData()
	{
		staffNumber = "";
		tableNumber = "";
		searchCustomList = new List<custom>();
		confirmCustomList = new List<custom>();

		Start ();
	}
}

