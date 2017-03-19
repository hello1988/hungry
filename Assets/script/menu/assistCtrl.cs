using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class assistCtrl : MonoBehaviour 
{
	[SerializeField]
	private GameObject mainMenu;
	[SerializeField]
	private Image[] imagesList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void backToMainMenu()
	{
		LeanTween.scale (gameObject, Vector3.zero, 0.3f).setOnComplete(hide);
		mainMenu.SetActive (true);

	}

	public void hide()
	{
		gameObject.SetActive (false);
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
