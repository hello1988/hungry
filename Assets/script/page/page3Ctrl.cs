using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page3Ctrl : pageBase 
{
	[SerializeField]
	private GameObject loadImg;
	[SerializeField]
	private GameObject searchUI; // 從iPhone收集到的資料
	[SerializeField]
	private GameObject confirmScroll; // 已確認的顧客
	[SerializeField]
	private GameObject addCustomUI; // 臨時顧客UI
	[SerializeField]
	private Text newCustomName;	// 新顧客的名字
	[SerializeField]
	private GameObject tipText;	// 提示文字

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
		UIMgr.Instance.setBackground (UIMgr.BG.C);
		loadImg.SetActive (true);
		setNextBtnActive(false);
		searchUI.SetActive (false);
		confirmScroll.SetActive (false);
		addCustomUI.SetActive (false);
		tipText.SetActive (false);

		StartCoroutine (showCustom());
	}

	public IEnumerator showCustom()
	{
		yield return new WaitForSeconds (1);
		loadImg.SetActive (false);

		bool isShow = (DataMgr.Instance.getConfirmCustomList().Count > 0);
		setNextBtnActive(isShow);
		searchUI.SetActive (true);
		tipText.SetActive (true);
		searchUI.GetComponent<searchCustomCtrl>().refreshSearchList();

		confirmScroll.SetActive (true);
	}

	public void addCustomToConfirm( custom cus )
	{
		scrollCtrl ctrl = confirmScroll.GetComponentInChildren<scrollCtrl> ();
		GameObject newItem = ctrl.addItem ();
		newItem.GetComponent<Image> ().sprite = cus.cusPhoto;
		newItem.transform.localScale = Vector3.zero;
		LeanTween.scale (newItem, Vector3.one, 0.3f);

		setNextBtnActive(true);
	}

	public void onEndEdit()
	{
		if (newCustomName == null) {return;}

		string cusName = newCustomName.text;
		if( string.IsNullOrEmpty(cusName) ){return;}

		setNextBtnActive(true);
	}

	public void openAddCustomUI()
	{
		addCustomUI.SetActive (true);
		setNextBtnActive(false);
	}

	public void nextPage()
	{
		// 如果臨時顧客的介面是開著的 就關掉吧
		if (addCustomUI.activeInHierarchy) 
		{
			addNewCustom ();
			addCustomUI.SetActive (false);
		}
		else 
		{
			pageMgr.Instance.nextPage (4);
		}
	}

	private void addNewCustom()
	{
		custom newCustom = custom.createDefaultCustom(newCustomName.text);
		InputField input = addCustomUI.GetComponentInChildren<InputField> ();
		input.text = "";
		// 新客戶 加到確認名單
		addCustomToConfirm( newCustom );
		DataMgr.Instance.addCustomToConfirm (newCustom);
	}
}
