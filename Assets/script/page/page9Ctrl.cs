using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// public class page8Ctrl : pageBase, IPointerDownHandler, IPointerUpHandler
public class page9Ctrl : pageBase
{
	[SerializeField]
	private Sprite[] demoSprite;
	[SerializeField]
	private Sprite waitSprite;
	[SerializeField]
	private Image img;

	private int demoIndex;
	void Awake () 
	{
		Button checkButton = nextBtn.GetComponent<Button> ();
		checkButton.onClick.AddListener (nextPage);

		homeVisible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public override void onPageEnable()
	{
		UIMgr.Instance.setBackground (UIMgr.BG.F);
		demoIndex = 0;
		setImg ();
	}

	public void onImgClick()
	{
		demoIndex++;
		if (demoIndex < demoSprite.Length) 
		{
			setImg ();
			setNextBtnActive (false);

			RectTransform rect = img.GetComponent<RectTransform> ();
			rect.sizeDelta = new Vector2 (1536, 2048);
		}
		else
		{
			img.sprite = waitSprite;
			setNextBtnActive (true);
			img.SetNativeSize ();
		}
	}

	private void setImg()
	{
		img.sprite = demoSprite[demoIndex];
	}

	public void nextPage()
	{
		pageMgr.Instance.homePage();
	}


}
