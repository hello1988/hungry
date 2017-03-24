using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class demoClick : MonoBehaviour, IPointerClickHandler 
{
	[SerializeField]
	private page9Ctrl pageCtrl;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		pageCtrl.onImgClick ();
	}
}
