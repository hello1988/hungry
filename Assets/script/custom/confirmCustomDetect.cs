using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class confirmCustomDetect  : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
	private Vector3 startPos = Vector3.zero;
	private Vector3 oriPos = Vector3.zero;

	// Use this for initialization
	void Awake () {
		oriPos = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
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

		// Debug.logger.Log (string.Format("xOffset : {0}",(endPos.x - startPos.x)));

		// 總長約 3.6
		if ((endPos.x - startPos.x) > 0.7) 
		{
			this.transform.parent.GetComponent<page4Ctrl> ().preCustom ();
		}
		else if ((endPos.x - startPos.x) < -0.7) 
		{
			this.transform.parent.GetComponent<page4Ctrl> ().nextCustom ();
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
		LeanTween.moveLocal(this.gameObject, oriPos, 0.3f);
		LeanTween.scale (this.gameObject, Vector3.one, 0.3f);
	}
}
