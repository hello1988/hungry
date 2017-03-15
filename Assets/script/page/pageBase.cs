using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pageBase : MonoBehaviour 
{
	[SerializeField]
	private GameObject background;
	[SerializeField]
	protected GameObject checkBtn;

	public virtual void onPageEnable(){}

	public void setBackground( UIMgr.BG index )
	{
		background.SetActive (true);
		Image img = background.GetComponent<Image> ();
		img.sprite = UIMgr.Instance.getBackground (index);
	}

	public void setCheckBtnActive( bool isActive )
	{
		checkBtn.SetActive (isActive);
	}
}
