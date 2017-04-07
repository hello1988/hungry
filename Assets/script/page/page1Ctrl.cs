using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page1Ctrl : pageBase 
{
	// private Button checkButton;
	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);
		homeVisible = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.A);
		setNextBtnActive(false);
		StartCoroutine (showNextBtn());
	}

	public IEnumerator showNextBtn()
	{
		yield return new WaitForSeconds (1);
		setNextBtnActive(true);
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (2);
	}
}
