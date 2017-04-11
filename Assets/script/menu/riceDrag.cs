using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class riceDrag : MonoBehaviour,IDragHandler, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private int fadeDistance = 300;
	[SerializeField]
	private riceCtrl rice;
	[SerializeField]
	private GameObject[] anchorList;
	[SerializeField]
	private GameObject[] appetiteList;
	[SerializeField]
	private GameObject touchPoint;

	private Vector3 oriPos;
	private float left;
	private float right;
	private int dragCounter = 0;
	void Awake () 
	{
		oriPos = transform.localPosition;
		left = anchorList[0].transform.localPosition.x-30;
		right = anchorList[anchorList.Length - 1].transform.localPosition.x+30;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrag(PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition();
		float posX = touchPoint.transform.localPosition.x;
		posX = Math.Min ( Math.Max( posX, left ), right );
		transform.localPosition = new Vector3(posX, oriPos.y, oriPos.z );

		dragCounter++;
		if ((dragCounter % 5) == 0) 
		{
			setAppetiteAlpha ();
		}
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		dragCounter = 0;
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		int anchorIndex = findNearestAnchor ();
		setPositionToAnchor (anchorIndex);

		GameObject appetite = appetiteList [anchorIndex];
		rice.setAppetite (appetite);
	}

	public void setPositionToAnchor( int index )
	{
		if ((index < 0) || (index >= anchorList.Length)) {return;}

		GameObject anchor = anchorList [index];
		Vector3 anchorPos = anchor.transform.localPosition;
		transform.localPosition = new Vector3(anchorPos.x, oriPos.y, oriPos.z );

		setAppetiteAlpha ();
	}

	private void setAppetiteAlpha()
	{
		Vector3 pos = transform.localPosition;
		for( int index = 0;index < appetiteList.Length;index++ )
		{
			Vector3 appetitePos = appetiteList [index].transform.localPosition;
			float distance = Math.Min( Math.Abs ( appetitePos.x - pos.x ), fadeDistance );
			float alpha = (fadeDistance - distance) / fadeDistance;

			Image img = appetiteList [index].GetComponent<Image> ();
			img.color = new Color (1,1,1,alpha);
		}
	}

	private int findNearestAnchor ()
	{
		Vector3 pos = transform.localPosition;
		int nearestDistance = int.MaxValue;
		int nearestIndex = -1;
		for( int index = 0;index < anchorList.Length;index++ )
		{
			Vector3 anchorPos = anchorList [index].transform.localPosition;
			int distance = (int)Math.Abs (anchorPos.x - pos.x);
			if (distance < nearestDistance) 
			{
				nearestDistance = distance;
				nearestIndex = index;
			}
		}

		return nearestIndex;
	}
}
