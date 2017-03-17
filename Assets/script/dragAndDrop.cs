using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class dragAndDrop : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
	[SerializeField]
	private GameObject targetArea;

	private Vector3 oriPos = Vector3.zero;
	private Vector3 touchStart = Vector3.zero;

	public void Awake()
	{
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector3 offset =  getCurMousePosition() - touchStart;

		transform.position = (oriPos+offset);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		oriPos = transform.position;
		transform.localScale=new Vector3(0.7f,0.7f,0.7f);

		touchStart = getCurMousePosition();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (targetArea != null) 
		{
			RectTransform rect = GetComponent<RectTransform> ();
			RectTransform tarRect = targetArea.GetComponent<RectTransform> ();

			GameObject refPoint = new GameObject ();
			refPoint.name = "refPoint";
			refPoint.transform.localScale = Vector3.one;
			refPoint.transform.position = transform.position;
			refPoint.transform.SetParent (targetArea.transform.parent);

			Vector3 pos = refPoint.transform.localPosition;
			Vector3 tarPos = targetArea.transform.localPosition;

			Destroy (refPoint);

			bool checkX = Math.Abs (pos.x - tarPos.x) < (rect.rect.width / 2 + tarRect.rect.width / 2);
			bool checkY = Math.Abs (pos.y - tarPos.y) < (rect.rect.height / 2 + tarRect.rect.height / 2);
			// Debug.logger.Log (string.Format("Abs ({0} - {1}) < ({2} / 2 + {3} / 2)",pos.x,tarPos.x,rect.rect.width,tarRect.rect.width));
			// Debug.logger.Log (string.Format("Abs ({0} - {1}) < ({2} / 2 + {3} / 2)",pos.y,tarPos.y,rect.rect.height,tarRect.rect.height));

			if (checkX && checkY) 
			{
				// Debug.logger.Log ("collision");
				playEffect ();
			}
			else 
			{
				// Debug.logger.Log ("no collision");
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
		LeanTween.move (this.gameObject, targetArea.transform.position, 0.2f);
		LeanTween.scale(this.gameObject, new Vector3(0, 0, 0), 0.3f).setOnComplete(resume);
	}

	private void resume()
	{
		transform.localScale=new Vector3(1f,1f,1f);
		transform.position = oriPos;
	}

	private Vector3 getCurMousePosition()
	{
		float distance = UIMgr.Instance.getplaneDistance ();
		return Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance) );
	}
}
