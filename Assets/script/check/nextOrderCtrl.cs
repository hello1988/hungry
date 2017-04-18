using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class nextOrderCtrl : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private page8Ctrl pageCtrl;
	[SerializeField]
	private GameObject templete;
	[SerializeField]
	private GameObject orderParent;
	[SerializeField]
	private float tweenSec = 0.3f;

	private List<GameObject> objList;
	private GameObject touchPoint;
	private Vector3 startPos = Vector3.zero;
	void Awake () 
	{
		objList = new List<GameObject> ();

		touchPoint = new GameObject ("touchPoint");
		touchPoint.transform.SetParent (transform.parent);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void resetOrder()
	{
		foreach (GameObject obj in objList) 
		{
			Destroy (obj);
		}

		objList.Clear ();
	}

	public void addOrder( Sprite sprite )
	{
		GameObject clone = Instantiate<GameObject> (templete);
		clone.name = string.Format ("order{0}",(objList.Count+1));
		clone.transform.SetParent (orderParent.transform);
		clone.transform.localScale = Vector3.one;

		RectTransform rect = templete.GetComponent<RectTransform> ();
		Vector3 pos = templete.transform.localPosition;
		float posY = pos.y - rect.sizeDelta.y * objList.Count;
		clone.transform.localPosition = new Vector3 (pos.x, posY, pos.z);

		clone.GetComponent<Image> ().sprite = sprite;
		clone.SetActive (true);
		objList.Add (clone);

	}

	public void OnPointerUp (PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		Vector3 endPos = touchPoint.transform.localPosition;

		float offsetY = (endPos - startPos).y;
		if (offsetY >= 100) 
		{
			pageCtrl.nextOrder ();
		}
		else if(offsetY <= -100)
		{
			pageCtrl.preOrder ();
		}
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		touchPoint.transform.position = UIMgr.Instance.getCurMousePosition ();
		startPos = touchPoint.transform.localPosition;
	}

	public void showPreOrder()
	{
		RectTransform rect = templete.GetComponent<RectTransform> ();
		Vector3 pos = orderParent.transform.localPosition;
		LeanTween.moveLocalY (orderParent.gameObject, (pos.y - rect.sizeDelta.y), 0.3f);
	}

	public void showNextOrder()
	{
		RectTransform rect = templete.GetComponent<RectTransform> ();
		Vector3 pos = orderParent.transform.localPosition;
		LeanTween.moveLocalY (orderParent.gameObject, (pos.y + rect.sizeDelta.y), 0.3f);
	}
}
