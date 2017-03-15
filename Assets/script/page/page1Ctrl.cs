using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page1Ctrl : pageBase 
{
	[SerializeField]
	private Text tableTxt;

	// private Button checkButton;
	void Awake () 
	{
		Button checkButton = checkBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void onPageEnable()
	{
		setBackground (UIMgr.BG.A);
		bool isShowCheck = !string.IsNullOrEmpty (DataMgr.Instance.getTableNumber ());

		setCheckBtnActive(isShowCheck);
	}

	public void onEndEdit()
	{
		if (tableTxt == null) {return;}

		DataMgr.Instance.setTableNumber(tableTxt.text);
		bool isShowCheck = !string.IsNullOrEmpty (tableTxt.text);
		setCheckBtnActive(isShowCheck);
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (2);
	}
}
