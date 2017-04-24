using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Const;

public class orderUnit : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private float minWidth = 200;
	[SerializeField]
	private float maxWidth = 300;
	[SerializeField]
	private float tweenSec = 0.3f;
	[SerializeField]
	private page7Ctrl pageCtrl;

	private Staple stapleType;

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSelected( bool isSelected,  bool playTween = true )
	{
		if (isSelected) 
		{
			enlargeStaple (playTween);
		}
		else
		{
			narrowStaple( playTween );
		}
	}

	private void enlargeStaple( bool playTween )
	{
		if (playTween) 
		{
			LeanTween.scale (gameObject, Vector3.one, tweenSec);
		}
		else 
		{
			transform.localScale = Vector3.one;
		}
	}

	private void narrowStaple( bool playTween )
	{
		float scale = minWidth / maxWidth;
		if (playTween) 
		{
			LeanTween.scale (gameObject, Vector3.one*scale, tweenSec);
		}
		else 
		{
			transform.localScale = Vector3.one*scale;
		}
	}

	public void setOrder( Staple type, Sprite sprite )
	{
		stapleType = type;
		GetComponent<Image> ().sprite = sprite;
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		pageCtrl.OnOrderUnitClick (stapleType);
	}
}
