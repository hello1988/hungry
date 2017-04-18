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
		FRY = 1,	// 炸
		RAW,		// 生食
		BROIL,		// 烤
	}

	// 食材子分類
	public enum Food
	{
		BEEF = 1,	// 牛肉
		LAMB,		// 羊肉
		PORK,		// 豬肉
		CHICKEN,	// 雞肉
	}

	// 主食子分類
	public enum Staple
	{
		RICE = 1,	// 飯
		SUSHI,		// 壽司
		NOODLE,		// 麵
	}
}

public class DataMgr : MonoBehaviour 
{
	// 店員編號
	private string staffNumber = "";
	// 桌號
	private string tableNumber = "";
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
		customNum = num;
	}

	public List<custom> getSearchCustomList()
	{
		return searchCustomList;
	}


	public void setConfirmCustomList( List<custom> cusList )
	{
		confirmCustomList = cusList;
	}

	public List<custom> getConfirmCustomList()
	{
		if (confirmCustomList.Count > 0) 
		{
			return confirmCustomList;
		}

		List<custom> defaultList = new List<custom> ();
		defaultList.Add (orderingCustom);
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

