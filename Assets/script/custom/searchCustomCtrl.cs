using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class searchCustomCtrl : MonoBehaviour
{
	[SerializeField]
	private GameObject curCustom;
	[SerializeField]
	private GameObject nextCustom;
	[SerializeField]
	private GameObject addCustom;
	[SerializeField]
	private page3Ctrl pageCtrl;

	private int customIndex = -1;	// 目前顯示顧客
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void refreshSearchList ()
	{
		List<custom> searchCustomList = DataMgr.Instance.getSearchCustomList ();
		if (searchCustomList.Count > 0) 
		{
			customIndex = 0;
		} 

		setCurCustomImg();
		setNextCustomImg ();
	}

	public void showNextCustom ()
	{
		Vector3 pos = curCustom.transform.position;
		// curCustom.transform.localPosition = Vector3.Lerp( pos, new Vector3(pos.x, (pos.y+5000),pos.z), 0.2f);
		LeanTween.move(curCustom, new Vector3(pos.x, (pos.y+400), pos.z), 0.3f).setOnComplete(prepareNextCustome);
		LeanTween.scale (nextCustom, Vector3.zero, 0.3f);
	}

	public void prepareNextCustome()
	{
		curCustom.transform.localScale = Vector3.zero;

		List<custom> searchCustomList = DataMgr.Instance.getSearchCustomList ();
		if ((customIndex < 0) || (customIndex >= searchCustomList.Count)) 
		{
			return;
		}

		customIndex++;

		setCurCustomImg();
		setNextCustomImg ();
		curCustom.GetComponent<searchCustomListener> ().resume ();
		LeanTween.scale (nextCustom, Vector3.one, 0.3f);
	}

	public void confirmCunstom ()
	{
		List<custom> searchCustomList = DataMgr.Instance.getSearchCustomList ();
		if ((customIndex < 0) || (customIndex >= searchCustomList.Count)) 
		{
			return;
		}

		custom cus = searchCustomList[customIndex];
		pageCtrl.addCustomToConfirm (cus);
		LeanTween.scale (curCustom , Vector3.zero, 0.3f).setOnComplete(prepareNextCustome);
	}


	private void setCurCustomImg()
	{
		List<custom> searchCustomList = DataMgr.Instance.getSearchCustomList ();
		if( (customIndex < 0) || (customIndex >= searchCustomList.Count) )
		{
			curCustom.SetActive (false);
			return;
		}

		custom cus = searchCustomList [customIndex];
		curCustom.SetActive (true);
		foreach (Image img in curCustom.GetComponentsInChildren<Image>()) 
		{
			if (img.name == "photo") 
			{
				img.sprite = cus.cusPhoto;
			} 
			else if (img.name == "name") 
			{
				img.sprite = cus.cusName;
			}
		}
	}

	private void setNextCustomImg()
	{
		List<custom> searchCustomList = DataMgr.Instance.getSearchCustomList ();
		if( (customIndex < 0) || ((customIndex+1) >= searchCustomList.Count) )
		{
			nextCustom.SetActive (false);
			addCustom.SetActive (true);
			return;
		}

		nextCustom.SetActive (true);
		addCustom.SetActive (false);
		custom cus = searchCustomList [customIndex+1];
		foreach (Image img in nextCustom.GetComponentsInChildren<Image>()) 
		{
			if (img.name == "photo") 
			{
				img.sprite = cus.cusPhoto;
			}
		}
	}
}
