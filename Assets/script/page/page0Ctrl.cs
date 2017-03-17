using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page0Ctrl : pageBase 
{
	[SerializeField]
	private Text staffTxt;

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
		bool isShowCheck = !string.IsNullOrEmpty (DataMgr.Instance.getStaffNumber ());

		setNextBtnActive(isShowCheck);
	}

	public void onEndEdit()
	{
		if (staffTxt == null) {return;}

		DataMgr.Instance.setStaffNumber(staffTxt.text);
		bool isShowCheck = !string.IsNullOrEmpty (staffTxt.text);
		setNextBtnActive(isShowCheck);
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (1);
	}
}
