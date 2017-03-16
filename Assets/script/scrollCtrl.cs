using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollCtrl : MonoBehaviour 
{
	[SerializeField]
	private GameObject template;
	[SerializeField]
	private GameObject scrollPanel;

	private List<GameObject> itemList = new List<GameObject> ();
	private int itemIndex = 0;
	private float scrollHeight = 0f;
	void Awake()
	{
		RectTransform scrollRect = scrollPanel.GetComponent<RectTransform> ();
		scrollHeight = scrollRect.sizeDelta.y;
	}

	// Use this for initialization
	void Start () 
	{

	}

	public GameObject addItem()
	{
		GameObject clone = Instantiate<GameObject> (template);

		clone.name = string.Format("item{0}",itemIndex++);
		clone.transform.localScale = Vector3.one;
		clone.transform.localPosition = Vector3.zero;
		clone.transform.SetParent( this.transform,false );
		clone.SetActive (true);

		itemList.Add (clone);
		rePosition (true);

		return clone;
	}

	public void delItem( string name )
	{
		bool removed = false;
		for( int index = 0;index < itemList.Count;index++ )
		{
			if (itemList [index].name == name) 
			{
				removed = true;
				GameObject target = itemList [index];
				itemList.Remove (target);
				Destroy (target);
				break;
			}
		}
		if (removed) 
		{
			rePosition ();
		}
	}

	private void rePosition(bool rollToTop = false)
	{
		RectTransform tempRect = template.GetComponent<RectTransform> ();
		float yOffset = tempRect.sizeDelta.y + 30;

		float newHeight = Math.Max( scrollHeight, yOffset * itemList.Count );

		RectTransform rect = this.GetComponent<RectTransform> ();
		rect.sizeDelta = new Vector2 (rect.sizeDelta.x, newHeight);

		float top = (float)( (newHeight / 2.0) - yOffset/2.0 );

		for( int index = 0;index < itemList.Count;index++ )
		{
			itemList [index].transform.localPosition = new Vector3(0,(top - yOffset*index),0);
		}

		if (rollToTop) 
		{
			this.transform.localPosition = new Vector3(0,-((newHeight - scrollHeight) / 2),0);
		}
	}
}
