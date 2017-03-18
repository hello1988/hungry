using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoBehaviour 
{
	public enum FilterType
	{
		COOK,	// 料理方式
		FOOD,	// 食材
		STAPLE,	// 主食
	}

	// 店員編號
	private string staffNumber = "";
	// 桌號
	private string tableNumber = "";
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
		filterInfo.buildFilterMap ();
		orderingCustom = custom.createDefaultCustom ("default");
		prepareFakeCustom ();
	}

	public void setStaffNumber( string staff )
	{
		staffNumber = staff;
	}

	public string getStaffNumber()
	{
		return staffNumber;
	}

	public void setTableNumber( string table )
	{
		tableNumber = table;
	}

	public string getTableNumber()
	{
		return tableNumber;
	}

	public List<custom> getSearchCustomList()
	{
		return searchCustomList;
	}

	public void moveCustomToConfirm ( int indexInSearch )
	{
		if ((indexInSearch < 0) || (indexInSearch >= searchCustomList.Count)) 
		{
			return;
		}

		custom cus = searchCustomList[indexInSearch];
		searchCustomList.RemoveAt (indexInSearch);
		confirmCustomList.Add (cus);
	}

	public void addCustomToConfirm( custom cus )
	{
		confirmCustomList.Add (cus);
	}

	public List<custom> getConfirmCustomList()
	{
		return confirmCustomList;
	}

	public void setOrderingCustom( int confirmIndex )
	{
		if ((confirmIndex < 0) || (confirmIndex >= this.confirmCustomList.Count)) {return;}

		orderingCustom = confirmCustomList [confirmIndex];
	}

	public custom getOrderingCustom()
	{
		return orderingCustom;
	}

	// TEST
	private void prepareFakeCustom()
	{
		custom cus = new custom ();
		cus.cusPhoto = Resources.Load<Sprite>("custom/photo1");
		cus.cusName = Resources.Load<Sprite>("custom/name1");
		searchCustomList.Add (cus);

		cus = new custom ();
		cus.cusPhoto = Resources.Load<Sprite>("custom/photo2");
		cus.cusName = Resources.Load<Sprite>("custom/name2");
		searchCustomList.Add (cus);
	}
}

class filterInfo
{
	public static readonly string smallImgPath = "cook/cook_S/cook{0}";
	public static readonly string bigImgPath = "cook/cook_L/cook{0}";

	private static Dictionary<DataMgr.FilterType,Dictionary<int, filterInfo>> filterMap;

	public Sprite sprite_S;
	public Sprite sprite_L;

	public static void buildFilterMap()
	{
		filterMap = new Dictionary<DataMgr.FilterType,Dictionary<int, filterInfo>> ();

		buildCookResStart ();
	}

	public static void buildCookResStart()
	{
		downloadMgr.Instance.downloadSprite (string.Format (smallImgPath, 1), downloadCallBack, new object[] {DataMgr.FilterType.COOK,"S",1,smallImgPath});
		downloadMgr.Instance.downloadSprite (string.Format (bigImgPath, 1), downloadCallBack, new object[] {DataMgr.FilterType.COOK,"L",1,bigImgPath});
	}

	private static void downloadCallBack(Sprite sprite, object userData)
	{
		if (sprite == null) {return;}

		object[] args = (object[])userData;
		DataMgr.FilterType type = (DataMgr.FilterType) args[0];
		string size = (string)args [1];
		int index= (int)args [2];
		string path = (string)args [3];

		if (!filterMap.ContainsKey (type)) 
		{
			filterMap.Add (type, new Dictionary<int, filterInfo> ());
		}

		if (!filterMap [type].ContainsKey (index)) 
		{
			filterMap [type].Add (index, new filterInfo());
		}

		if (size == "S") 
		{
			filterMap [type] [index].sprite_S = sprite;
		}
		else if (size == "L") 
		{
			filterMap [type] [index].sprite_L = sprite;
		}

		index++;
		string downloadPath = string.Format (path, index);
		// Debug.logger.Log (string.Format("download path : {0}",downloadPath));
		downloadMgr.Instance.downloadSprite (downloadPath, downloadCallBack, new object[] {DataMgr.FilterType.COOK,size,index,path});
	}

	public static Dictionary<DataMgr.FilterType,Dictionary<int, filterInfo>> getFilterMap ()
	{
		return filterMap;
	}
}