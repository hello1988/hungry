using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkOrderCtrl : MonoBehaviour 
{
	[SerializeField]
	private Image orderFloor;
	[SerializeField]
	private Image orderStaple;
	[SerializeField]
	private Image orderNumber;
	[SerializeField]
	private Image customPhoto;
	[SerializeField]
	private Sprite[] numberList;

	private Vector3 oriPos;
	// Use this for initialization
	void Awake () 
	{
		oriPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setInfo( orderData data )
	{
		orderFloor.sprite = data.orderFloor;
		orderStaple.sprite = data.stapleSprite;
		customPhoto.sprite = data.customPhoto;

		int num = Math.Min(Math.Max (data.orderNum, 1),9);
		orderNumber.sprite = numberList [num - 1];

	}

	public void showPreOrder()
	{
		LeanTween.moveLocalY (gameObject, oriPos.y+1500, 0.3f).setOnComplete(showPreOrderStep2);
	}

	public void showPreOrderStep2()
	{
		transform.localPosition = new Vector3 ( oriPos.x, oriPos.y-1500, oriPos.z );
		LeanTween.moveLocalY (gameObject, oriPos.y, 0.3f);
	}

	public void showNextOrder()
	{
		LeanTween.moveLocalY (gameObject, oriPos.y-1500, 0.3f).setOnComplete(showNextOrderStep2);
	}

	public void showNextOrderStep2 ()
	{
		transform.localPosition = new Vector3 ( oriPos.x, oriPos.y+1500, oriPos.z );
		LeanTween.moveLocalY (gameObject, oriPos.y, 0.3f);
	}
}
