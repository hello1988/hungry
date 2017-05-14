using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class custom 
{
	private static Dictionary<string,Sprite> defaultSprite;	// 預設圖片
	private static int customIndex = 1;	// 顧客流水號
	private static stapleComparer comparer = new stapleComparer();

	public Sprite cusPhoto;	// 顧客圖
	public String cusName;	// 顧客名字
	public readonly int customID;	// 顧客流水號
	public int budget = 300;

	private Dictionary<FilterType, List<int>> preferFilter;	// 顧客偏好過濾器
	private List<menu> preferMenu;	// 顧客偏好菜單
	private int viewingIndex;	// 正在看的菜單
	private Dictionary<int,int> confirmMenu;	// 已選取的菜單

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
		preferFilter = new Dictionary<FilterType, List<int>>();
		foreach( FilterType type in Enum.GetValues(typeof(FilterType)))
		{
			preferFilter.Add(type,new List<int>());
		}

		preferMenu = new List<menu>();
		confirmMenu = new Dictionary<int,int> ();

		// Test
		for( int index = 1;index <= 30;index++ )
		{
			menu m = menuMgr.Instance.getMenuByID (index);
			if (m == null) {continue;}

			preferMenu.Add (m);
		}

		viewingIndex = 0;

		if (defaultSprite != null) {return;}

		defaultSprite = new Dictionary<string, Sprite> ();

		downloadMgr.Instance.downloadSprite ("custom/photo1",custom.loadDefaultSprite,"photo");
		// downloadMgr.Instance.downloadSprite ("custom/name1",custom.loadDefaultSprite,"name");
	}

	public void addPreferFilter( FilterType type, int filtlerIndex )
	{
		if (preferFilter [type].Contains (filtlerIndex)) {return;}

		preferFilter [type].Add (filtlerIndex);
	}

	public void removePreferFilter( FilterType type, int filtlerIndex )
	{
		if (!preferFilter.ContainsKey (type)) {return;}

		if (!preferFilter [type].Contains (filtlerIndex)) {return;}

		preferFilter [type].Remove(filtlerIndex);
	}

	public Dictionary<FilterType, List<int>> getPreferFilter()
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

	public Dictionary<int,int> getConfirmMenu()
	{
		return confirmMenu;
	}

	public List<int> getConfirmMenuIDList()
	{
		List<int> menuIDList = new List<int> (confirmMenu.Keys);
		menuIDList.Sort (comparer);
		return menuIDList;
	}

	public void modifyConfirmMenu( int menuID, int num )
	{
		if( !confirmMenu.ContainsKey(menuID) )
		{
			confirmMenu.Add (menuID, 0);
		}

		confirmMenu [menuID] = Math.Max( (confirmMenu [menuID]+num), 1 );
	}

	public void deleteConfirmMenu( int menuID )
	{
		confirmMenu.Remove (menuID);
	}
}

public class stapleComparer: IComparer<int>
{
	public int Compare(int menuID1, int menuID2)
	{
		menu m1 = menuMgr.Instance.getMenuByID (menuID1);
		menu m2 = menuMgr.Instance.getMenuByID (menuID2);
		// 小->大
		return ( (int)m1.getUseStaple () - (int)m2.getUseStaple () );
	}
}