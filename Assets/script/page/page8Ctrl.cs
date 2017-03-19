using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page8Ctrl : pageBase 
{
	[SerializeField]
	private GameObject mainMenu;
	[SerializeField]
	private GameObject[] assist;
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
		UIMgr.Instance.setBackground (UIMgr.BG.F);

		setNextBtnActive(true);
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (9);
	}
}
