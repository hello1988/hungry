﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class maskClick : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		target.SendMessage ("OnMaskClick");
	}
}
