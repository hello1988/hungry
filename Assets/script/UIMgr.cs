using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour 
{
	[SerializeField]
	private Sprite[] backgroundList;

	public enum BG
	{
		A,
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
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public Sprite getBackground( BG index )
	{
		return backgroundList [(int)index];
	}
}
