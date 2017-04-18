using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class orderUnit : MonoBehaviour 
{
	enum spriteMode
	{
		NONE,
		DOT,
		ORDER,
	}

	[SerializeField]
	private Sprite dot;
	[SerializeField]
	private float dotWidth;
	[SerializeField]
	private float stapleWidth;
	[SerializeField]
	private float tweenSec = 0.3f;

	private Sprite orderImg;
	private spriteMode mode = spriteMode.NONE;
	private LTDescr tweenID = null;

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setOrderImg( Sprite sprite )
	{
		orderImg = sprite;
	}

	public void showDot( bool playTween = true )
	{
		if (mode == spriteMode.DOT) {return;}

		mode = spriteMode.DOT;
		float scale = (dotWidth/stapleWidth);

		if (tweenID != null) 
		{
			LeanTween.descr (tweenID.id);
			tweenID = null;
		}

		if (playTween) 
		{
			tweenID = LeanTween.scale (gameObject, Vector3.one * scale, tweenSec).setOnComplete (setDotToImage);
		}
		else 
		{
			setDotToImage ();
		}
	}

	private void setDotToImage()
	{
		transform.localScale = Vector3.one;
		GetComponent<Image> ().sprite = dot;

		RectTransform rect = GetComponent<RectTransform> ();
		rect.sizeDelta = Vector2.one * dotWidth;
	}

	public void showOrderImg()
	{
		if (mode == spriteMode.ORDER) {return;}

		RectTransform rect = GetComponent<RectTransform> ();
		rect.sizeDelta = Vector2.one * stapleWidth;

		mode = spriteMode.ORDER;
		float scale = (dotWidth/stapleWidth);
		transform.localScale = Vector3.one*scale;

		GetComponent<Image> ().sprite = orderImg;

		if (tweenID != null) 
		{
			LeanTween.descr (tweenID.id);
			tweenID = null;
		}
		tweenID = LeanTween.scale (gameObject, Vector3.one, tweenSec);
	}
}
