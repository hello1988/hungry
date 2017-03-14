using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class dropAndClick : MonoBehaviour
{
	public void Awake()
	{
		Button btn = GetComponent<Button> ();
		btn.onClick.AddListener (onBtnClick);
	}
	
	public void onBtnClick()
	{
		Debug.logger.Log ("onBtnClick");
	}

}
