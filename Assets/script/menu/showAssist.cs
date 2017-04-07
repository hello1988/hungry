using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class showAssist : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private int assistIndex;
	[SerializeField]
	private page6Ctrl pageCtrl;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnPointerClick (PointerEventData eventData)
	{
		pageCtrl.showAssist (assistIndex);
	}
}
