using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class selectFilter : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private page5Ctrl pageCtrl;

	private DataMgr.FilterType filterType;
	private int filterIndex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setFilterType( DataMgr.FilterType type )
	{
		filterType = type;
	}

	public DataMgr.FilterType getFilterType()
	{
		return filterType;
	}

	public void setFilterIndex(int id)
	{
		filterIndex = id;
	}

	public int getFilterIndex()
	{
		return filterIndex;
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		pageCtrl.onFilterClick (this);
	}

	public void setSelected(bool select)
	{
		Debug.logger.Log (string.Format("setSelected : {0}",select));
	}
}
