using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class clickAndSpeech : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private assist2Ctrl assCtrl;
	[SerializeField]
	private string speechKey;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		assCtrl.playTextSpeech (speechKey);
	}
}
