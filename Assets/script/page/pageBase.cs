using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pageBase : MonoBehaviour 
{
	[SerializeField]
	protected GameObject nextBtn;

	protected bool homeVisible = true;

	public virtual void onPageEnable(){}

	void OnEnable() 
	{
		UIMgr.Instance.setHomeBtnVisible (homeVisible);
	}

	public void setNextBtnActive( bool isActive )
	{
		nextBtn.SetActive (isActive);
	}
}
