using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pointDrag : MonoBehaviour,IDragHandler
{
	[SerializeField]
	private sauseCtrl sause;
	[SerializeField]
	private GameObject anchorLeft;
	[SerializeField]
	private GameObject anchorRight;
	[SerializeField]
	private GameObject touchPoint;

	private Vector3 oriPos;
	private float left;
	private float right;
	private float maxWidth;
	void Awake () 
	{
		oriPos = transform.localPosition;
		left = anchorLeft.transform.localPosition.x;
		right = anchorRight.transform.localPosition.x;
		maxWidth = right - left;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrag(PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition();
		float posX = touchPoint.transform.localPosition.x;
		posX = Math.Min ( Math.Max( posX, left ), right );
		transform.localPosition = new Vector3(posX, oriPos.y, oriPos.z );

		float progress = (posX - left)/maxWidth;
		sause.setProgress (progress);
	}

}
