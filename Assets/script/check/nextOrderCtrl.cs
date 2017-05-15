using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextOrderCtrl : MonoBehaviour
{
	[SerializeField]
	private GameObject[] dotList;
	[SerializeField]
	private float tweenSec = 0.3f;

	void Awake () 
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showDot( int idx )
	{
		for( int index = 0;index < dotList.Length;index++)
		{
			float scale = (idx == index) ? 1.5f: 1.0f;

			LeanTween.scale (dotList [index], Vector2.one*scale, tweenSec);
		}
	}
}
