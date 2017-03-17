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
	private float scrollWidth = 0f;
	void Awake()
	{
		RectTransform scrollRect = scrollPanel.GetComponent<RectTransform> ();
		scrollWidth = scrollRect.sizeDelta.x;
		scrollHeight = scrollRect.sizeDelta.y;

	}

	// Use this for initialization
	void Start () 
	{
		/* test
		addItem ();
		addItem ();
		addItem ();
		addItem ();
		addItem ();
		addItem ();
		addItem ();
		*/
	}

	public GameObject addItem()
	{
		GameObject clone = Instantiate<GameObject> (template);

		clone.name = string.Format("item{0}",itemIndex++);
		clone.transform.SetParent( this.transform,false );
		clone.transform.localScale = Vector3.one;
		clone.transform.localPosition = Vector3.zero;
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

		float xOffset = tempRect.sizeDelta.x + 10;
		float yOffset = tempRect.sizeDelta.y + 30;

		int colCount = Convert.ToInt32 (Math.Floor (scrollWidth / xOffset));
		int rowCount = Convert.ToInt32 (Math.Ceiling (itemList.Count / (float)colCount));

		float newHeight = Math.Max( scrollHeight, yOffset * rowCount );

		RectTransform rect = this.GetComponent<RectTransform> ();
		rect.sizeDelta = new Vector2 (rect.sizeDelta.x, newHeight);

		float top = (float)( (newHeight / 2.0) - yOffset/2.0 );
		float totalWidth = colCount * xOffset;
		float left = -(totalWidth/2)+(xOffset/2);

		int itemCount = 0;
		for( int index = 0;index < rowCount;index++ )
		{
			for(int jIndex = 0;jIndex < colCount;jIndex++)
			{
				if (itemCount >= itemList.Count) {break;}

				float x = left + xOffset * jIndex;
				float y = top - yOffset * index;
				itemList [itemCount].transform.localPosition = new Vector3(x, y,0);
				itemCount++;
			}
		}

		if (rollToTop) 
		{
			this.transform.localPosition = new Vector3(0,-((newHeight - scrollHeight) / 2),0);
		}
	}
}
