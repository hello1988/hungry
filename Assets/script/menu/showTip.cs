using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class showTip : MonoBehaviour, IPointerClickHandler 
{
	[SerializeField]
	private assist3Ctrl assCtrl;
	[SerializeField]
	private int tipIndex;

	private GameObject tipImage = null;
	private GameObject tipBG = null;
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		// StopAllCoroutines ();

		if (tipImage != null) 
		{
			tipImage.SetActive (false);
		}
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		bool isShow = true;
		if (tipImage == null) 
		{
			tipBG = createImageObj ();
			tipBG.name = "BG";
			Color color = new Color32 (255, 255, 255, 105);
			Image bgImg = tipBG.GetComponent<Image> ();
			bgImg.color = color;

			tipImage = createImageObj();
			tipImage.name = string.Format ("tipImage{0}", (tipIndex + 1));
		} 
		else 
		{
			isShow = !tipImage.activeInHierarchy;
		}

		if (isShow) 
		{
			Image img = tipImage.GetComponent<Image> ();
			img.sprite = assCtrl.getTipSprite (tipIndex);
			img.SetNativeSize ();
		}

		tipBG.SetActive (isShow);
		tipImage.SetActive (isShow);
	}

	public GameObject createImageObj()
	{
		GameObject newObj = new GameObject ();
		newObj.AddComponent<Image> ();
		newObj.transform.SetParent (transform);
		newObj.transform.localPosition = Vector3.zero;
		newObj.transform.localScale = Vector3.one;

		RectTransform rect = newObj.GetComponent<RectTransform> ();
		RectTransform parentRect = GetComponent<RectTransform> ();
		rect.sizeDelta = parentRect.sizeDelta;

		return newObj;
	}
}
