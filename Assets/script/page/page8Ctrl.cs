using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page8Ctrl : pageBase 
{
	[SerializeField]
	private GameObject mainMenu;
	[SerializeField]
	private GameObject[] assistList;
	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.F);

		setNextBtnActive(true);
		mainMenu.SetActive (true);
		foreach( GameObject assist in assistList )
		{
			assist.SetActive (false);
		}
	}

	public void showAssist( int assistIndex )
	{
		if( (assistIndex < 0) || (assistIndex >= assistList.Length)){return ;}

		// mainMenu.SetActive (false);
		foreach( GameObject assist in assistList )
		{
			assist.SetActive (false);
		}

		assistList [assistIndex].transform.position = UIMgr.Instance.getCurMousePosition();
		assistList [assistIndex].GetComponent<assistCtrl> ().show ();
	}

	public void onItemDrop(GameObject item)
	{
		Debug.logger.Log (string.Format("p8 onItemDrop : {0}",item.name));
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (9);
	}

}
