using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class checkFilter : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
	[SerializeField]
	private Image filterImg;
	[SerializeField]
	private GameObject deleteBtn;
	[SerializeField]
	private page6Ctrl pageCtrl;

	Vector3 imgOriPos = Vector3.zero;
	Vector3 delOriPos = Vector3.zero;
	private bool pressing = false;
	private DateTime pressTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!pressing) {return;}

		if ((System.DateTime.Now - pressTime).TotalMilliseconds < 1500) {return;}
		longPress ();
		pressing = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		pressing = true;
		pressTime = DateTime.Now;

		imgOriPos = filterImg.transform.localPosition;
		delOriPos = deleteBtn.transform.localPosition;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		pressing = false;
	}

	public void setImage( Sprite sprite )
	{
		filterImg.sprite = sprite;
	}

	private void longPress()
	{
		LeanTween.moveLocal (filterImg.gameObject, new Vector3((imgOriPos.x-190),imgOriPos.y,imgOriPos.z+20),0.3f);
		LeanTween.moveLocal (deleteBtn, new Vector3((delOriPos.x-190),delOriPos.y,delOriPos.z+20),0.3f);
	}
}
