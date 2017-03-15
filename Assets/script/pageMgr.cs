using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pageMgr : MonoBehaviour 
{
	[SerializeField]
	private GameObject[] pageList;

	private int curPage = 0;

	private static pageMgr _instance = null;
	public static pageMgr Instance
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
		if ((pageList == null) || (pageList.Length == 0)) {return;}

		foreach( GameObject page in pageList )
		{
			page.SetActive (false);
		}

		pageList [0].SetActive (true);
		pageBase page1 = pageList [0].GetComponent<pageBase> ();
		page1.onPageEnable ();
		curPage = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void nextPage( int nextPage )
	{
		if( (nextPage < 0) || (nextPage >= pageList.Length)){return;}

		// TODO 換頁動畫
		pageList [curPage].SetActive(false);
		pageList [nextPage].SetActive(true);
		pageList [nextPage].GetComponent<pageBase> ().onPageEnable ();
		curPage = nextPage;

	}
}
