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

	private Vector3 imgOriPos = Vector3.zero;
	private Vector3 delOriPos = Vector3.zero;
	private bool pressing = false;
	private DateTime pressTime;
	private DataMgr.FilterType filterType;
	private int filterIndex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!pressing) {return;}

		if ((System.DateTime.Now - pressTime).TotalMilliseconds < 500) {return;}
		longPress ();
		pressing = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		pressing = true;
		pressTime = DateTime.Now;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		pressing = false;
	}

	public void setInfo( DataMgr.FilterType type, int index, Sprite sprite )
	{
		filterType = type;
		filterIndex = index;
		filterImg.sprite = sprite;

		imgOriPos = filterImg.transform.localPosition;
		delOriPos = deleteBtn.transform.localPosition;
	}

	private void longPress()
	{
		LeanTween.moveLocal (filterImg.gameObject, new Vector3((imgOriPos.x-190),imgOriPos.y,imgOriPos.z+20),0.3f);
		LeanTween.moveLocal (deleteBtn, new Vector3((delOriPos.x-190),delOriPos.y,delOriPos.z+20),0.3f);

		StopCoroutine(resume ());
		StartCoroutine (resume ());
	}

	public IEnumerator resume()
	{
		yield return new WaitForSeconds (5);
		LeanTween.moveLocal (filterImg.gameObject,imgOriPos,0.3f);
		LeanTween.moveLocal (deleteBtn, delOriPos,0.3f);
	}

	public void deleteFilter()
	{
		pageCtrl.deleteFilter (gameObject);
	}

	public DataMgr.FilterType getFilterType()
	{
		return filterType;
	}

	public int getFilterIndex()
	{
		return filterIndex;
	}
}
