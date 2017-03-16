using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class searchCustomListener : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
	private Vector3 oriPos = Vector3.zero;

	private Vector3 startPos = Vector3.zero;

	// Use this for initialization
	public void Awake()
	{
		oriPos = transform.position;
	}


	public void OnPointerDown(PointerEventData eventData)
	{
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		startPos = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPosition.z));
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 endPos = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPosition.z));

		// Debug.logger.Log (string.Format("yOffset : {0}",(endPos.y - startPos.y)));
		// 總長約 6.3
		if ((endPos.y - startPos.y) > 0.7) 
		{
			this.transform.parent.GetComponent<searchCustomCtrl> ().showNextCustom ();
		}
		else
		{
			this.transform.parent.GetComponent<searchCustomCtrl> ().confirmCunstom();
		}
	}

	public void resume()
	{
		transform.position = new Vector3(oriPos.x, oriPos.y-100, oriPos.z );
		LeanTween.move (this.gameObject, oriPos, 0.2f);
		LeanTween.scale (this.gameObject, Vector3.one, 0.3f );
	}
}
