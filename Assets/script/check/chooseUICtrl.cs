using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseUICtrl : MonoBehaviour 
{
	[SerializeField]
	private GameObject mask;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void showUI()
	{
		Vector3 pos = transform.localPosition;
		transform.localPosition = new Vector3(pos.x+3000, pos.y, pos.z);
		transform.localScale = Vector3.one;
		mask.SetActive (false);

		LeanTween.moveLocalX (gameObject, 0, 0.3f).setOnComplete(showMask);
	}

	public void hideUI()
	{
		mask.SetActive (false);
		LeanTween.moveLocalX (gameObject, 3000, 0.3f);
	}

	public void showMask()
	{
		mask.SetActive (true);
	}
}
