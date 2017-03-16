using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class custom : MonoBehaviour 
{
	[SerializeField]
	private Image cusPhoto;
	[SerializeField]
	private Image cusFloor;
	[SerializeField]
	private Image cusName;

	// public delegate void dSprite(Sprite sprite, object userData = null);
	private static Dictionary<string,Sprite> defaultSprite;

	public void Awake()
	{
		if (defaultSprite != null) {return;}

		defaultSprite = new Dictionary<string, Sprite> ();

		downloadMgr.Instance.downloadSprite ("custom/defaultPhoto",loadDefaultSprite,"photo");
		downloadMgr.Instance.downloadSprite ("custom/defaultName",loadDefaultSprite,"name");
	}

	public void loadDefaultSprite(Sprite sprite, object userData)
	{
		string spriteName = (string)userData;
		defaultSprite.Add (spriteName, sprite);
	}

	public static custom createDefaultCustom( string customName )
	{
		return new custom ();
	}

	// 被點擊時 在顧客名單刪除自己 並加入到確認名單
	public void onCustomClick()
	{
		
	}
}
