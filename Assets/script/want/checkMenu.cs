using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class checkMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	[SerializeField]
	private slideOrder slideOrderDetect;
	[SerializeField]
	private Image menuImg;
	[SerializeField]
	private GameObject deleteBtn;
	[SerializeField]
	private int dragDistance = 245;

	private Vector3 imgOriPos = Vector3.zero;
	private Vector3 delOriPos = Vector3.zero;
	private Vector3 startPos = Vector3.zero;

	private GameObject touchPoint;
	void Awake () 
	{
		imgOriPos = menuImg.transform.localPosition;
		delOriPos = deleteBtn.transform.localPosition;

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

		slideOrderDetect.OnPointerDown (eventData);
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		Vector3 endPos = touchPoint.transform.localPosition;

		float moveDistance = endPos.x - startPos.x;
		float offsetX = 0;
		if (moveDistance <= -(dragDistance / 2)) 
		{
			offsetX = -dragDistance;
		}

		menuImg.transform.localPosition = new Vector3( imgOriPos.x+offsetX, imgOriPos.y, imgOriPos.z ); 
		deleteBtn.transform.localPosition = new Vector3( delOriPos.x+offsetX, delOriPos.y, delOriPos.z ); 

		slideOrderDetect.OnPointerUp (eventData);
	}

	public void OnDrag (PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		Vector3 pos = touchPoint.transform.localPosition;

		float offsetX = pos.x - startPos.x;

		float posX = Math.Max ( Math.Min(imgOriPos.x+offsetX, imgOriPos.x), imgOriPos.x-dragDistance);
		menuImg.transform.localPosition = new Vector3( posX, imgOriPos.y, imgOriPos.z ); 

		posX = Math.Max ( Math.Min(delOriPos.x+offsetX, delOriPos.x), delOriPos.x-dragDistance);
		deleteBtn.transform.localPosition = new Vector3( posX, delOriPos.y, delOriPos.z ); 
	}

	public void setMenuImage ( Sprite sprite )
	{
		menuImg.sprite = sprite;

		menuImg.transform.localPosition = imgOriPos;
		deleteBtn.transform.localPosition = delOriPos;
	}

}
