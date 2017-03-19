using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page7Ctrl : pageBase 
{
	[SerializeField]
	private GameObject switchFilterUI;
	[SerializeField]
	private GameObject indexScroll;
	private DataMgr.FilterType filterType =  DataMgr.FilterType.STAPLE;
	void Awake () 
	{
		
	}

	void Start()
	{
		switchFilterUI.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.F);

		resetScroll ();
	}

	public void showSwitchFilter()
	{
		switchFilterUI.GetComponent<switchCtrl> ().showUI ();
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (8);
	}

	public void resetScroll( DataMgr.FilterType type )
	{
		Debug.logger.Log (string.Format("resetScroll : {0}",type));
		filterType = type;
		resetScroll ();
	}

	public void resetScroll()
	{
		// TODO 之後再做顧客偏好篩選
		System.Random seed = new System.Random();
		custom orderingCustom = DataMgr.Instance.getOrderingCustom();
		scrollCtrl ctrl = indexScroll.GetComponent<scrollCtrl> ();
		ctrl.reset ();

		Dictionary<int, Sprite> spriteMap = spriteMgr.Instance.getIndexSpriteMap (filterType, false);
		foreach( int index in spriteMap.Keys )
		{
			GameObject newObj = ctrl.addItem ();
			int menuNumber = (seed.Next () % 9) + 1;
			newObj.GetComponent<indexCtrl> ().setInfo (spriteMap [index], menuNumber);
		}
	}
}
