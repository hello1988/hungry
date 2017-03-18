using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page4Ctrl : pageBase 
{
	[SerializeField]
	private GameObject curCustom;
	[SerializeField]
	private Button left;
	[SerializeField]
	private Button right;

	private int customIndex = 0;

	// private Button checkButton;
	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);
		homeVisible = false;

		left.onClick.AddListener (preCustom);
		right.onClick.AddListener (nextCustom);

		customIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void preCustom()
	{
		if (customIndex <= 0) {return;}

		customIndex--;
		LeanTween.moveLocalX (curCustom.gameObject, 1300, 0.3f);
		LeanTween.scale (curCustom.gameObject, Vector3.zero, 0.3f).setOnComplete (mirrorX);
	}

	public void nextCustom()
	{
		List<custom> confirmList = DataMgr.Instance.getConfirmCustomList ();
		if ((customIndex + 1) >= confirmList.Count) {return;}

		customIndex++;
		LeanTween.moveLocalX (curCustom.gameObject, -1300, 0.3f);
		LeanTween.scale (curCustom.gameObject, Vector3.zero, 0.3f).setOnComplete (mirrorX);
	}

	public void mirrorX()
	{
		Vector3 pos = curCustom.transform.localPosition;
		curCustom.transform.localPosition = new Vector3(-pos.x,pos.y,pos.z);

		setCustomImg ();
		curCustom.GetComponent<confirmCustomDetect> ().resume ();
	}

	public void setCustomImg()
	{
		List<custom> confirmList = DataMgr.Instance.getConfirmCustomList ();
		custom cus = confirmList [customIndex];
		curCustom.GetComponent<confirmCustomDetect> ().setImageByCustom (cus);

		updatePageBtn ();
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.C);
		setNextBtnActive(true);
		setCustomImg ();
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (5);
	}

	private void updatePageBtn()
	{
		List<custom> confirmList = DataMgr.Instance.getConfirmCustomList ();
		bool isLeftShow = (customIndex > 0);
		bool isRightShow = ((customIndex + 1) < confirmList.Count);

		left.gameObject.SetActive (isLeftShow);
		right.gameObject.SetActive (isRightShow);
	}
}
