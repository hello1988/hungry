using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class confirmCustomDetect  : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IDragHandler
{
	[SerializeField]
	private page4Ctrl parentObj;

	private Vector3 startPos = Vector3.zero;
	private Vector3 oriLocalPos = Vector3.zero;
	private Vector3 oriPos = Vector3.zero;
	private bool pressing = false;

	// Use this for initialization
	void Awake () {
		oriLocalPos = transform.localPosition;
		oriPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!pressing) {return;}
		Vector3 curMousePosition = UIMgr.Instance.getCurMousePosition();

		// 總長約 53
		float xOffset = curMousePosition.x - startPos.x;
		if (xOffset > 20.0) 
		{
			transform.parent.GetComponent<page4Ctrl> ().preCustom ();
			pressing = false;
		}
		else if (xOffset < -20.0) 
		{
			transform.parent.GetComponent<page4Ctrl> ().nextCustom ();
			pressing = false;
		}
		else 
		{
			transform.position = new Vector3 ((oriPos.x+xOffset), oriPos.y, oriPos.z);
		}

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		startPos = UIMgr.Instance.getCurMousePosition ();
		pressing = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		pressing = false;
		Vector3 endPos = UIMgr.Instance.getCurMousePosition();
		// Debug.logger.Log (string.Format("xOffset : {0}",(endPos.x - startPos.x)));

		transform.localPosition = oriLocalPos;

		if(Mathf.Abs(endPos.x - startPos.x) < 1.0)
		{
			// Debug.logger.Log ("OnPointerClick");
			parentObj.onCustomClick();
		}
	}


	public void setImageByCustom( custom cus )
	{
		foreach (Image img in this.GetComponentsInChildren<Image>()) 
		{
			if (img.name == "photo") 
			{
				img.sprite = cus.cusPhoto;
			} 
			else if (img.name == "name") 
			{
				img.sprite = cus.cusName;
			}
		}
	}

	public void resume()
	{
		LeanTween.moveLocal(this.gameObject, oriLocalPos, 0.3f);
		LeanTween.scale (this.gameObject, Vector3.one, 0.3f);
	}
}
