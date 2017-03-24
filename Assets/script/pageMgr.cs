using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pageMgr : MonoBehaviour 
{
	[SerializeField]
	private GameObject[] pageList;
	[SerializeField]
	private int startPage = 0;

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

		pageList [startPage].SetActive (true);
		pageBase showPage = pageList [startPage].GetComponent<pageBase> ();
		showPage.onPageEnable ();
		curPage = startPage;
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
		UIMgr.Instance.setHomeBtnVisible (false);

		pageList [curren].SetActive(false);
		pageList [target].SetActive(true);
		pageList [target].GetComponent<pageBase> ().onPageEnable ();
	}

	public void homePage()
	{
		resetData ();
		DataMgr.Instance.resetData ();

		foreach( GameObject page in pageList )
		{
			page.SetActive (false);
		}

		curPage = 0;
		pageList [curPage].SetActive (true);
		pageBase showPage = pageList [curPage].GetComponent<pageBase> ();
		showPage.onPageEnable ();
		UIMgr.Instance.setHomeBtnVisible (false);
	}

	public void resetData()
	{
		record.Clear ();
	}
}
