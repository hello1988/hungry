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
	[SerializeField]
	private Button backBtn;
	[SerializeField]
	private GameObject[] initList;

	Vector3 oriPos = Vector3.zero;
	void Awake () 
	{
		if (backBtn != null) 
		{
			backBtn.onClick.AddListener (backToMainMenu);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void backToMainMenu()
	{
		LeanTween.scale (gameObject, Vector3.zero, 0.3f).setOnComplete(hide);
		LeanTween.moveLocal (gameObject, oriPos, 0.3f);
		mainMenu.SetActive (true);

		UIMgr.Instance.setHomeBtnVisible (true);
	}

	public void hide()
	{
		gameObject.SetActive (false);
	}

	public void show()
	{
		transform.localScale = Vector3.zero;
		oriPos = transform.localPosition;
		gameObject.SetActive (true);
		LeanTween.scale (gameObject, Vector3.one, 0.3f);
		LeanTween.moveLocal (gameObject, Vector3.zero, 0.3f);

		foreach (GameObject initObj in initList) 
		{
			initObj.SendMessage ("assistInit");
		}

		UIMgr.Instance.setHomeBtnVisible (backBtn == null);
	}

	public void setSprite( Sprite[] sprites )
	{
		int size = Math.Min (sprites.Length, imagesList.Length );
		for( int index = 0;index < size;index++ )
		{
			imagesList [index].sprite = sprites [index];
		}
	}

	public void OnMaskClick()
	{
		backToMainMenu ();
	}

}
