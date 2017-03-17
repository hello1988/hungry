using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page6Ctrl : pageBase 
{
	// private Button checkButton;
	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.C);
		setNextBtnActive(true);
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (7);
	}
}
