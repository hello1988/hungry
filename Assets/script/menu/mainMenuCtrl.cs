using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenuCtrl : MonoBehaviour 
{
	[SerializeField]
	private Image[] imagesList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSprite( Sprite[] sprites )
	{
		int size = Math.Min (sprites.Length, imagesList.Length );

		for( int index = 0;index < size;index++ )
		{
			imagesList [index].sprite = sprites [index];
		}
	}
}
