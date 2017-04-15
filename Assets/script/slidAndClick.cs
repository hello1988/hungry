using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class slidAndClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	public delegate void dSlideCallBack();
	public enum Axis
	{
		X,
		Y,
	}

	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT,
	}

	[SerializeField]
	private Axis detectAxis = Axis.X;	// 偵測軸線

	private GameObject touchPoint;
	private Vector3 startPos = Vector3.zero;
	private dSlideCallBack[] callBackList = new dSlideCallBack[4];

	void Awake () {
		touchPoint = new GameObject ("touchPoint");	
		touchPoint.transform.SetParent (transform.parent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		startPos = touchPoint.transform.localPosition;

	}

	public void OnPointerUp (PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		Vector3 endPos = touchPoint.transform.localPosition;

		Direction dir;
		// 跨過X軸 上下偵測
		if(detectAxis == Axis.X)
		{
			// 在同一側 視為點擊
			if (startPos.y * endPos.y >= 0) 
			{
				dir = ( endPos.y > 0 ) ? Direction.UP: Direction.DOWN ;
			}
			else 
			{
				dir = ( endPos.y > startPos.y ) ? Direction.UP: Direction.DOWN ;
			}
		}

		// 跨過Y軸 左右偵測
		else
		{
			// 在同一側 視為點擊
			if (startPos.x * endPos.x >= 0) 
			{
				dir = ( endPos.x > 0 ) ? Direction.RIGHT: Direction.LEFT ;
			}
			else
			{
				dir = ( endPos.x > startPos.x ) ? Direction.RIGHT: Direction.LEFT ;
			}
		}

		dSlideCallBack callBack = callBackList [(int)dir];
		if (callBack != null) 
		{
			callBack ();
		}
	}

	public void setCallBack( Direction dir, dSlideCallBack callBack)
	{
		if (callBack == null) {return;}

		callBackList [(int)dir] = callBack;
	}
}
