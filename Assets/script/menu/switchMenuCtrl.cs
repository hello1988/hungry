using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Const;

public class switchMenuCtrl : MonoBehaviour
{
	[SerializeField]
	private float left;
	[SerializeField]
	private float right;
	[SerializeField]
	private GameObject mask;
	[SerializeField]
	private GameObject subFilterScroll;
	[SerializeField]
	private switchMenuDrag leftBar;
	[SerializeField]
	private GameObject switchFilter;

	FilterType filterType;
	void Awake () 
	{
		mask.SetActive (false);

		Vector3 pos = transform.localPosition;
		transform.localPosition = new Vector3 (left, pos.y, pos.z);

		leftBar.setInfo ( left, right );
		switchFilter.SetActive (false);

		filterType = FilterType.COOK;
		resetScroll ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMaskClick()
	{
		hideMenu (0.3f);
	}

	public void hideMenu( float seconds )
	{
		mask.SetActive (false);
		LeanTween.moveLocalX (gameObject, left, seconds);
	}

	public void showMenu( float seconds )
	{
		LeanTween.moveLocalX (gameObject, right, seconds).setOnComplete(showMask);
	}

	public void hideMask()
	{
		mask.SetActive (false);
	}

	public void showMask()
	{
		mask.SetActive (true);
	}

	public void resetScroll ( FilterType type )
	{
		filterType = type;
		resetScroll ();
	}

	public void resetScroll ()
	{
		// TODO 之後再做顧客偏好篩選
		custom orderingCustom = DataMgr.Instance.getOrderingCustom();
		System.Random seed = new System.Random();
		scrollCtrl ctrl = subFilterScroll.GetComponent<scrollCtrl> ();
		ctrl.reset ();

		Dictionary<int, Sprite> spriteMap = spriteMgr.Instance.getIndexSpriteMap (filterType, true);
		foreach( int index in spriteMap.Keys )
		{
			for( int c = 0; c < 3;c++)
			{
				GameObject newObj = ctrl.addItem ();
				// TODO 幾道菜等表單建好再計算
				int menuNumber = (seed.Next () % 9) + 1;

				subFilterCtrl sCtrl = newObj.GetComponent<subFilterCtrl> ();
				sCtrl.setInfo (spriteMap [index], menuNumber);
				sCtrl.setSubIndex (index);
			}
		}
	}

	public void OnSubFilterClick( GameObject clickedObj )
	{
		Debug.logger.Log ("OnSubFilterClick");
		hideMenu (0.3f);
	}

	public void OnFilterBtnClick()
	{
		bool isShow = !switchFilter.activeInHierarchy;
		switchFilter.SetActive (isShow);
	}

	public void OnSwitchFilterClick( int filterID )
	{
		switchFilter.SetActive (false);

		FilterType type = (FilterType)filterID;
		resetScroll (type);
	}
}
