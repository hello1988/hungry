using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class checkOrderCtrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private page8Ctrl pageCtrl;
	[SerializeField]
	private Image orderFloor;
	[SerializeField]
	private Image orderNumber;
	[SerializeField]
	private Image customPhoto;
	[SerializeField]
	private Sprite[] numberList;

	private Vector3 oriPos;
	private orderData cacheData;
	private GameObject touchPoint;
	private Vector3 startPos;
	void Awake () 
	{
		oriPos = transform.localPosition;
		cacheData = null;

		touchPoint = new GameObject ("touchPoint");
		touchPoint.transform.SetParent (transform.parent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		startPos = touchPoint.transform.localPosition;
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		Vector3 endPos = touchPoint.transform.localPosition;

		Vector3 offset = endPos - startPos;
		if (offset.y > 100) 
		{
			pageCtrl.nextOrder ();
		}
		else if(offset.y < -100) 
		{
			pageCtrl.preOrder ();
		}
	}

	public void setInfo( orderData data )
	{
		orderFloor.sprite = data.orderFloor;
		customPhoto.sprite = data.customPhoto;

		int num = Math.Min(Math.Max (data.orderNum, 1),9);
		orderNumber.sprite = numberList [num - 1];

	}

	public void showPreOrder( orderData data )
	{
		cacheData = data;
		LeanTween.moveLocalY (gameObject, oriPos.y-1500, 0.3f).setOnComplete(showPreOrderStep2);
	}

	public void showPreOrderStep2()
	{
		if (cacheData != null) 
		{
			setInfo (cacheData);
			cacheData = null;
		}
		transform.localPosition = new Vector3 ( oriPos.x, oriPos.y+1500, oriPos.z );
		LeanTween.moveLocalY (gameObject, oriPos.y, 0.3f);
	}

	public void showNextOrder( orderData data )
	{
		cacheData = data;
		LeanTween.moveLocalY (gameObject, oriPos.y+1500, 0.3f).setOnComplete(showNextOrderStep2);
	}

	public void showNextOrderStep2 ()
	{
		if (cacheData != null) 
		{
			setInfo (cacheData);
			cacheData = null;
		}
		transform.localPosition = new Vector3 ( oriPos.x, oriPos.y-1500, oriPos.z );
		LeanTween.moveLocalY (gameObject, oriPos.y, 0.3f);
	}
}
