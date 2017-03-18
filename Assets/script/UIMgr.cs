using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour 
{
	[SerializeField]
	private Image background;
	[SerializeField]
	private Sprite[] backgroundList;
	[SerializeField]
	private GameObject home;
	[SerializeField]
	private GameObject canvasObj;

	public enum BG
	{
		A,
		C,
		E,
		F,
	}

	private static UIMgr _instance = null;
	public static UIMgr Instance
	{
		get{return _instance;}
	}

	// Use this for initialization
	private void Awake ()
	{
		_instance = this;
		System.DateTime dt = new System.DateTime ();
		(System.DateTime.Now-dt).TotalMilliseconds
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void setBackground( BG index )
	{
		background.sprite = backgroundList[(int)index];
	}

	public void setHomeBtnVisible( bool visible )
	{
		home.SetActive (visible);
	}

	public float getplaneDistance()
	{
		return canvasObj.GetComponent<Canvas> ().planeDistance;
	}

	public Vector3 getCurMousePosition()
	{
		float distance = getplaneDistance ();
		return Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance) );
	}
}
