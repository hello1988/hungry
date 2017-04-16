using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class alternateCustom : MonoBehaviour, IPointerClickHandler 
{
	[SerializeField]
	private GameObject photo;
	[SerializeField]
	private int oriSize = 300;		// 圖像原始尺寸
	[SerializeField]
	private int selectedSize = 250;	// 點擊後的尺寸

	private custom customData;
	private bool isSelected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void init(custom cus, float delaySec)
	{
		customData = cus;
		isSelected = false;
		setPhotoSize (oriSize);

		Image img = photo.GetComponent<Image> ();
		img.sprite = cus.cusPhoto;

		transform.localScale = Vector3.zero;
		StartCoroutine (resume(delaySec));
	}

	public IEnumerator resume( float delaySec )
	{
		yield return new WaitForSeconds (delaySec);

		LeanTween.scale (gameObject, Vector3.one, 0.3f);

	}

	public custom getCustom()
	{
		return customData;
	}

	public bool getSelected()
	{
		return isSelected;
	}

	public void setSelected( bool selected )
	{
		isSelected = selected;
		if (selected) 
		{
			setPhotoSize (selectedSize);
		}
		else 
		{
			setPhotoSize (oriSize);
		}
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		setSelected (!isSelected);
	}

	private void setPhotoSize( int length )
	{
		RectTransform rect = photo.GetComponent<RectTransform> ();
		rect.sizeDelta = Vector2.one*length;
	}
}
