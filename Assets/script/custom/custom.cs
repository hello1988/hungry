using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class custom 
{
	private static Dictionary<string,Sprite> defaultSprite;	// 預設圖片
	private static int customIndex = 1;	// 顧客流水號

	public Sprite cusPhoto;	// 顧客圖
	public String cusName;	// 顧客名字
	public readonly int customID;	// 顧客流水號
	public int budget = 300;

	private Dictionary<DataMgr.FilterType, List<int>> preferFilter;	// 顧客偏好過濾器
	private List<menu> preferMenu;	// 顧客偏好菜單
	private int viewingIndex;	// 正在看的菜單

	public static void loadDefaultSprite(Sprite sprite, object userData)
	{
		string spriteName = (string)userData;
		defaultSprite.Add (spriteName, sprite);
	}

	public static custom createDefaultCustom( string customName )
	{
		custom cus = new custom ();
		cus.cusPhoto = defaultSprite ["photo"];
		cus.cusName = string.Format("新顧客{0}",cus.customID);
		return cus;
	}

	public custom()
	{
		customID = customIndex++;
		preferFilter = new Dictionary<DataMgr.FilterType, List<int>>();
		foreach( DataMgr.FilterType type in Enum.GetValues(typeof(DataMgr.FilterType)))
		{
			preferFilter.Add(type,new List<int>());
		}

		preferMenu = new List<menu>();

		// Test
		preferMenu.Add( menuMgr.Instance.getMenuByID(3) );
		preferMenu.Add( menuMgr.Instance.getMenuByID(2) );
		preferMenu.Add( menuMgr.Instance.getMenuByID(1) );
		viewingIndex = 0;

		if (defaultSprite != null) {return;}

		defaultSprite = new Dictionary<string, Sprite> ();

		downloadMgr.Instance.downloadSprite ("custom/photo1",custom.loadDefaultSprite,"photo");
		// downloadMgr.Instance.downloadSprite ("custom/name1",custom.loadDefaultSprite,"name");
	}

	public void addPreferFilter( DataMgr.FilterType type, int filtlerIndex )
	{
		if (preferFilter [type].Contains (filtlerIndex)) {return;}

		preferFilter [type].Add (filtlerIndex);
	}

	public void removePreferFilter( DataMgr.FilterType type, int filtlerIndex )
	{
		if (!preferFilter.ContainsKey (type)) {return;}

		if (!preferFilter [type].Contains (filtlerIndex)) {return;}

		preferFilter [type].Remove(filtlerIndex);
	}

	public Dictionary<DataMgr.FilterType, List<int>> getPreferFilter()
	{
		return preferFilter;
	}

	public void setPreferMenu(  List<menu> menuList )
	{
		preferMenu = menuList;
	}

	public List<menu> getPreferMenu()
	{
		return preferMenu;
	}

	/**
	* offset 
	* = 0 表示目前正在看的菜單
	* > 0 表示下一道菜
	* < 0 表示上一道菜
	*/
	public menu getMenu( int offset )
	{
		if (offset > 0) { viewingIndex = (viewingIndex + 1) % preferMenu.Count; }
		else if (offset < 0) { viewingIndex = (viewingIndex + preferMenu.Count - 1) % preferMenu.Count; }

		return preferMenu [viewingIndex];
	}
}
