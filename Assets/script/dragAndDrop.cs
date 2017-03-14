using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class dragAndDrop : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
	private Vector3 oriPos = Vector3.zero;
	[SerializeField]
	private GameObject targetArea;
	public void Awake()
	{
		oriPos = transform.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		GetComponent<RectTransform>().pivot.Set(0,0);

		Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		transform.position = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPosition.z));

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		transform.localScale=new Vector3(0.7f,0.7f,0.7f);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (targetArea != null) 
		{
			RectTransform rect = GetComponent<RectTransform> ();
			RectTransform tarRect = targetArea.GetComponent<RectTransform> ();
			Vector3 pos = this.gameObject.transform.localPosition;
			Vector3 tarPos = targetArea.transform.localPosition;


			bool checkX = Math.Abs (pos.x - tarPos.x) < (rect.rect.width / 2 + tarRect.rect.width / 2);
			bool checkY = Math.Abs (pos.y - tarPos.y) < (rect.rect.height / 2 + tarRect.rect.height / 2);
			// Debug.logger.Log (string.Format("Abs ({0} - {1}) < ({2} / 2 + {3} / 2)",pos.x,tarPos.x,rect.rect.width,tarRect.rect.width));
			// Debug.logger.Log (string.Format("Abs ({0} - {1}) < ({2} / 2 + {3} / 2)",pos.y,tarPos.y,rect.rect.height,tarRect.rect.height));

			if (checkX && checkY) 
			{
				Debug.logger.Log ("collision");
				playEffect ();
			}
			else 
			{
				Debug.logger.Log ("no collision");
				resume ();
			}
		}
		else 
		{
			resume ();
		}
	}

	private void playEffect()
	{
		LeanTween.scale(this.gameObject, new Vector3(0, 0, 0), 2f).setOnComplete(resume);
	}

	private void resume()
	{
		transform.localScale=new Vector3(1f,1f,1f);
		transform.position = oriPos;
	}
}
