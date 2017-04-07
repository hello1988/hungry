using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tweenAlpha : MonoBehaviour 
{
	[SerializeField]
	private float maxAlpha = 1.0f;
	[SerializeField]
	private float minAlpha = 0.5f;
	[SerializeField]
	private float duringSec = 0.5f;

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		// GetComponent<Image> ().color = new Color32(255,255,255,maxAlpha);
		step1 ();
	}

	private void step1()
	{
		if (!gameObject.activeInHierarchy) {return;}

		// LeanTween.alpha (gameObject, minAlpha, duringSec).setOnComplete (step2);
		LeanTween.value (gameObject, setAlpha, maxAlpha, minAlpha, duringSec).setOnComplete(step2);
	}

	private void step2()
	{
		if (!gameObject.activeInHierarchy) {return;}

		// LeanTween.alpha (gameObject, maxAlpha, duringSec).setOnComplete (step1);
		LeanTween.value (gameObject, setAlpha, minAlpha, maxAlpha, duringSec).setOnComplete(step1);
	}

	private void setAlpha( float value )
	{
		GetComponent<Image> ().color = new Color(1f,1f,1f,value);
	}
}
