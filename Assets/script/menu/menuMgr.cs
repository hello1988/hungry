using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMgr : MonoBehaviour 
{
	Dictionary<DataMgr.FilterType,List<menu>> menuMap;

	private static menuMgr _instance = null;
	public static menuMgr Instance
	{
		get{return _instance;}
	}

	// Use this for initialization
	private void Awake ()
	{
		_instance = this;
	}

	void Start()
	{
		buildMenuMap ();
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	// TODO
	public List<menu> getPreferMenu( custom cus )
	{
		return new List<menu> ();
	}

	private void buildMenuMap()
	{
		menuMap = new Dictionary<DataMgr.FilterType, List<menu>> ();
	}

}
