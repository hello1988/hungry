using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class custom 
{
	public Sprite cusPhoto;
	public Sprite cusName;
	public readonly int customID;
	private Dictionary<DataMgr.FilterType, List<int>> preferFilter;

	// public delegate void dSprite(Sprite sprite, object userData = null);
	private static Dictionary<string,Sprite> defaultSprite;
	private static int customIndex = 1;

	public static void loadDefaultSprite(Sprite sprite, object userData)
	{
		string spriteName = (string)userData;
		defaultSprite.Add (spriteName, sprite);
	}

	public static custom createDefaultCustom( string customName )
	{
		custom cus = new custom ();
		cus.cusPhoto = defaultSprite ["photo"];
		cus.cusName = defaultSprite ["name"];
		return cus;
	}

	public custom()
	{
		customID = customIndex++;
		preferFilter = new Dictionary<DataMgr.FilterType, List<int>>();

		if (defaultSprite != null) {return;}

		defaultSprite = new Dictionary<string, Sprite> ();

		downloadMgr.Instance.downloadSprite ("custom/photo1",custom.loadDefaultSprite,"photo");
		downloadMgr.Instance.downloadSprite ("custom/name1",custom.loadDefaultSprite,"name");
	}

	public void addPreferFilter( DataMgr.FilterType type, int filtlerIndex )
	{
		if (!preferFilter.ContainsKey (type)) 
		{
			preferFilter.Add (type, new List<int> ());
		}

		if (preferFilter [type].Contains (filtlerIndex)) {return;}

		preferFilter [type].Add (filtlerIndex);
	}

	public Dictionary<DataMgr.FilterType, List<int>> getPreferFilter()
	{
		return preferFilter;
	}
}
