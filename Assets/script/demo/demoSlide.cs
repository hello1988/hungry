using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class demoSlide : MonoBehaviour
{
	[SerializeField]
	private Image numberImg;
	[SerializeField]
	private Sprite[] numberList;
	[SerializeField]
	private slidAndClick orderNumCtrl;

	private int numberIndex;

	void Awake () 
	{
		orderNumCtrl.setCallBack (slidAndClick.Direction.LEFT, minusOrderNum);
		orderNumCtrl.setCallBack (slidAndClick.Direction.RIGHT, addOrderNum);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		numberIndex = 0;
	}

	public void addOrderNum()
	{
		numberIndex = Math.Min ((numberIndex + 1), (numberList.Length-1));
		updateNumberImg ();
	}

	public void minusOrderNum()
	{
		numberIndex = Math.Max ((numberIndex - 1), 0);
		updateNumberImg ();
	}

	private void updateNumberImg()
	{
		numberImg.sprite = numberList [numberIndex];
	}
}
