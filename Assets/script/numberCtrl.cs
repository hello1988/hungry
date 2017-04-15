using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberCtrl : MonoBehaviour 
{
	[SerializeField]
	private Sprite[] spriteList;	// 請依0-9排列

	[SerializeField]
	private Image digit;	// 個位數(必填)
	[SerializeField]
	private Image ten;		// 十位數
	[SerializeField]
	private Image hundry;	// 百位數
	[SerializeField]
	private Image thousand;// 千位數

	private List<Image> numberList;
	private int maxNumber = 0;

	// Use this for initialization
	void Awake () 
	{
		numberList = new List<Image> ();
		numberList.Add (digit);
		maxNumber = 9;

		if (ten != null) 
		{
			numberList.Add (ten);
			maxNumber = 99;
		}

		if (hundry != null) 
		{
			numberList.Add (hundry);
			maxNumber = 999;
		}

		if (thousand != null) 
		{
			numberList.Add (thousand);
			maxNumber = 9999;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setValue( int value )
	{
		int[] valueList = new int[numberList.Count];
		int showValue = Math.Max (0, value);
		showValue = Math.Min (maxNumber, showValue);
		for( int index = 0;index < numberList.Count;index++ )
		{
			int num = showValue % 10;
			numberList [index].gameObject.SetActive (true);
			numberList [index].sprite = spriteList [num];
			showValue /= 10;

			valueList [index] = num;
		}

		for( int index = numberList.Count-1;index >= 1 ;index-- )
		{
			if (valueList [index] > 0) {break;}

			numberList [index].gameObject.SetActive (false);
		}
	}
}
