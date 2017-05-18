using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class playAniAlpha : MonoBehaviour 
{
	[SerializeField]
	protected Sprite[] spriteList;
	[SerializeField]
	protected float swapSec = 3.0f;

	protected Sprite[] aniList;
	protected int spriteIndex;
	private GameObject shadow;
	private bool isLoop = true;
	private backAction completeCallBack = null;

	public void Awake () 
	{
		if ((spriteList != null) && (spriteList.Length > 0)) 
		{
			aniList = spriteList;
		}

	}

	// Update is called once per frame
	void Update () 
	{
		if( !getShadow().activeInHierarchy) {return ;}

		getShadow().transform.localPosition = transform.localPosition;
	}

	public void setAniList( Sprite[] newSpriteList )
	{
		aniList = newSpriteList;
		spriteIndex = 0;
		Image img = GetComponent<Image> ();
		img.sprite = aniList [spriteIndex];
	}

	public void setLoop( bool loop, backAction callBack=null )
	{
		isLoop = loop;
		completeCallBack = callBack;
	}

	public void play()
	{
		StopAllCoroutines ();
		if (!gameObject.activeInHierarchy) {return;}

		StartCoroutine ( nextSprite() );
	}

	public void stop()
	{
		StopAllCoroutines ();

		getShadow().SetActive (false);
	}

	private IEnumerator nextSprite()
	{
		getShadow().SetActive (true);
		Image sImg = getShadow().GetComponent<Image> ();
		Image img = GetComponent<Image> ();

		sImg.sprite = img.sprite;
		sImg.color = new Color (1,1,1,1);

		spriteIndex = (spriteIndex + 1) % aniList.Length;
		img.sprite = aniList [spriteIndex];
		img.color = new Color (1,1,1,0);

		LeanTween.value (gameObject, setSpriteAlpha, 0, 1, swapSec);

		yield return new WaitForSeconds (swapSec);

		if (!isLoop && (spriteIndex >= (aniList.Length - 1))) 
		{
			if (completeCallBack != null) 
			{
				completeCallBack ();
			}
		}
		else
		{
			play ();
		}
	}

	public void setSpriteAlpha( float val )
	{

		Image sImg = getShadow().GetComponent<Image> ();
		Image img = GetComponent<Image> ();

		sImg.color = new Color (1,1,1,(1-val));
		img.color = new Color (1,1,1,val);
	}

	private GameObject getShadow()
	{
		if( shadow == null )
		{
			shadow = new GameObject ("shadow");
			shadow.AddComponent<Image> ();
			shadow.transform.SetParent (transform.parent);

			shadow.transform.localPosition = transform.localPosition;
			shadow.transform.localScale = transform.localScale;

			RectTransform sRect = shadow.GetComponent<RectTransform> ();
			RectTransform rect = GetComponent<RectTransform> ();
			sRect.sizeDelta = rect.sizeDelta;	
		}

		return shadow;
	}
}
