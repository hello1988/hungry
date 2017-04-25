using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Const;

public class checkFilter : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler, IBeginDragHandler, IEndDragHandler
{
	[SerializeField]
	private Image filterImg;
	[SerializeField]
	private page4Ctrl pageCtrl;
	[SerializeField]
	private ScrollRect scrollRectParent;
	[SerializeField]
	private int dragDistance = 190;

	private Vector3 imgOriPos = Vector3.zero;
	private Vector3 startPos = Vector3.zero;
	private GameObject touchPoint;

	private FilterType filterType;
	private int filterIndex;

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {}


	public void OnPointerDown(PointerEventData eventData)
	{
		GameObject tPoint = getTouchPoint ();
		tPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		startPos = tPoint.transform.localPosition;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		GameObject tPoint = getTouchPoint ();
		tPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		Vector3 endPos = tPoint.transform.localPosition;

		float moveDistance = Math.Abs( endPos.x - startPos.x );
		float offsetX = 0;
		if (moveDistance >= (dragDistance / 2)) 
		{
			offsetX = -dragDistance;
		}

		filterImg.transform.localPosition = new Vector3( imgOriPos.x+offsetX, imgOriPos.y, imgOriPos.z ); 
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		// 把事件也轉給 ScrollRect
		scrollRectParent.OnBeginDrag (eventData);
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		// 把事件也轉給 ScrollRect
		scrollRectParent.OnEndDrag (eventData);
	}

	public void OnDrag (PointerEventData eventData)
	{
		// 把事件也轉給 ScrollRect
		scrollRectParent.OnDrag (eventData);

		GameObject tPoint = getTouchPoint ();
		tPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		Vector3 pos = tPoint.transform.localPosition;
		float offsetX = pos.x - startPos.x;

		float posX = Math.Max ( Math.Min(imgOriPos.x+offsetX, imgOriPos.x), imgOriPos.x-dragDistance);
		filterImg.transform.localPosition = new Vector3( posX, imgOriPos.y, imgOriPos.z ); 

	}

	public void setInfo( FilterType type, int index, Sprite sprite )
	{
		filterType = type;
		filterIndex = index;
		filterImg.sprite = sprite;

		imgOriPos = filterImg.transform.localPosition;
	}

	public void deleteFilter()
	{
		pageCtrl.deleteFilter (gameObject);
	}

	public FilterType getFilterType()
	{
		return filterType;
	}

	public int getFilterIndex()
	{
		return filterIndex;
	}

	private GameObject getTouchPoint()
	{
		if (touchPoint != null) 
		{
			return touchPoint;
		}

		Transform trans = transform.parent.Find ("touchPoint");
		touchPoint = (trans != null) ? trans.gameObject : null;
		if (touchPoint != null) 
		{
			return touchPoint;
		}

		touchPoint = new GameObject ("touchPoint");
		touchPoint.transform.SetParent (transform.parent);
		return touchPoint;
	}
}
