using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class page8Ctrl : pageBase
{

	void Awake () 
	{
		// Button checkButton = nextBtn.GetComponent<Button> ();
		// checkButton.onClick.AddListener (nextPage);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public override void onPageEnable()
	{

		setNextBtnActive (true);
	}


	public void nextPage()
	{
		pageMgr.Instance.nextPage (9);
	}

}
