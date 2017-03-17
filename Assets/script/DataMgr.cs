using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoBehaviour 
{
	// 店員編號
	private string staffNumber = "";
	// 桌號
	private string tableNumber = "";
	// 搜尋到的顧客
	private List<custom> searchCustomList = new List<custom>();
	// 要點餐的顧客
	private List<custom> confirmCustomList = new List<custom>();

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
