using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class slideOrder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private int slideDistance = 300;
	[SerializeField]
	private page7Ctrl pageCtrl;

	private GameObject touchPoint;
	private Vector3 startPos = Vector3.zero;

	void Awake () 
	{
		touchPoint = new GameObject ("touchPoint");
		touchPoint.transform.SetParent (transform.parent);

		touchPoint.transform.localPosition = Vector3.zero;
		touchPoint.transform.localScale = Vector3.one;
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

		float offsetY = (endPos - startPos).y;
		if (offsetY > slideDistance) 
		{
			pageCtrl.nextOrder ();
		}
		else if (offsetY < -slideDistance) 
		{
			pageCtrl.preOrder ();
		}
	}

}
