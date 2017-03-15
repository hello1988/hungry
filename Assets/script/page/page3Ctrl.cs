using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page3Ctrl : pageBase 
{
	[SerializeField]
	private GameObject customScroll; // 從iPhone收集到的資料
	[SerializeField]
	private GameObject confirmScroll; // 已確認的顧客
	[SerializeField]
	private GameObject addCustomUI; // 臨時顧客UI

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
		setNextBtnActive(false);
		customScroll.SetActive (false);
		confirmScroll.SetActive (false);
		addCustomUI.SetActive (false);

		StartCoroutine (customList());
	}

	public IEnumerator customList()
	{
		yield return new WaitForSeconds (5);
		setNextBtnActive(true);
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (4);
	}
}
