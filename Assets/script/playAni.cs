using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playAni : MonoBehaviour 
{
	[SerializeField]
	private Image target;
	[SerializeField]
	private float delaySec;
	[SerializeField]
	private Sprite[] spriteList;

	private int frame = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void play()
	{
		StopAllCoroutines ();
		StartCoroutine(nextFrame());
	}

	public IEnumerator nextFrame()
	{
		yield return new WaitForSeconds (delaySec);

		frame = (frame + 1) % spriteList.Length;
		target.sprite = spriteList [frame];
		StartCoroutine(nextFrame());
	}

	public void stop()
	{
		StopAllCoroutines ();
		frame = 0;
		target.sprite = spriteList [0];
	}
}
