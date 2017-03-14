using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour 
{
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


}
