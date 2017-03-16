using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class custom 
{
	public Sprite cusPhoto;
	public Sprite cusName;

	// public delegate void dSprite(Sprite sprite, object userData = null);
	private static Dictionary<string,Sprite> defaultSprite;

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
		if (defaultSprite != null) {return;}

		defaultSprite = new Dictionary<string, Sprite> ();

		downloadMgr.Instance.downloadSprite ("custom/photo1",custom.loadDefaultSprite,"photo");
		downloadMgr.Instance.downloadSprite ("custom/name1",custom.loadDefaultSprite,"name");
	}
}
