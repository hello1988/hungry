﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sauseCtrl : MonoBehaviour 
{
	[SerializeField]
	private GameObject progressImg;
	[SerializeField]
	private GameObject pointer;

	private Vector3 oriPos;
	private Vector2 oriSize;
	private float left;
	// Use this for initialization
	void Awake () 
	{
		oriPos = progressImg.transform.localPosition;

		RectTransform rect = progressImg.GetComponent<RectTransform> ();
		oriSize = rect.sizeDelta;

		left = oriPos.x - oriSize.x / 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void assistInit()
	{
		// Debug.logger.Log ("assistInit");
		setProgress (0.5f);
	}

	public void setProgress( float progress )
	{
		float width = oriSize.x*progress;

		RectTransform rect = progressImg.GetComponent<RectTransform> ();
		rect.sizeDelta = new Vector2(width, oriSize.y);

		float posX = left + width / 2;
		progressImg.transform.localPosition = new Vector3( posX, oriPos.y, oriPos.z );
	}
}
