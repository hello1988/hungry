using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class indexCtrl : MonoBehaviour, IPointerClickHandler
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void setInfo( Sprite sprite, int menuNum )
	{
		GetComponent<Image> ().sprite = sprite;

		GetComponentInChildren<numberCtrl> ().setValue (menuNum);
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		Debug.logger.Log ("OnPointerClick");
	}
}
