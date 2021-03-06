﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Const;

public class selectFilter : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private page3Ctrl pageCtrl;

	private FilterType filterType;
	private int filterIndex;
	private bool selected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setFilterType( FilterType type )
	{
		filterType = type;
	}

	public FilterType getFilterType()
	{
		return filterType;
	}

	public void setFilterIndex(int id)
	{
		filterIndex = id;
	}

	public int getFilterIndex()
	{
		return filterIndex;
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		pageCtrl.onFilterClick (this);
	}

	public void setSelected(bool selected)
	{
		// Debug.logger.Log (string.Format("setSelected : {0}",selected));
		this.selected = selected;
		Image img = GetComponent<Image> ();
		if (selected) 
		{
			img.color = new Color32 (185, 170, 175, 150);
		}
		else
		{
			img.color = new Color32 (255, 255, 255, 255);
		}
	}

	public bool isSelected()
	{
		return selected;
	}
}
