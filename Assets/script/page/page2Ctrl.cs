using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page2Ctrl : pageBase 
{
	[SerializeField]
	private GameObject loadImg;
	[SerializeField]
	private GameObject tipText;	// 提示文字
	[SerializeField]
	private alternateCustom[] customList;

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
		setNextBtnActive(false);
		loadImg.SetActive (true);
		tipText.SetActive (false);

		for( int index = 0;index < customList.Length;index++ )
		{
			customList [index].gameObject.SetActive (false);
		}

		StartCoroutine (showCustom());
	}

	public IEnumerator showCustom()
	{
		yield return new WaitForSeconds (2);
		loadImg.SetActive (false);

		setNextBtnActive(true);
		tipText.SetActive (true);

		initAlternateCustom ();
	}

	public void nextPage()
	{
		List<custom> confirmList = getConfirmList ();
		if (confirmList.Count <= 0) {return ;}

		// 設定點餐顧客 並把第一個設為點餐中
		DataMgr.Instance.setConfirmCustomList (confirmList);
		DataMgr.Instance.setOrderingCustom (confirmList[0]);

		pageMgr.Instance.nextPage (3);
	}

	private void initAlternateCustom()
	{
		List<custom> searchList = DataMgr.Instance.getSearchCustomList ();
		List<custom> confirmList = DataMgr.Instance.getConfirmCustomList ();
		int size = Math.Min (customList.Length, searchList.Count);

		for( int index = 0;index < size;index++ )
		{
			customList [index].gameObject.SetActive (true);
			customList [index].init (searchList [index]);

			// 如果已經在確認名單的 設定為以選取
			for (int confirmIndex = 0; confirmIndex < confirmList.Count; confirmIndex++) 
			{
				if (confirmList [confirmIndex].customID == searchList [index].customID) 
				{
					customList [index].setSelected (true);
					break;
				}
			}
		}
	}

	private List<custom> getConfirmList()
	{
		List<custom> result = new List<custom> ();
		for( int index = 0;index < customList.Length;index++ )
		{
			if (!customList [index].gameObject.activeInHierarchy) {break;}

			if (!customList [index].getSelected ()) {continue;}

			result.Add (customList [index].getCustom ());
		}

		return result;
	}
}
