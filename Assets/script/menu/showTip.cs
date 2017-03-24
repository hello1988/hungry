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
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		StopAllCoroutines ();

		if (tipImage != null) 
		{
			tipImage.SetActive (false);
		}
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (tipImage == null) 
		{
			tipImage = new GameObject ();
			tipImage.name = string.Format ("tipImage{0}",(tipIndex+1));
			tipImage.AddComponent<Image> ();
			tipImage.transform.SetParent (transform);
			tipImage.transform.localPosition = Vector3.zero;
			tipImage.transform.localScale = Vector3.one;

			RectTransform rect = tipImage.GetComponent<RectTransform> ();
			RectTransform parentRect = GetComponent<RectTransform> ();
			rect.sizeDelta = parentRect.sizeDelta;

			Image img = tipImage.GetComponent<Image> ();
			img.sprite = assCtrl.getTipSprite (tipIndex);
		}

		tipImage.SetActive (true);
		StopAllCoroutines ();
		StartCoroutine (resume());
	}

	public IEnumerator resume()
	{
		yield return new WaitForSeconds (10);

		tipImage.SetActive (false);
	}
}
