using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class showAssist : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private int assistIndex;
	[SerializeField]
	private page6Ctrl pageCtrl;
	[SerializeField]
	private mainMenuScroll scrollDectect;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnPointerClick (PointerEventData eventData)
	{
		pageCtrl.showAssist (assistIndex);
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		scrollDectect.OnPointerDown (eventData);
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		scrollDectect.OnPointerUp (eventData);
	}
}
