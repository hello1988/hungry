using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page5Ctrl : pageBase 
{
	[SerializeField]
	private Image focusImage;	// 過濾器(大圖)
	[SerializeField]
	private GameObject filterScroll; // 過濾器捲動區

	private selectFilter focusFilter;


	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);

	}

	void Start()
	{
		initFilterScroll ();
	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.E);
		setNextBtnActive(true);
		resetScrollItem ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void onFilterClick( selectFilter arg )
	{
		focusFilter = arg;

		Dictionary<DataMgr.FilterType,Dictionary<int, filterInfo>> filterMap = filterInfo.getFilterMap ();
		DataMgr.FilterType type = focusFilter.getFilterType ();
		int index = focusFilter.getFilterIndex ();
		focusImage.sprite = filterMap [type] [index].sprite_L;
	}

	public void selectFilter()
	{
		if (focusFilter == null) {return;}

		custom orderingCus = DataMgr.Instance.getOrderingCustom ();
		orderingCus.addPreferFilter ( focusFilter.getFilterType(), focusFilter.getFilterIndex() );

		focusFilter.setSelected (true);
	}

	public void nextPage()
	{
		pageMgr.Instance.nextPage (6);
	}

	private void resetScrollItem()
	{
		scrollCtrl ctrl = filterScroll.GetComponent<scrollCtrl> ();
		foreach (GameObject obj in ctrl.getItemList()) 
		{
			selectFilter fImg = obj.GetComponent<selectFilter> ();
			fImg.setSelected (false);
		}
	}

	private void initFilterScroll()
	{
		scrollCtrl ctrl = filterScroll.GetComponent<scrollCtrl> ();
		Dictionary<DataMgr.FilterType,Dictionary<int, filterInfo>> filterMap = filterInfo.getFilterMap ();
		foreach (DataMgr.FilterType type in filterMap.Keys) 
		{
			Dictionary<int, filterInfo> fInfo = filterMap [type];
			foreach (int index in fInfo.Keys) 
			{
				GameObject newObj = ctrl.addItem ();
				selectFilter fImg = newObj.GetComponent<selectFilter> ();
				fImg.setFilterType (type);
				fImg.setFilterIndex (index);
				newObj.GetComponent<Image> ().sprite = filterMap [type] [index].sprite_S;
			}
		}

	}
}
