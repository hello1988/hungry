using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// public class page8Ctrl : pageBase, IPointerDownHandler, IPointerUpHandler
public class page6Ctrl : pageBase
{
	[SerializeField]
	private GameObject mainMenu;
	[SerializeField]
	private GameObject[] assistList;
	[SerializeField]
	private Button preMenu;
	[SerializeField]
	private Button nextMenu;

	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);

		preMenu.onClick.AddListener (toPreMenu);
		nextMenu.onClick.AddListener (toNextMenu);

	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.F);
		setMenuInfo ();
	}

	public void showAssist( int assistIndex )
	{
		if( (assistIndex < 0) || (assistIndex >= assistList.Length)){return ;}

		// mainMenu.SetActive (false);
		foreach( GameObject assist in assistList )
		{
			assist.SetActive (false);
		}

		custom curCustom = DataMgr.Instance.getOrderingCustom ();
		menu m = curCustom.getMenu (0);

		assistList [assistIndex].transform.position = UIMgr.Instance.getCurMousePosition();
		assistCtrl ctrl = assistList [assistIndex].GetComponent<assistCtrl> ();
		ctrl.show ();
		ctrl.setSprite (m.getAssistSpriteList (assistIndex));
	}

	public void onItemDrop(GameObject item)
	{
		// Debug.logger.Log (string.Format("p6 onItemDrop : {0}",item.name));

		custom orderingCus = DataMgr.Instance.getOrderingCustom ();
		int menuID = orderingCus.getMenu(0).getMenuID();
		orderingCus.modifyConfirmMenu (menuID,1);
	}

	public void toPreMenu()
	{
		LeanTween.moveLocalY (gameObject, 3000, 0.3f);
		setMenuInfo (-1);

		transform.localPosition = new Vector3 (0,-3000,0);
		LeanTween.moveLocalY (gameObject, 0, 0.3f);
	}

	public void toNextMenu()
	{
		LeanTween.moveLocalY (gameObject, -3000, 0.3f);
		setMenuInfo (1);

		transform.localPosition = new Vector3 (0, 3000,0);
		LeanTween.moveLocalY (gameObject, 0, 0.3f);
	}

	public void nextPage()
	{
		custom cus = DataMgr.Instance.getOrderingCustom ();
		if (cus.getConfirmMenu ().Count <= 0) {return;}

		pageMgr.Instance.nextPage (7);
	}

	private void setMenuInfo( int offset = 0)
	{
		custom curCustom = DataMgr.Instance.getOrderingCustom ();
		menu m = curCustom.getMenu (offset);

		setNextBtnActive(true);
		mainMenu.SetActive (true);
		mainMenu.GetComponent<mainMenuCtrl> ().setSprite (m.getMainSpriteList());
		foreach( GameObject assist in assistList )
		{
			assist.SetActive (false);
		}
	}

}
