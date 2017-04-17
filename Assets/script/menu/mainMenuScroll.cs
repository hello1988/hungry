using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mainMenuScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private page6Ctrl pageCtrl;
	[SerializeField]
	private int scrollDistance = 800;

	private GameObject touchPoint;
	private Vector3 startPos;

	void Awake () 
	{
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
		float offsetY = endPos.y - startPos.y;
		// Debug.logger.Log (string.Format("offsetY : {0}",offsetY));
		if (offsetY > scrollDistance)
		{
			pageCtrl.toPreMenu ();
		}
		else if (offsetY < -scrollDistance) 
		{
			pageCtrl.toNextMenu ();
		}
	}
}
