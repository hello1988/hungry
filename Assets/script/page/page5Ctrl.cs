﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page5Ctrl : pageBase 
{

	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);

	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.E);
		setNextBtnActive(true);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (6);
	}

}
