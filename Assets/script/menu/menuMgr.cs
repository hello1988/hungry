﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		Dictionary<DataMgr.FilterType, List<int>> filter = cus.getPreferFilter ();

		foreach( menu m in menuList )
		{
			if (!filter [DataMgr.FilterType.COOK].Contains ((int)m.getCookWay())) {continue;}
			if (!filter [DataMgr.FilterType.FOOD].Contains ((int)m.getUseFood())) {continue;}
			if (!filter [DataMgr.FilterType.STAPLE].Contains ((int)m.getUseStaple())) {continue;}

			preferMenu.Add (m);
		}

		cus.setPreferMenu( preferMenu );
	}

	private void buildMenuMap()
	{
		menuList = new List<menu> ();

		// test
		menuList.Add(new menu(1,"menu1",100,DataMgr.Cook.BROIL,DataMgr.Food.BEEF,DataMgr.Staple.NOODLE));
	}

}