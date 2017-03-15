using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pageMgr : MonoBehaviour 
{
	[SerializeField]
	private GameObject[] pageList;

	private Stack<int> record = new Stack<int>();

	private int curPage = 0;

	private enum DIR
	{
		LEFT,
		RIGHT,
	}

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

	/**下一頁*/
	public void nextPage( int nextPage )
	{
		if( (nextPage < 0) || (nextPage >= pageList.Length)){return;}

		pageEffect (DIR.RIGHT, curPage, nextPage);

		record.Push (curPage);
		curPage = nextPage;

	}

	/**上一頁*/
	public void prePage()
	{
		if (record.Count == 0) {return;}

		int pre = record.Pop ();
		pageEffect (DIR.LEFT, curPage, pre);

		curPage = pre;
	}

	// TODO 換頁動畫
	private void pageEffect( DIR dir, int curren, int target  )
	{
		pageList [curren].SetActive(false);
		pageList [target].SetActive(true);
		pageList [target].GetComponent<pageBase> ().onPageEnable ();
	}
}
