﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class menuMgr : MonoBehaviour 
{
	List<menu> menuList;

	private static menuMgr _instance = null;
	public static menuMgr Instance
	{
		get{return _instance;}
	}

	// Use this for initialization
	private void Awake ()
	{
		_instance = this;
	}

	void Start()
	{
		buildMenuMap ();
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	// 篩選出符合顧客條件的菜單
	public void setPreferMenuToCustom( custom cus )
	{
		List<menu> preferMenu = new List<menu> ();
		Dictionary<FilterType, List<int>> filter = cus.getPreferFilter ();

		foreach( menu m in menuList )
		{
			if (!filter [FilterType.COOK].Contains ((int)m.getCookWay())) {continue;}

			preferMenu.Add (m);
		}

		cus.setPreferMenu( preferMenu );
	}

	public menu getMenuByID( int menuID )
	{
		for (int index = 0; index < menuList.Count; index++) 
		{
			if (menuList [index].getMenuID () != menuID) {continue;}

			return menuList [index];
		}

		return null;
	}

	public void menuToSpeech( string audioKey, string txt )
	{
		textToSpeech tts = GetComponent<textToSpeech> ();
		// StartCoroutine (tts.speech (audioKey, txt, "zh-tw"));

		tts.speech (audioKey);
	}

	private void buildMenuMap()
	{
		menuList = new List<menu> ();

		// Json 表單讀取流程
		// 除了雷 我沒有別的字可以形容Unity
		TextAsset menuStr = Resources.Load<TextAsset>("menu/menu");
		menuInfo jsonObj = JsonUtility.FromJson<menuInfo>(menuStr.text);
		foreach( menuStruct mStruct in jsonObj.data )
		{
			menuList.Add (mStruct.createMenu());
		}

//		textToSpeech tts = GetComponent<textToSpeech> ();
//		tts.onMenuReady (menuList);
	}

}
