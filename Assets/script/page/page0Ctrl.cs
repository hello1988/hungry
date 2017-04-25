using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page0Ctrl : pageBase 
{
	[SerializeField]
	private InputField inputText;
	[SerializeField]
	private Image tipImg;
	[SerializeField]
	private Sprite[] tipList;

	private string[] collectList;

	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);
		homeVisible = false;

		collectList = new string[tipList.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.A);
		inputText.text = "";

		initCollectList ();
		setTipImage( 0 );
		setNextBtnActive(false);
	}

	public void onEndEdit()
	{
		for( int index = 0;index < collectList.Length;index++ )
		{
			if( !string.IsNullOrEmpty( collectList [index] ))
			{
				continue;
			}

			string sVal = inputText.text;
			// 特例 顧客人數特別處理
			if (index == 2) 
			{
				int iVal;
				if (!int.TryParse (inputText.text, out iVal)) 
				{
					sVal = "";
					break;
				}

				if (iVal > 7) 
				{
					sVal = "7";
					inputText.text = "7";
				}
			}

			collectList [index] = sVal;
			inputText.contentType = (index < 2) ? InputField.ContentType.Alphanumeric : InputField.ContentType.IntegerNumber;
			setTipImage( index + 1 );
			break;
		}

		if (isNextBtnShow ()) 
		{
			setNextBtnActive (true);
		}
		else 
		{
			inputText.text = "";
		}
	}

	public void nextPage()
	{
		DataMgr.Instance.setStaffNumber(collectList [0]);
		DataMgr.Instance.setTableNumber(collectList [1]);
		DataMgr.Instance.setCustomNum(int.Parse(collectList [2]));

		pageMgr.Instance.nextPage (1);
	}

	private void initCollectList()
	{
		for( int index = 0;index < collectList.Length;index++ )
		{
			collectList [index] = "";
		}
	}

	private bool isNextBtnShow()
	{
		for( int index = 0;index < collectList.Length;index++ )
		{
			if( string.IsNullOrEmpty( collectList [index] ))
			{
				return false;
			}
		}

		return true;
	}

	private void setTipImage( int index )
	{
		if ((index < 0) || (index >= tipList.Length)) {return;}

		tipImg.sprite = tipList [index];
	}
}
