using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoBehaviour 
{
	// 店員編號
	private string staffNumber = "";
	// 桌號
	private string tableNumber = "";

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
}
