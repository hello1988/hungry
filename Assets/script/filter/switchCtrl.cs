using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class switchCtrl : MonoBehaviour , IPointerClickHandler
{
	[SerializeField]
	private GameObject mask;
	[SerializeField]
	private page7Ctrl pageCtrl;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showUI()
	{
		transform.localPosition = new Vector3 (1600, 0, 0);
		transform.localScale = Vector3.one;
		gameObject.SetActive (true);
		mask.SetActive (false);

		LeanTween.moveLocalX (gameObject, 0, 0.3f).setOnComplete(showMask);
	}

	public void showMask()
	{
		mask.SetActive (true);
	}

	void hideUI()
	{
		mask.SetActive (false);
		LeanTween.moveLocalX (gameObject, 1600, 0.3f);

	}

	public void OnPointerClick(PointerEventData e)
	{
		hideUI();
	}

	// 以料理方式排序
	public void onSwitch1Click()
	{
		resetScroll( DataMgr.FilterType.COOK );
	}

	// 以食材排序
	public void onSwitch2Click()
	{
		resetScroll( DataMgr.FilterType.FOOD );
	}

	// 以主食排序
	public void onSwitch3Click()
	{
		resetScroll( DataMgr.FilterType.STAPLE );
	}

	private void resetScroll(DataMgr.FilterType switchType)
	{
		hideUI ();
		pageCtrl.resetScroll (switchType);
	}
}
