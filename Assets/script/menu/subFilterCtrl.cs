﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class subFilterCtrl : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private GameObject target;

	public int subIndex;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getSubIndex()
	{
		return subIndex;
	}

	public void setSubIndex( int idx )
	{
		subIndex = idx;
	}

	public void setInfo( Sprite sprite, int menuNum )
	{
		GetComponent<Image> ().sprite = sprite;

		GetComponentInChildren<numberCtrl> ().setValue (menuNum);
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		target.SendMessage ("OnSubFilterClick", gameObject);
	}
}
