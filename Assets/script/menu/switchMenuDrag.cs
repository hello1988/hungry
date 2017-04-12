using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 一個拖著parent移動的概念
public class switchMenuDrag : MonoBehaviour, IDragHandler,IPointerDownHandler, IPointerUpHandler
{
	private switchMenuCtrl dragedParent;
	private GameObject touchPoint;
	private Vector3 startPos;
	private Vector3 oriPos;
	private float left;
	private float right;

	void Awake () 
	{
		dragedParent = transform.parent.GetComponent<switchMenuCtrl>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setInfo( float leftBound, float rightBound )
	{
		left = leftBound;
		right = rightBound;
	}

	public void OnDrag (PointerEventData eventData)
	{
		GameObject tPoint = getTouchPoint ();
		tPoint.transform.position = UIMgr.Instance.getCurMousePosition ();

		float offsetX = tPoint.transform.localPosition.x - startPos.x;
		float posX = oriPos.x + offsetX;
		posX = Math.Max ( Math.Min( posX, right ), left );

		Vector3 pos = dragedParent.transform.localPosition;
		dragedParent.transform.localPosition = new Vector3 (posX, pos.y, pos.z);
		dragedParent.hideMask ();
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		GameObject tPoint = getTouchPoint ();
		tPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		startPos = tPoint.transform.localPosition;
		oriPos = dragedParent.transform.localPosition;

	}

	public void OnPointerUp (PointerEventData eventData)
	{
		Vector3 pos = dragedParent.transform.localPosition;
		float middle = (left + right) / 2;

		float distance = Math.Min ( Math.Abs( pos.x - left ), Math.Abs(pos.x - right) );
		float durSec = (distance / (middle - left)) * 0.15f;
		if (pos.x < middle) 
		{
			dragedParent.hideMenu (durSec);
		}
		else
		{
			dragedParent.showMenu (durSec);
		}
	}

	private GameObject getTouchPoint()
	{
		if (touchPoint == null) 
		{
			touchPoint = new GameObject ("touchPoint");
			touchPoint.transform.SetParent ( dragedParent.transform.parent );
		}

		return touchPoint;
	}
}
