using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page3Ctrl : pageBase 
{
	[SerializeField]
	private GameObject loadImg;
	[SerializeField]
	private GameObject customScroll; // 從iPhone收集到的資料
	[SerializeField]
	private GameObject confirmScroll; // 已確認的顧客
	[SerializeField]
	private GameObject addCustomUI; // 臨時顧客UI
	[SerializeField]
	private Text newCustomName;	// 新顧客的名字

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
		loadImg.SetActive (true);
		setNextBtnActive(false);
		customScroll.SetActive (false);
		// confirmScroll.SetActive (false);
		// addCustomUI.SetActive (false);

		StartCoroutine (customList());
	}

	public IEnumerator customList()
	{
		yield return new WaitForSeconds (1);
		loadImg.SetActive (false);

		// setNextBtnActive(true);
		customScroll.SetActive (true);
	}

	public void onEndEdit()
	{
		if (newCustomName == null) {return;}

		custom newCustom = custom.createDefaultCustom(newCustomName.text);
		// TODO 新客戶 加到確認名單

		setNextBtnActive(true);
	}

	public void nextPage()
	{
		// 如果臨時顧客的介面是開著的 就關掉吧
		if (addCustomUI.activeInHierarchy) 
		{
			addCustomUI.SetActive (false);
		}
		else 
		{
			pageMgr.Instance.nextPage (4);
		}
	}
}
