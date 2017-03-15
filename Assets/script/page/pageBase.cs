using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pageBase : MonoBehaviour 
{
	[SerializeField]
	protected GameObject nextBtn;

	public virtual void onPageEnable(){}

	public void setNextBtnActive( bool isActive )
	{
		nextBtn.SetActive (isActive);
	}
}
