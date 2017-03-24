using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class demoSlide : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private Image numberImg;
	[SerializeField]
	private Sprite[] numberList;

	private Vector3 startPos;
	private int numberIndex;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		numberIndex = 0;
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		startPos = UIMgr.Instance.getCurMousePosition ();
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		Vector3 endPos = UIMgr.Instance.getCurMousePosition ();

		float xOffset = endPos.x - startPos.x;
		if (xOffset > 0) 
		{
			numberIndex = Math.Min ((numberIndex + 1), (numberList.Length-1));
		}
		else if (xOffset < 0) 
		{
			numberIndex = Math.Max ((numberIndex - 1), 0);
		}

		updateNumberImg ();
	}

	private void updateNumberImg()
	{
		numberImg.sprite = numberList [numberIndex];
	}
}
