using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class downloadMgr : MonoBehaviour 
{
	public delegate void dSprite(Sprite sprite, object userData = null);

	private static downloadMgr _instance = null;
	public static downloadMgr Instance
	{
		get{return _instance;}
	}

	// Use this for initialization
	private void Awake ()
	{
		_instance = this;
	}

	public void downloadSprite(string path, dSprite callBack, object userData = null)
	{
		Sprite sprite = Resources.Load<Sprite>(path);
		callBack (sprite, userData);
	}

}
