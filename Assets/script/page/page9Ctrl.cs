using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class page9Ctrl : pageBase
{
	[SerializeField]
	private float waitSec = 3;

	void Awake () 
	{
		homeVisible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.A);

		StartCoroutine (nextPage ());
	}


	public IEnumerator nextPage()
	{
		yield return new WaitForSeconds (waitSec);

		pageMgr.Instance.homePage ();
	}

}
